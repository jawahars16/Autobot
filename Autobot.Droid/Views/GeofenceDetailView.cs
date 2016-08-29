using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using Autobot.ViewModel;
using Android.Gms.Maps;
using Autobot.Common;
using Android.Gms.Common;
using Com.Lilarcor.Cheeseknife;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Views.Animations;
using Android.Animation;
using Autobot.Model;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.Widget;
using Android.Widget;

namespace Autobot.Droid.Views
{
    [Activity(ParentActivity = typeof(GeofenceView))]
    public class GeofenceDetailView : MvxAppCompatActivity, IOnMapReadyCallback
    {
        [InjectView(Resource.Id.toolbar)]
        private Android.Support.V7.Widget.Toolbar toolbar;

        private IMenu menu;
        private Marker marker;
        private Circle circle;
        private LatLng defaultLocation = new LatLng(13.112317, 80.155083);
        private GoogleMap map;

        public GeofenceDetailViewModel ThisViewModel
        {
            get
            {
                return this.ViewModel as GeofenceDetailViewModel;
            }
        }

        private void RefreshMap(Geofence geofence)
        {
            if (map == null)
            {
                return;
            }

            if (circle != null)
            {
                circle.Remove();
            }

            var location = new LatLng(geofence.Latitude, geofence.Longitude);
            SetMarker(location);
            MoveCamera(location, geofence.Radius);
            AddGeofence(location, geofence.Radius);
        }

        [InjectOnCheckedChange(Resource.Id.availableUnits)]
        private void OnCheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            RefreshMap(ThisViewModel.Geofence);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_GeofenceDetail);
            Cheeseknife.Inject(this);
            SupportMapFragment mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
            SetSupportActionBar(toolbar);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.menu = menu;
            MenuInflater.Inflate(Resource.Menu.geofence_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.done:
                    ThisViewModel.SaveCommand.Execute();
                    break;
                case Resource.Id.delete:
                    ThisViewModel.DeleteCommand.Execute();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.MapClick += OnMapClick;
            RefreshMap(ThisViewModel.Geofence);
        }

        private void OnMapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ThisViewModel.Geofence.Latitude = e.Point.Latitude;
            ThisViewModel.Geofence.Longitude = e.Point.Longitude;

            RefreshMap(ThisViewModel.Geofence);
        }

        #region Helper Methods
        public void AddGeofence(LatLng location, int radius)
        {
            CircleOptions options = new CircleOptions();
            options
                .InvokeRadius(radius)
                .InvokeCenter(location)
                .InvokeFillColor(Color.ParseColor("#551ABC9C"))
                .InvokeStrokeColor(Color.ParseColor("#1ABC9C"))
                .InvokeStrokeWidth(5)
                .InvokeRadius(radius);

            circle = map.AddCircle(options);

            ValueAnimator vAnimator = new ValueAnimator();
            vAnimator.RepeatCount = 0;
            vAnimator.SetIntValues(0, radius);
            vAnimator.SetDuration(500);
            vAnimator.SetEvaluator(new IntEvaluator());
            vAnimator.SetInterpolator(new AccelerateDecelerateInterpolator());
            vAnimator.Update += (s, e) =>
            {
                float animatedFraction = e.Animation.AnimatedFraction;
                circle.Radius = animatedFraction * radius;
            };
            vAnimator.Start();
        }

        private void SetMarker(LatLng position)
        {
            if (marker == null)
            {
                MarkerOptions markerOptions = new MarkerOptions();
                markerOptions.SetPosition(position);
                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
                marker = map.AddMarker(markerOptions);
            }
            else
            {
                marker.Position = position;
            }
        }

        private void MoveCamera(LatLng location, int radius)
        {
            float zoom = GetZoomLevel(radius);
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(zoom);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.AnimateCamera(cameraUpdate);
        }

        private float GetZoomLevel(int radius)
        {
            double scale = radius / 500.0;
            return (float)(16 - Math.Log(1.5) / Math.Log(2f));
        }
        #endregion
    }
}