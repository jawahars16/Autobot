using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autobot.Services;
using Android.Gms.Location;
using Android.Gms.Common.Apis;
using System.Threading.Tasks;
using Android.Gms.Common;
using Model = Autobot.Model;
using Autobot.Droid.Infrastructure.Triggers;
using Autobot.Common;
using Autobot.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using Autobot.Platform;
using Android;
using Android.Support.V4.App;
using Android.Support.V7.App;

namespace Autobot.Droid.Services
{
    public class LocationService : ILocationService
    {
        public const string GEOFENCE = "com.autobot.GEOFENCE";
        public const string ARRIVING = "ARRIVING";
        public const string LEAVING = "LEAVING";
        public const string GEOFENCE_RULES = "GEOFENCE_RULES";
        public const string GEOFENCE_KEY_DELIMITER = "#";
        public const int REQUEST_CODE = 0;

        private GoogleApiClient client;

        private Task<bool> BuildClientAsync()
        {
            var source = new TaskCompletionSource<bool>();
            client = new GoogleApiClient.Builder(Application.Context)
                .AddConnectionCallbacks(() =>
                {
                    source.SetResult(true);
                })
                .AddOnConnectionFailedListener((o) =>
                {
                    source.SetResult(false);
                })
                .AddApi(LocationServices.API)
                .Build();
            client.Connect();
            return source.Task;
        }

        public Task<bool> CheckPermission()
        {
            var source = new TaskCompletionSource<bool>();
            
            if(Android.Support.V4.Content.ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessFineLocation) == Android.Content.PM.Permission.Granted)
            {
                return Task.FromResult(true);
            }

            App.CurrentActivity.PermissionResult += (s, e) =>
            {
                if (e.RequestCode == REQUEST_CODE)
                {
                    source.SetResult(true);
                }
                else
                {
                    source.SetResult(false);
                }
            };

            ActivityCompat.RequestPermissions(App.CurrentActivity, new string[]
            {
               Manifest.Permission.AccessFineLocation,
               Manifest.Permission.AccessCoarseLocation
            }, REQUEST_CODE);

            return source.Task;
        }

        private string GetGeofenceId(Model.Rule rule)
        {
            return rule.Trigger.Tag.Split(GEOFENCE_KEY_DELIMITER[0])[1];
        }

        public async Task<bool> AddGeofence(Model.Rule rule)
        {
            bool clientStatus = await BuildClientAsync();

            if (clientStatus)
            {
                string geofenceId = GetGeofenceId(rule);
                Model.Geofence geofence = await Database.Default.GetGeofence(geofenceId);

                var request = GetGeofencingRequest(geofence.Id, geofence.Latitude, geofence.Longitude, geofence.Radius);
                var intent = await GetPendingIntentAsync(geofence.Id);

                bool permitted = await CheckPermission();
                bool result = false;

                if (permitted)
                {
                    Statuses status = await LocationServices.GeofencingApi.AddGeofencesAsync(
                        client,
                        request,
                        intent);
                    result = status.IsSuccess;
                }

                client.Disconnect();
                return result;
            }

            return false;
        }

        public async Task<bool> RemoveGeofence(Model.Rule rule)
        {
            bool clientStatus = await BuildClientAsync();

            if (clientStatus)
            {
                var intent = await GetPendingIntentAsync(GetGeofenceId(rule));
                Statuses status = await LocationServices.GeofencingApi.RemoveGeofencesAsync(
                    client,
                    intent);

                client.Disconnect();
                return status.IsSuccess;
            }

            return clientStatus;
        }

        private async Task<PendingIntent> GetPendingIntentAsync(string key)
        {
            var intent = new Intent(GEOFENCE);
            var rules = await Database.Default.GetRulesByGeofence(key);
            intent.PutStringArrayListExtra(GEOFENCE_RULES, rules.Select(rule => rule.Tag).ToList());
            return PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        private GeofencingRequest GetGeofencingRequest(string key, double latitude, double longitude, int radius)
        {
            var fence = new GeofenceBuilder()
                            .SetRequestId(key)
                            .SetCircularRegion(
                                latitude,
                                longitude,
                                radius
                            )
                            .SetExpirationDuration(Geofence.NeverExpire)
                            .SetTransitionTypes(Geofence.GeofenceTransitionEnter | Geofence.GeofenceTransitionExit)
                            .Build();

            var builder = new GeofencingRequest.Builder();
            builder.SetInitialTrigger(GeofencingRequest.InitialTriggerEnter);
            builder.AddGeofences(new List<IGeofence>
            {
                fence
            });

            return builder.Build();
        }

        public bool IsLocationTrigger(Model.Trigger trigger)
        {
            return trigger.Tag.Contains(GEOFENCE);
        }

        public async Task<Model.Trigger> HandleLocationTrigger(Model.Trigger trigger)
        {
            if (IsLocationTrigger(trigger))
            {
                var availableGeofences = await Database.Default.GetGeofenceList();

                if (availableGeofences.Any())
                {
                    Model.Geofence geofence = (Model.Geofence)await Prompt.Make(App.CurrentActivity, availableGeofences.Cast<ISelectable>()).ShowAsync();
                    trigger.Tag = $"{trigger.Tag}{GEOFENCE_KEY_DELIMITER}{geofence.Id}";
                }
                else
                {
                    // No geofences available...
                }
            }

            return trigger;
        }
    }
}