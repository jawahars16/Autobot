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
    public class GeofenceListViewModel : MvxViewModel
    {
        private readonly IPresentationService presentationService;

        public IMvxCommand GeofenceDetailCommand { get; set; }

        public GeofenceListViewModel(IPresentationService presentationService)
        {
            this.presentationService = presentationService;
            GeofenceDetailCommand = new MvxCommand<Geofence>(geo => OnGeofenceDetail(geo));
        }

        private void OnGeofenceDetail(Geofence geofence)
        {
            presentationService.ShowDialog<GeofenceDetailViewModel>();
        }
    }
}
