using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.UI;

namespace ewarcgis_new
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        public MapViewModel()
        {
            SetupMobileMap();
        }

        private Map _map = null;

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get => _map;
            set { _map = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
        async private void SetupMobileMap()
        {
            Boolean isMapLoaded = false;
            var mapPackageFolder = @"D:\EwLinear\Input\WorldMap\";
            var mapPackageFile = @"BaseMap_Updated.mmpk";
            var mapPackagePath = System.IO.Path.Combine(mapPackageFolder, mapPackageFile);
            try
            {
                var mapPackage = await MobileMapPackage.OpenAsync(mapPackagePath);
                if (mapPackage != null && mapPackage.Maps.Count > 0)
                {
                    Map = mapPackage.Maps[0];
                    Map.MaxScale = 10000;
                    Map.MinScale = 128000000;
                    Map.BackgroundColor = System.Drawing.Color.FromArgb(51, 161, 255); 
                    await Map.LoadAsync();
                    if (Map.LoadStatus != Esri.ArcGISRuntime.LoadStatus.FailedToLoad)
                    {
                        isMapLoaded = true;
                    }
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Write(ex.Message);
            }
            if (!isMapLoaded)
            {
                Map = new Map(Basemap.CreateStreetsVector());
            }
        }
    }
}
