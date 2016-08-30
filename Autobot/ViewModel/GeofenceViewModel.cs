using Autobot.Common;
using Autobot.Model;
using Autobot.Services;
using MvvmCross.Core.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.ViewModel
{
    [ImplementPropertyChanged]
    public class GeofenceViewModel : MvxViewModel
    {
        private readonly IPresentationService presentationService;

        public IMvxCommand GeofenceDetailCommand { get; set; }
        public List<Geofence> GeofenceList { get; set; }
        public IMvxCommand MenuCommand { get; set; }

        public GeofenceViewModel(IPresentationService presentationService)
        {
            this.presentationService = presentationService;
            GeofenceDetailCommand = new MvxCommand<Geofence>(geo => OnGeofenceDetail(geo));
            MenuCommand = new MvxCommand(OnMenu);
        }

        public async void Resume()
        {
            GeofenceList = await Database.Default.GetGeofenceList();
        }

        private async void OnMenu()
        {
            var item = await presentationService.RequestNavigation();
            ShowViewModel(item.Target);
        }

        private void OnGeofenceDetail(Geofence geofence)
        {
            if (geofence == null)
            {
                ShowViewModel<GeofenceDetailViewModel>();
            }
            else
            {
                ShowViewModel<GeofenceDetailViewModel>(new { Id = geofence?.Id });
            }
        }
    }
}
