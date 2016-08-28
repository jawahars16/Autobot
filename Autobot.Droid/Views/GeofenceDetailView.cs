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

namespace Autobot.Droid.Views
{
    [Activity(ParentActivity = typeof(GeofenceView))]
    public class GeofenceDetailView : MvxFragmentActivity, IOnMapReadyCallback
    {
        private Marker marker;
        private Circle circle;
        private LatLng defaultLocation = new LatLng(13.112317, 80.155083);
        private GoogleMap map;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_GeofenceDetail);
            Cheeseknife.Inject(this);
            SupportMapFragment mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            SetMarker(defaultLocation);
            MoveCamera(defaultLocation);
            AddGeofence(defaultLocation, 500);
            map.MapClick += OnMapClick;
        }

        private void OnMapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            if(circle != null)
            {
                circle.Remove();
            }
            marker.Position = e.Point;
            AddGeofence(e.Point, 500);
        }

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
            MoveCamera(location, GetZoomLevel(circle));

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

        public void AnimateMarker(LatLng toPosition, bool hideMarker)
        {
            Handler handler = new Handler();
            long start = SystemClock.ElapsedRealtime();

            Projection proj = map.Projection;
            Point startPoint = proj.ToScreenLocation(marker.Position);
            LatLng startLatLng = proj.FromScreenLocation(startPoint);

            long duration = 500;
            var interpolator = new LinearInterpolator();
            Action action = null;

            action = ()=>
            {
                long elapsed = SystemClock.ElapsedRealtime() - start;
                float t = interpolator.GetInterpolation((float)elapsed
                        / duration);
                double lng = t * toPosition.Longitude + (1 - t)
                        * startLatLng.Longitude;
                double lat = t * toPosition.Latitude + (1 - t)
                        * startLatLng.Longitude;
                marker.Position = new LatLng(lat, lng);
                if (t < 1.0)
                {
                    // Post again 16ms later.
                    handler.PostDelayed(action, 16);
                }
                else
                {
                    if (hideMarker)
                    {
                        marker.Visible = false;
                    }
                    else
                    {
                        marker.Visible = true;
                    }
                }
            };

            handler.Post(action);
        }

        private void SetMarker(LatLng position)
        {
            MarkerOptions markerOptions = new MarkerOptions();
            markerOptions.SetPosition(position);
            markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
            marker = map.AddMarker(markerOptions);
        }

        private void MoveCamera(LatLng location, float zoom = 15)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(zoom);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.AnimateCamera(cameraUpdate);
        }

        private int AddGeofence(LatLng location, double radius)
        {
            CircleOptions options = new CircleOptions();
            options
                .InvokeRadius(radius)
                .InvokeCenter(location)
                .InvokeFillColor(Color.ParseColor("#80FFFFFF"))
                .InvokeStrokeColor(Color.ParseColor("#0079CA"))
                .InvokeStrokeWidth(5);
            Circle circle = map.AddCircle(options);
            circle.Visible = true;
            return GetZoomLevel(circle);
        }

        private int GetZoomLevel(Circle circle)
        {
            if (circle != null)
            {
                double radius = circle.Radius;
                double scale = radius / 500;
                return (int)(16 - Math.Log(1.5) / Math.Log(2));
            }
            return 0;
        }

    }
}