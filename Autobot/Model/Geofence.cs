﻿using Autobot.Common;
using Autobot.Platform;
using PropertyChanged;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Model
{
    [ImplementPropertyChanged]
    public class Geofence : ISelectable
    {
        public Geofence(double latitude, double longitude, int radius)
        {
            Latitude = latitude;
            Longitude = longitude;
            Radius = radius;
        }

        public Geofence()
        {

        }

        [PrimaryKey]
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Radius { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Icon { get; set; }

        public async Task SaveAsync()
        {
            if (Id == null)
            {
                Id = $"{Latitude}_{Longitude}_{Radius}";
                Description = $"{Latitude}, {Longitude}";
                await Database.Default.SaveAsync(this);
            }
            else
            {
                await Database.Default.UpdateAsync(this);
            }
        }

        public async Task DeleteAsync()
        {
            await Database.Default.DeleteAsync(this);
        }
    }
}
