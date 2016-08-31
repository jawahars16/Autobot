using Autobot.Common;
using Autobot.Model;
using Autobot.Services;
using Autobot.Viewmodel;
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
    public class GeofenceDetailViewModel : MvxViewModel
    {
        private readonly IPresentationService presentationService;
        private readonly ILocationService locationService;

        public Geofence Geofence { get; set; }
        public IMvxCommand SaveCommand { get; set; }
        public IMvxCommand DeleteCommand { get; set; }

        public GeofenceDetailViewModel(IPresentationService presentationService, ILocationService locationService)
        {
            this.presentationService = presentationService;
            this.locationService = locationService;

            SaveCommand = new MvxCommand(OnSave);
            DeleteCommand = new MvxCommand(OnDelete);
        }

        protected async override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (parameters.Data.ContainsKey("Id"))
            {
                string id = parameters.Data["Id"];
                Geofence = await Database.Default.GetGeofence(id);
            }
            else
            {
                Geofence = new Geofence(13.112317, 80.155083, 100);
            }
        }

        private async void OnDelete()
        {
            var rules = await Database.Default.GetRulesByGeofence(Geofence.Id);
            if (!rules.Any())
            {
                await Geofence.DeleteAsync();
            }
        }

        private async void OnSave()
        {
            string title = await presentationService.PromptText("Enter title", "Enter a name for your geofence. (Eg. Work, Home, etc.)");
            Geofence.Title = title;
            await Geofence.SaveAsync();
            Close(this);
        }
    }
}
