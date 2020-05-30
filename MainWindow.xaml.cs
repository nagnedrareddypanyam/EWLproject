using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks.Geocoding;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;

namespace ewarcgis_new
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GroupLayer gLayer = new GroupLayer();
        GraphicsOverlay overviewoverlay = new GraphicsOverlay();
        GraphicsOverlay generaloverlay = new GraphicsOverlay();
        GraphicsOverlay coastaloverlay = new GraphicsOverlay();
        GraphicsOverlay approachoverlay = new GraphicsOverlay();
        GraphicsOverlay harbouroverlay = new GraphicsOverlay();
        GraphicsOverlay berthingoverlay = new GraphicsOverlay();
        GraphicsOverlay _sketchOverlay = new GraphicsOverlay();

        FeatureLayer overviewfeature = new FeatureLayer();
        FeatureLayer generalfeature = new FeatureLayer();
        FeatureLayer coastalfeature = new FeatureLayer();
        FeatureLayer approachfeature = new FeatureLayer();
        FeatureLayer harbourfeature = new FeatureLayer();
        FeatureLayer berthinfeature = new FeatureLayer();
        GraphicsOverlayCollection graphicoverlayscol = new GraphicsOverlayCollection();
        GraphicCollection graphic_collection ;

        //GraphicsOverlay overviewlabeloverlay = new GraphicsOverlay();
        //GraphicsOverlay generallabeloverlay = new GraphicsOverlay();
        //GraphicsOverlay coastallabeloverlay = new GraphicsOverlay();
        //GraphicsOverlay approachlabeloverlay = new GraphicsOverlay();
        //GraphicsOverlay harbourlabeloverlay = new GraphicsOverlay();
        //GraphicsOverlay berthinglabeloverlay = new GraphicsOverlay();


        //List<Graphic> total_Graphics = new List<Graphic>();
        //List<Graphic> overview_Graphic = new List<Graphic>();
        //List<Graphic> general_Graphic = new List<Graphic>();
        //List<Graphic> coastal_Graphic = new List<Graphic>();
        //List<Graphic> approach_Graphic = new List<Graphic>();
        //List<Graphic> harbour_Graphic = new List<Graphic>();
        //List<Graphic> berthing_Graphic = new List<Graphic>();
        private Graphic _polygonGraphic;
        public Dictionary<string, object> graphattribute;
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
          
        }
        private void Initialize()
        {
           // graphiclaysoff();
            
            MyMapView.GraphicsOverlays.Add(generaloverlay);
            MyMapView.GraphicsOverlays.Add(overviewoverlay);
            MyMapView.GraphicsOverlays.Add(coastaloverlay);
            MyMapView.GraphicsOverlays.Add(approachoverlay);
            MyMapView.GraphicsOverlays.Add(harbouroverlay);
            MyMapView.GraphicsOverlays.Add(berthingoverlay);

            MyMapView.GraphicsOverlays.Add(_sketchOverlay);

           // graphicoverlayscol.Add(generaloverlay);
           
          

            generaloverlay.Id = "one";//generallabeloverlay.Id = "seven";
            overviewoverlay.Id = "two";//overviewlabeloverlay.Id = "eight";
            coastaloverlay.Id = "three"; //coastallabeloverlay.Id = "nine";
            approachoverlay.Id = "four";//approachlabeloverlay.Id = "ten";
            harbouroverlay.Id = "five"; //harbourlabeloverlay.Id = "eleven";
            berthingoverlay.Id = "six"; //berthinglabeloverlay.Id = "twelve";
            _sketchOverlay.Id = "seven";
            check();
            //check_new();
            scaleforoverlay();
            graphiclaysoff();
            // Fill the combo box with choices for the sketch modes (shapes)
            SketchModeComboBox.ItemsSource = System.Enum.GetValues(typeof(SketchCreationMode));
            SketchModeComboBox.SelectedIndex = 0;
           // MyMapView.DrawStatusChanged += OnDrawStatusChanged;
            MyMapView.GeoViewTapped += MapViewTapped_Mouse_Point;
           MyMapView.PreviewMouseWheel += mouseWheel_Changed;
           // MyMapView.PreviewMouseWheel += scaleforoverlay;
        }
        private void cbAllFeatures_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool newVal = (cbAllFeatures.IsChecked == true);
            cbFeatureAbc.IsChecked = newVal;
            cbFeatureXyz.IsChecked = newVal;
            cbFeatureWww.IsChecked = newVal;
            cbFeatureAbc1.IsChecked = newVal;
            cbFeatureXyz1.IsChecked = newVal;
            cbFeatureWww1.IsChecked = newVal;

        }

        private void cbFeature_CheckedChanged(object sender, RoutedEventArgs e)
        {
            cbAllFeatures.IsChecked = null;
            if ((cbFeatureAbc.IsChecked == true) && (cbFeatureXyz.IsChecked == true) && (cbFeatureWww.IsChecked == true))
                cbAllFeatures.IsChecked = true;
            if ((cbFeatureAbc.IsChecked == false) && (cbFeatureXyz.IsChecked == false) && (cbFeatureWww.IsChecked == false))
                cbAllFeatures.IsChecked = false;
        }

        public void graphiclaysoff()
        {
            generaloverlay.IsVisible = false; //generallabeloverlay.IsVisible = false;
            overviewoverlay.IsVisible = true;//overviewlabeloverlay.IsVisible = true;
            coastaloverlay.IsVisible = false; //coastallabeloverlay.IsVisible = false;
            approachoverlay.IsVisible = false; //approachlabeloverlay.IsVisible = false;
            harbouroverlay.IsVisible = false; //harbourlabeloverlay.IsVisible = false;
            berthingoverlay.IsVisible = false;//berthinglabeloverlay.IsVisible = false;
        }
        private void scaleforoverlay()
        {
            // overviewoverlay.MinScale = generaloverlay.MinScale= coastaloverlay.MinScale = approachoverlay.MinScale= harbouroverlay.MinScale= berthingoverlay.MinScale= 100000;
            overviewoverlay.MaxScale = 10000;
            //generaloverlay.MaxScale= coastaloverlay.MaxScale= approachoverlay.MaxScale= harbouroverlay.MaxScale= berthingoverlay.MaxScale =   100000;
             
            overviewoverlay.MinScale = 128000000;
            
           
           // generaloverlay.MinScale = 102400000;
            
            //coastaloverlay.MinScale = 25600000;
            
           // approachoverlay.MinScale = 6400000;
            
           // harbouroverlay.MinScale = 1600000;
           
           // berthingoverlay.MinScale = 400000;


        }
        List<string> latitude = new List<string>();
        List<string> longitude = new List<string>();
        List<string> scale = new List<string>();
        List<string> usage = new List<string>();
        List<string> DatasetTitle = new List<string>();
        public void check_new()
        {
            XDocument doc = XDocument.Load(@"D:\EwLinear\Input\TEST_INTERNATIONALDAYLINE_INPUT_NEW.xml");
            int ScalarVariableCount = doc.Root.Descendants("Products")
                                  .Elements("ENC").Count();
            Console.WriteLine("count{0}", ScalarVariableCount);
            var polygonPoints1 = new Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84);
            var temppolygonPoints1 = new Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84);
            // var polygonPoints_list = new List<string, string>();
            var labelPointBass = new Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84);
            // IDictionary<string, Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84) > dict = new Dictionary<string, string>();
            var query = from Polygon in doc.Root.Descendants("ENC")
                        group Polygon by Polygon.Element("ShortName").Value into gr
                        select gr;

            var shortname = doc.Root.Descendants("shortname");
            var srt = shortname.ToString();
            var reasons = doc.Descendants("Polygon").ToList();


            //for (int i = 0; i < elemList.Count; i++)
            //{
            //    Console.WriteLine(elemList[i].InnerXml);
            //}
            // var textname = new List<string>();


            Func<XElement, string, string, string> getAttributeValue = (xElement, name, text) => xElement.Element(name).Value;


            // Task<IEnumerable<IGrouping<string, XElement>>> query;
            // query = ConfigurationManager.AppSettings.Get();

            // query = objMgr.GetAVCS();
            foreach (var value in query)
            {
                List<XElement> str = new List<XElement>();
                // textname.Add(value.Key);
                //  textname.Add(value.Key);
                var q2 = value.Descendants("Polygon").SelectMany(array => array.Value.ToList());

                value.Descendants("Polygon").ToList().ForEach(item =>
                {
                    str.Add(item);
                });
                if (str.Count > 1)
                {
                    foreach (var set1 in str)
                    {
                        set1.Descendants("Position").ToList().ForEach(item =>
                        {
                            latitude.Add(item.Attribute("latitude").Value);
                            longitude.Add(item.Attribute("longitude").Value);
                            temppolygonPoints1.Add(new MapPoint(Convert.ToDouble(item.Attribute("longitude").Value), Convert.ToDouble(item.Attribute("latitude").Value)));
                        });

                        foreach (var res in temppolygonPoints1)
                        {
                            if (Convert.ToDouble(res.X) < 0)
                            {
                                var longinew = Convert.ToDouble(res.X) + 360;
                                // longitude.Add(Convert.ToString(longinew));
                                polygonPoints1.Add(new MapPoint(longinew, Convert.ToDouble(res.Y)));
                            }
                            else
                            {
                                polygonPoints1.Add(new MapPoint(Convert.ToDouble(res.X), Convert.ToDouble(res.Y)));
                            }

                        }
                        var q5 = value.Descendants("Metadata").Select(s => new
                        {
                            ONE = s.Element("Scale").Value,
                            TWO = s.Element("Usage").Value,
                            THREE = s.Element("DatasetTitle").Value
                        }).ToList();
                        string four = q5[0].THREE;
                        string two = q5[0].TWO;
                        usage.Add(four);

                        CreateGraphic_Label(polygonPoints1, value.Key, two);
                        temppolygonPoints1.Clear();
                        polygonPoints1.Clear();
                    }
                }
                else
                {
                    value.Descendants("Position").ToList().ForEach(item =>
                    {
                        latitude.Add(item.Attribute("latitude").Value);
                        longitude.Add(item.Attribute("longitude").Value);
                        temppolygonPoints1.Add(new MapPoint(Convert.ToDouble(item.Attribute("longitude").Value), Convert.ToDouble(item.Attribute("latitude").Value)));
                    });

                    foreach (var res in temppolygonPoints1)
                    {
                        if (Convert.ToDouble(res.X) < 0)
                        {
                            var longinew = Convert.ToDouble(res.X) + 360;
                            // longitude.Add(Convert.ToString(longinew));
                            polygonPoints1.Add(new MapPoint(longinew, Convert.ToDouble(res.Y)));
                        }
                        else
                        {
                            polygonPoints1.Add(new MapPoint(Convert.ToDouble(res.X), Convert.ToDouble(res.Y)));
                        }

                    }




                    var q1 = value.Descendants("Metadata").Select(s => new
                    {
                        ONE = s.Element("Scale").Value,
                        TWO = s.Element("Usage").Value,
                        THREE = s.Element("DatasetTitle").Value
                    }).ToList();
                    string three = q1[0].THREE;
                    string two = q1[0].TWO;
                    string one = q1[0].ONE;
                    usage.Add(three);
                    CreateGraphic_Label(polygonPoints1, value.Key, two);

                }


                temppolygonPoints1.Clear();
                polygonPoints1.Clear();
                //}
            }


        }
        public void check()
        {
            XDocument doc = XDocument.Load(@"D:\EwLinear\Input\AVCS_Cateloge_wk45_new_modif.xml");
            int ScalarVariableCount = doc.Root.Descendants("Products")
                                  .Elements("ENC").Count();
            Console.WriteLine("count{0}", ScalarVariableCount);
            var polygonPoints1 = new Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84);

            // var polygonPoints_list = new List<string, string>();
            var labelPointBass = new Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84);
            // IDictionary<string, Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84) > dict = new Dictionary<string, string>();
            var query = from Polygon in doc.Root.Descendants("ENC")
                        group Polygon by Polygon.Element("ShortName").Value into gr
                        select gr;

            var shortname = doc.Root.Descendants("shortname");
            var srt = shortname.ToString();
            var reasons = doc.Descendants("Polygon").ToList();


            //for (int i = 0; i < elemList.Count; i++)
            //{
            //    Console.WriteLine(elemList[i].InnerXml);
            //}
            // var textname = new List<string>();
            var latitude = new List<string>();
            var longitude = new List<string>();
            var scale = new List<string>();
            var usage = new List<string>();
            var DatasetTitle = new List<string>();

            Func<XElement, string, string, string> getAttributeValue = (xElement, name, text) => xElement.Element(name).Value;


            // Task<IEnumerable<IGrouping<string, XElement>>> query;
            // query = ConfigurationManager.AppSettings.Get();

            // query = objMgr.GetAVCS();
            foreach (var value in query)
            {
                List<XElement> str = new List<XElement>();
                // textname.Add(value.Key);
                //  textname.Add(value.Key);
                var q2 = value.Descendants("Polygon").SelectMany(array => array.Value.ToList());

                value.Descendants("Polygon").ToList().ForEach(item =>
                {
                    str.Add(item);
                });
                if (str.Count > 1)
                {
                    foreach (var set1 in str)
                    {
                        set1.Descendants("Position").ToList().ForEach(item =>
                        {
                            latitude.Add(item.Attribute("latitude").Value);
                            longitude.Add(item.Attribute("longitude").Value);
                            polygonPoints1.Add(new MapPoint(Convert.ToDouble(item.Attribute("longitude").Value), Convert.ToDouble(item.Attribute("latitude").Value)));
                        });
                       
                        var q5 = value.Descendants("Metadata").Select(s => new
                        {
                            ONE = s.Element("Scale").Value,
                            TWO = s.Element("Usage").Value,
                            THREE = s.Element("DatasetTitle").Value
                        }).ToList();
                        string four = q5[0].THREE;
                         string two = q5[0].TWO;
                        usage.Add(four);
                        CreateGraphic_Label(polygonPoints1, value.Key, two);
                        polygonPoints1.Clear();
                    }
                }
                else
                {
                    value.Descendants("Position").ToList().ForEach(item =>
                    {
                        latitude.Add(item.Attribute("latitude").Value);
                        longitude.Add(item.Attribute("longitude").Value);
                        polygonPoints1.Add(new MapPoint(Convert.ToDouble(item.Attribute("longitude").Value), Convert.ToDouble(item.Attribute("latitude").Value)));


                    });
                    var q1 = value.Descendants("Metadata").Select(s => new
                    {
                        ONE = s.Element("Scale").Value,
                        TWO = s.Element("Usage").Value,
                        THREE = s.Element("DatasetTitle").Value
                    }).ToList();
                    string three = q1[0].THREE;
                    string two = q1[0].TWO;
                    string one = q1[0].ONE;
                    usage.Add(three);
                    CreateGraphic_Label(polygonPoints1, value.Key, two);

                }



                polygonPoints1.Clear();
                //}
            }


        }
        private void CreateGraphic_Label(Esri.ArcGISRuntime.Geometry.PointCollection _pointc, string key, string encName)
        {
            var Polygon = new Esri.ArcGISRuntime.Geometry.Polygon(_pointc);

            // Define the symbology of the polygon
            var polygonSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.FromArgb(20, 60, 62, 66),
                                   new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.FromArgb(46, 50, 54), 0.5));

            // a point to define where the text is drawn
            var labelbase1 = (Polygon.Extent);
            
            //text symbol which defines text size, the text and color
            TextSymbol textsym1 = new TextSymbol(key, System.Drawing.Color.FromArgb(255, 255, 255),
                                 12, Esri.ArcGISRuntime.Symbology.HorizontalAlignment.Center,
                                 Esri.ArcGISRuntime.Symbology.VerticalAlignment.Middle);
            textsym1.FontWeight = 0;
            ////create a graphic from the point and symbol
            Graphic _labelGraphic = new Graphic(labelbase1, textsym1);
            //Graphic _labelGraphic = new Graphic(labelbase1, textsym1);
            //polygonlabeloverlay.Graphics.Add(_labelGraphic);
            graphattribute = new Dictionary<string, object>();

            graphattribute.Add(key, encName);
           // graphattribute.Add(t1, two);
            _polygonGraphic = new Graphic(Polygon, graphattribute, polygonSymbol);
            if (encName == "1")
            {
              //  MyMapView.Map.OperationalLayers.Add(generalfeature);
               overviewoverlay.Graphics.Add(_polygonGraphic);
                overviewoverlay.Graphics.Add(_labelGraphic);
                graphic_collection = overviewoverlay.Graphics;
               // overviewlabeloverlay.Graphics.Add(_labelGraphic);
            }
            else if (encName == "2")
            {
                generaloverlay.Graphics.Add(_polygonGraphic);
                generaloverlay.Graphics.Add(_labelGraphic);
                graphic_collection = generaloverlay.Graphics;
                //generallabeloverlay.Graphics.Add(_labelGraphic);
            }
            else if (encName == "3")
            {
                coastaloverlay.Graphics.Add(_polygonGraphic);
                coastaloverlay.Graphics.Add(_labelGraphic);
                graphic_collection = coastaloverlay.Graphics;
                //coastallabeloverlay.Graphics.Add(_labelGraphic);
            }
            else if (encName == "4")
            {

                approachoverlay.Graphics.Add(_polygonGraphic);
                approachoverlay.Graphics.Add(_labelGraphic);
                graphic_collection = approachoverlay.Graphics;
                //approachlabeloverlay.Graphics.Add(_labelGraphic);
            }
            else if (encName == "5")
            {
                harbouroverlay.Graphics.Add(_polygonGraphic);
                harbouroverlay.Graphics.Add(_labelGraphic);
                graphic_collection = harbouroverlay.Graphics;
                //harbourlabeloverlay.Graphics.Add(_labelGraphic);
            }
            else if (encName == "6")
            {
                berthingoverlay.Graphics.Add(_polygonGraphic);
                berthingoverlay.Graphics.Add(_labelGraphic);
                graphic_collection = berthingoverlay.Graphics;
               // berthinglabeloverlay.Graphics.Add(_labelGraphic);
            }
            
        }
        private async void MapViewTapped_Mouse_Point(object sender, GeoViewInputEventArgs geoViewInputEventArgs)
        {
            //  MyMapView_MouseMove(sender, geoViewInputEventArgs);
            // IdentifyGraphicsOverlayResult result = null;
            //  Graphic _identifiedGraphic = null;
            _sketchOverlay.Graphics.Clear();
            try
            {
                var pixelTolerance = 1;
                var returnPopupsOnly = false;
                var maxResults = 100;
                System.Windows.Point tapScreenPoint = geoViewInputEventArgs.Position;
                MapPoint mapPoint = geoViewInputEventArgs.Location;
                var es = Mapcoordinates_Change(mapPoint);
                Graphic _pointgraph = point_graphic_creation(es);
                Graphic _identifiedGraphic = null;
                IReadOnlyList<IdentifyGraphicsOverlayResult> idGraphicOverlayResults = await MyMapView.IdentifyGraphicsOverlaysAsync(tapScreenPoint, pixelTolerance, returnPopupsOnly, maxResults);
                if (idGraphicOverlayResults.Count == 1)
                {
                    foreach (IdentifyGraphicsOverlayResult idGraphicResult in idGraphicOverlayResults)
                    {
                        // iterate all graphics in the overlay and select them
                        foreach (Graphic g in idGraphicResult.Graphics)
                        {
                            var ter=g.Symbol.ToString();
                            if (ter != "Esri.ArcGISRuntime.Symbology.TextSymbol")
                            {
                                if (g.IsSelected == true)
                                {
                                    g.IsSelected = false;
                                }
                                else
                                {
                                    g.IsSelected = true;
                                }

                            }
                            else
                            {
                                
                                var ter1 = g.Geometry;
                               // return;
                            }
                           

                        }   

                    }
                }
                else
                {
                    // iterate each graphics overlay
                    foreach (IdentifyGraphicsOverlayResult idGraphicResult in idGraphicOverlayResults)
                    {
                        // iterate all graphics in the overlay and select them
                        foreach (Graphic g in idGraphicResult.Graphics)
                        {
                            var ter = g.Symbol.ToString();
                            if (ter != "Esri.ArcGISRuntime.Symbology.TextSymbol")
                            {
                                if (g.IsSelected == true)
                                {
                                    g.IsSelected = false;
                                }
                                else
                                {
                                    g.IsSelected = true;
                                }

                            }
                            else
                            {

                                var ter1 = g.Geometry;
                                // return;
                            }

                        }
                    }

                    // Graphic _graphic = result.Graphics.Last();

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }



        }
       
        private Graphic point_graphic_creation(MapPoint point)
        {
            Graphic _polypointGraphic = null;
            //Create point symbol with outline
            // var pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, 10);
            // pointSymbol.Outline = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Blue, 2);
            var pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Transparent, 10);
            pointSymbol.Outline = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Transparent, 2);

            //Create point graphic with geometry & symbol
            _polypointGraphic = new Graphic(point, pointSymbol);

            //Add point graphic to graphic overlay
            //_sketchOverlay.Graphics.Add(_polypointGraphic);
            return _polypointGraphic;

        }
        static MapPoint Mapcoordinates_Change(MapPoint mapPoints)
        {
            try
            {
                // Create a point geometry in NYC in WGS84

                MapPoint startingPoint = new MapPoint(mapPoints.X, mapPoints.Y, SpatialReferences.WebMercator);
                // Perform the same projection without specified transformation
                MapPoint afterPoint1 = (MapPoint)GeometryEngine.Project(mapPoints, SpatialReference.Create(4326));
                return afterPoint1;
            }
            catch (Exception ex)
            {
                // latitude = "42.85888" longitude = "-79.40261"
                MapPoint startingpoint = new MapPoint(42.85888, -79.40261);
                return startingpoint; ;
            }



        }


       
        private void mouseWheel_Changed(object sender, MouseWheelEventArgs e)
        {
            var tes = MyMapView.GetCurrentViewpoint(ViewpointType.CenterAndScale);

            var list = new[] { 10000, 62500, 125000, 250000, 500000, 1000000, 2000000, 4000000, 8000000, 16000000, 32000000, 64000000, 128000000 };
            var input = tes.TargetScale;
            var diffList = from number in list
                           select new
                           {
                               number,
                               difference = Math.Abs(number - input)
                           };
            var result = (from diffItem in diffList
                          orderby diffItem.difference
                          select diffItem).First().number;

            double ImageScale = result;
            if (e.Delta > 0)
            {
                   // var tes = MyMapview.GetCurrentViewpoint(ViewpointType.CenterAndScale);
                
                    if (ImageScale > 10000 && ImageScale <= 125000)
                    {

                    berthingoverlay.IsVisible = true;
                    //berthingoverlay.ClearSelection();
                    //berthinglabeloverlay.IsVisible = true;
                   
                    }
                    else if (ImageScale > 125000 && ImageScale <= 500000)
                    {
                        harbouroverlay.IsVisible = true;
                        //harbouroverlay.ClearSelection();
                   // harbourlabeloverlay.IsVisible = true;
                  
                    }
                    else if (tes.TargetScale > 500000 && tes.TargetScale <= 2000000)
                    {
                        approachoverlay.IsVisible = true;
                       // approachoverlay.ClearSelection();
                        // approachlabeloverlay.IsVisible = true;
                 
                    }
                    else if (ImageScale > 2000000 && ImageScale <= 8000000)
                    {
                        coastaloverlay.IsVisible = true;
                       // coastaloverlay.ClearSelection();
                   // coastallabeloverlay.IsVisible = true;
                   
                    }
                    else if (ImageScale > 8000000 && ImageScale <= 32000000)
                    {
                         generaloverlay.IsVisible = true;
                       // generaloverlay.ClearSelection();
                   // generallabeloverlay.IsVisible = true;
                    

                    }
                    else if (ImageScale > 32000000 && ImageScale <= 128000000)
                    {
                        overviewoverlay.IsVisible = true;
                         //overviewoverlay.ClearSelection();
                    //overviewlabeloverlay.IsVisible = true;
                      
                    }
                }
            else if (e.Delta < 0)
            {
               // var tes = MyMapview.GetCurrentViewpoint(ViewpointType.CenterAndScale);

                if (ImageScale > 10000 && ImageScale <= 125000)
                {
                   
                    berthingoverlay.IsVisible = false;
                   // berthingoverlay.ClearSelection();
                   // berthinglabeloverlay.IsVisible = false;

                }
                else if (ImageScale > 125000 && ImageScale <= 500000)
                {
                 
                    harbouroverlay.IsVisible = false;
                   // harbouroverlay.ClearSelection();
                   // harbourlabeloverlay.IsVisible = false;
                  

                }
                else if (ImageScale > 500000 && ImageScale <= 2000000)
                {
                  
                    approachoverlay.IsVisible = false;
                    //approachoverlay.ClearSelection();
                   // approachlabeloverlay.IsVisible = false;

                }
                else if (ImageScale > 2000000 && ImageScale <= 8000000)
                {
                  
                    coastaloverlay.IsVisible = false;
                    //coastaloverlay.ClearSelection();
                    //coastallabeloverlay.IsVisible = false;

                }
                else if (ImageScale > 8000000 && ImageScale <= 32000000)
                {
                   
                    generaloverlay.IsVisible = false;
                    coastaloverlay.IsVisible = false;
                    approachoverlay.IsVisible = false;
                    berthingoverlay.IsVisible = false;
                    harbouroverlay.IsVisible = false;
                   // generaloverlay.ClearSelection();
                    //generallabeloverlay.IsVisible = false;

                }
                else if (tes.TargetScale > 32000000 && tes.TargetScale <= 128000000)
                {

                    overviewoverlay.IsVisible = true;
                    generaloverlay.IsVisible = false;
                    coastaloverlay.IsVisible = false;
                    approachoverlay.IsVisible = false;
                    berthingoverlay.IsVisible = false;
                    harbouroverlay.IsVisible = false;


                }

            }

        }
        Graphic routeline = null;
        Graphic polygondrawgrp = null;
        Graphic temprouteline = null;
        List<MapPoint> _mppoint = new List<MapPoint>();
        private async void DrawButtonClick(object sender, RoutedEventArgs e)
        {
            MyMapView.GeoViewTapped -= MapViewTapped_Mouse_Point;
            _sketchOverlay.Graphics.Clear();
            routeline = null;
            polygondrawgrp = null;
            try
            {
                // Let the user draw on the map view using the chosen sketch mode
                SketchCreationMode creationMode = (SketchCreationMode)SketchModeComboBox.SelectedItem;
                Esri.ArcGISRuntime.Geometry.Geometry geometry = await MyMapView.SketchEditor.StartAsync(creationMode, true);
               
                Graphic graphic = CreateGraphic(geometry);
                _sketchOverlay.Graphics.Add(graphic);
                if(geometry.GeometryType.ToString() == "Polyline")
                {
                    routeline = coordinatesystem_polyline(geometry);
                }
                else if(geometry.GeometryType.ToString() == "Polygon")
                {

                    polygondrawgrp = coordinatesystem_polygon(graphic);
                }
                

            }
            catch (TaskCanceledException)
            {
                // Ignore ... let the user cancel drawing
            }
            catch (Exception ex)
            {
                // Report exceptions
                MessageBox.Show("Error drawing graphic shape: " + ex.Message);
            }
            MyMapView.GeoViewTapped += MapViewTapped_Mouse_Point;


        }
        private Graphic CreateGraphic(Esri.ArcGISRuntime.Geometry.Geometry geometry)
        {
            // Create a graphic to display the specified geometry
            Symbol symbol = null;
            switch (geometry.GeometryType)
            {
                // Symbolize with a fill symbol
                case GeometryType.Envelope:
                case GeometryType.Polygon:
                    {
                        symbol = new SimpleFillSymbol()
                        {
                            Color = System.Drawing.Color.Transparent,
                            Style = SimpleFillSymbolStyle.Solid
                        };
                        break;
                    }
                // Symbolize with a line symbol
                case GeometryType.Polyline:
                    {
                        symbol = new SimpleLineSymbol()
                        {
                            Color = System.Drawing.Color.Red,
                            Style = SimpleLineSymbolStyle.Solid,
                            Width = 5d
                        };
                        break;
                    }
                // Symbolize with a marker symbol
                case GeometryType.Point:
                case GeometryType.Multipoint:
                    {

                        symbol = new SimpleMarkerSymbol()
                        {
                            Color = System.Drawing.Color.Red,
                            Style = SimpleMarkerSymbolStyle.Circle,
                            Size = 15d
                        };
                        break;
                    }
            }

            // pass back a new graphic with the appropriate symbol
            return new Graphic(geometry, symbol);
        }
        PolygonBuilder polygonbuild = null;
        public SketchCreationMode creationMode;
        private void Complete_click(object sender, RoutedEventArgs e)
        {
            completecommand(creationMode);
            //Symbolsystem_polyline(stopgraphiclin);
           // stopgraphicline.Clear();
           // routelineconfigclear();
           // MyMapview.GeoViewTapped -= MyMapView_OnGeoViewTapped;
           // MyMapView.GeoViewTapped -= MapViewTapped;
            e.Handled = true;
        }
        private void completecommand(SketchCreationMode sketch)
        {
            try
            {
                if (MyMapView.SketchEditor.CompleteCommand.CanExecute(null))
                {
                    MyMapView.SketchEditor.CompleteCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mylistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void mylistbox_SelectionChanged_default(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CancelButton_click(object sender, RoutedEventArgs e)
        {
            _sketchOverlay.Graphics.Clear();
            overviewoverlay.ClearSelection();
            generaloverlay.ClearSelection();
            coastaloverlay.ClearSelection();
            approachoverlay.ClearSelection();
            berthingoverlay.ClearSelection();
            harbouroverlay.ClearSelection();
            if (MyMapView.SketchEditor.CancelCommand.CanExecute(null))
            {
                MyMapView.SketchEditor.CancelCommand.Execute(null);
                // lvUsers.Items.Clear();
               // var systemCursor1 = System.Windows.Input.Cursors.Arrow;
               // Cursor = systemCursor1;
               // MyMapView.GeoViewTapped -= MyMapView_OnGeoViewTapped;
              // MyMapView.GeoViewTapped += MapViewTapped;
            }
        }

        private void SelectProductsUnderRootclick(object sender, RoutedEventArgs e)
        {
           
            var temproutegeom = MyMapView.SketchEditor.Geometry;//to know the routeline incomplete draw
            if (routeline != null)
            {
                Geometry_OnviewTap(routeline);
            }
            if(temproutegeom != null)
            {
                var tempgraphic = coordinatesystem_polyline(temproutegeom);
                Geometry_OnviewTap(tempgraphic);
            }
            if(polygondrawgrp!=null)
            {
                Geometry_OnviewTap(polygondrawgrp);
                _sketchOverlay.Graphics.Clear();
            }
            
        }
        private  void Geometry_OnviewTap(Graphic _graphic)
        {
            
            
                //var ree1 = graphic_collection.SelectMany(graphic => graphic.Attributes, (gr_key, gr_value) => new { gr_key, gr_value });
                var ere = _graphic.Geometry.GeometryType.ToString();
                List<GraphicsOverlay> grc = new List<GraphicsOverlay>();
                // GraphicCollection tempgrc=  
                //  GraphicsOverlayCollection grc=new GraphicsOverlayCollection() ;
                foreach (var item in MyMapView.GraphicsOverlays)
                {

                    if (item.Id != "seven")
                    {
                        grc.Add(item);
                    }
                }
                //var ree1 = grc.SelectMany(graphic => graphic.Attributes, (gr_key, gr_value) => new { gr_key, gr_value });

                if (ere == "Polyline")
                {
                    foreach (var ter in grc)
                    {

                        foreach (var item in ter.Graphics)
                        {
                            var textsymline = item.Symbol.ToString();
                            if (textsymline != "Esri.ArcGISRuntime.Symbology.TextSymbol")
                            {
                                if ((GeometryEngine.Intersects(item.Geometry, _graphic.Geometry) || GeometryEngine.Within(item.Geometry, _graphic.Geometry) || GeometryEngine.Overlaps(item.Geometry, _graphic.Geometry)))
                                {
                                    item.IsSelected = true;
                                }
                            }
                            else
                            {
                                var se = item.Geometry;
                            }
                        }


                    }
                }
                if (ere == "Polygon")
                {
                    foreach (var ter in grc)
                    {
                        foreach (var item in ter.Graphics)
                        {
                            var textsympolygon = item.Symbol.ToString();
                            if (textsympolygon != "Esri.ArcGISRuntime.Symbology.TextSymbol")
                            {
                                if ((GeometryEngine.Intersects(item.Geometry, _graphic.Geometry) || GeometryEngine.Within(item.Geometry, _graphic.Geometry) || GeometryEngine.Overlaps(item.Geometry, _graphic.Geometry)))
                                {
                                    item.IsSelected = true;
                                }
                            }
                            else
                            {
                                var ett = item.Geometry;
                            }

                        }


                    }
                }
            
            
           
               
           
        }
        private Graphic coordinatesystem_polygon(Graphic graphic)
        {
            Graphic polylgonGraphic = null;
            var poly = graphic.Geometry as Esri.ArcGISRuntime.Geometry.Polygon;
            this.polygonbuild = new PolygonBuilder(poly);
            foreach (var re in polygonbuild.Parts)
            {
                IReadOnlyList<MapPoint> mapPoints = re.Points;
                var polypoints = Mapcoordinates_Aftertransform(mapPoints);

                var polygon = new Esri.ArcGISRuntime.Geometry.Polygon(polypoints);

                //Create symbol for polyline
                var polylineSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.Transparent,
                    new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.FromArgb(0, 0, 255), 2));
                //Create a polyline graphic with geometry and symbol
                polylgonGraphic = new Graphic(polygon, polylineSymbol);

                //Add polygone to graphics overlay

                _sketchOverlay.Graphics.Add(polylgonGraphic);
                Esri.ArcGISRuntime.Geometry.Geometry gr = polygon;



            }
            return polylgonGraphic;
        }


        IReadOnlyList<MapPoint> _polypointcollection = new List<MapPoint>();
        PolylineBuilder polylineBuilder = null;
        private Graphic coordinatesystem_polyline(Esri.ArcGISRuntime.Geometry.Geometry geometry)
        {
            Graphic _polylineGraphic = null;
            var roadPolyline = geometry as Esri.ArcGISRuntime.Geometry.Polyline;
            // var roadPolyline = graphic.Geometry as Esri.ArcGISRuntime.Geometry.Polyline;
            this.polylineBuilder = new PolylineBuilder(roadPolyline);
            foreach (var r in polylineBuilder.Parts)
            {
                IReadOnlyList<MapPoint> mapPoints = r.Points;
                var polypoints = Mapcoordinates_Aftertransform(mapPoints);
                _polypointcollection = polypoints;
                //HandleMapTap1(polypoints);

                //var polypoints = Mapcoordinates_Aftertransform(mapPoints);
                //_polypointcollection = polypoints;//new
                var polyline = new Esri.ArcGISRuntime.Geometry.Polyline(polypoints);
                //Create symbol for polyline
                //  var polylineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.FromArgb(0, 0, 255), 3);
                // var polys3 = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Green, 3);
                var polys3 = new SimpleLineSymbol(SimpleLineSymbolStyle.DashDot, System.Drawing.Color.FromArgb(0, 0, 255), 3);
                // polys3.MarkerPlacement = SimpleLineSymbolMarkerPlacement.End;
                // polys3.ma = getpic1();
                //polys3.MarkerStyle = SimpleLineSymbolMarkerStyle.Arrow;
                //polys3.MarkerPlacement = SimpleLineSymbolMarkerPlacement.BeginAndEnd;

                //Create a polyline graphic with geometry and symbol
                _polylineGraphic = new Graphic(polyline, polys3);

                //Add polyline to graphics overlay

                //  _sketchPolylineOverlay.Graphics.Add(_polylineGraphic);
                Esri.ArcGISRuntime.Geometry.Geometry gr = polyline;


            }

            return _polylineGraphic;
        }
        static List<MapPoint> Mapcoordinates_Aftertransform(IReadOnlyList<MapPoint> _mappoint)
        {
            List<MapPoint> polypts = new List<MapPoint>();
            MapPoint afteraddegreeadd = null;
            MapPoint afterPoint1 = null;
            try
            {
                foreach (var se in _mappoint)
                {
                    double x = se.X;
                    double y = se.Y;
                    MapPoint Point1 = new MapPoint(x, y, SpatialReferences.WebMercator);
                    MapPoint temp= (MapPoint)GeometryEngine.Project(Point1, SpatialReference.Create(4326));

                    if (temp.X < 0)
                    {
                        double x1 = temp.X + 360;
                        afteraddegreeadd = new MapPoint(x1, temp.Y, SpatialReference.Create(4326));
                        afterPoint1 = afteraddegreeadd;
                        // polypts.Add(afteraddegreeadd);
                    }
                    else
                    {
                        afterPoint1 = temp;
                    }


                    //else
                    //{
                    //    polypts.Add(afterPoint1);
                    //}
                    polypts.Add(afterPoint1);
                }
                return polypts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return polypts;
            }
        }


        private LocatorTask _geocoder;
        private async void searchbtnclick(object sender, RoutedEventArgs e)
        {

           // UpdateSearch();
            string serch = srchtextbox.Text.ToUpper();
            //var matches = await _geocoder.GeocodeAsync(serch);
            //var pos = serch[2];
          
          //  GraphicsOverlayCollection graphicoverlaycollection = new GraphicsOverlayCollection();
            List<GraphicsOverlay> graphicoverlaycollection = new List<GraphicsOverlay>();
            var ter = MyMapView.GraphicsOverlays;
            foreach (var item in ter)
            {
                graphicoverlaycollection.Add(item);
            }
            // GraphicCollection graphicoverlaycollection1 = graphicoverlaycollection.Graphics;
            var tes = MyMapView.GetCurrentViewpoint(ViewpointType.CenterAndScale);
            var scale = tes.TargetScale;
            foreach (var item in graphicoverlaycollection)
            {

                foreach (var item1 in item.Graphics)
                {
                    if (item1.Attributes.ContainsKey(serch))
                    {
                        item1.IsSelected = true;
                    }
                }
            }
            ////var ree1 = graphicoverlaycollection.SelectMany(graphic => graphic.Attributes, (gr_key, gr_value) => new { gr_key, gr_value });
            //foreach (var tem in ree1)
            //{
            //    if (tem.gr_value.Key == serch)
            //    {
            //        tem.gr_key.IsSelected = true;
            //        //ZoomToLocation(tem.gr_key, scale);
            //    }
            //}
        }
       







    }
}

