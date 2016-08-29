using Autobot.Model;
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
        public Geofence Geofence { get; set; }

        public List<AvailableUnit> AvailableUnits { get; set; }

        public AvailableUnit SelectedUnit { get; set; }

        public GeofenceDetailViewModel()
        {
            Geofence = new Geofence();
            Geofence.Radius = 500;
            AvailableUnits = new List<AvailableUnit>
            {
               new AvailableUnit("100 m", 100),
               new AvailableUnit("500 m", 500),
               new AvailableUnit("1 km", 1000),
               new AvailableUnit("5 km", 5000),
               new AvailableUnit("10 km", 10000)
            };
            SelectedUnit = AvailableUnits[1];
        }
    }
}
