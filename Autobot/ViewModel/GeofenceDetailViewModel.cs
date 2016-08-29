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

        public Geofence Geofence { get; set; }
        public IMvxCommand SaveCommand { get; set; }
        public IMvxCommand DeleteCommand { get; set; }

        public GeofenceDetailViewModel(IPresentationService presentationService)
        {
            this.presentationService = presentationService;
            Geofence = new Geofence(13.112317, 80.155083, 500);
            SaveCommand = new MvxCommand(OnSave);
            DeleteCommand = new MvxCommand(OnDelete);
        }

        private async void OnDelete()
        {
            await Geofence.DeleteAsync();
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
