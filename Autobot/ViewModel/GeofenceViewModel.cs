using Autobot.Model;
using Autobot.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.ViewModel
{
    public class GeofenceViewModel : MvxViewModel
    {
        private readonly IPresentationService presentationService;

        public IMvxCommand GeofenceDetailCommand { get; set; }

        public GeofenceViewModel(IPresentationService presentationService)
        {
            this.presentationService = presentationService;
            GeofenceDetailCommand = new MvxCommand<Geofence>(geo => OnGeofenceDetail(geo));
        }

        private void OnGeofenceDetail(Geofence geofence)
        {
            ShowViewModel<GeofenceDetailViewModel>();
        }
    }
}
