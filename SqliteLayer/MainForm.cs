using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Samples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;

            LayerOverlay overlay = new LayerOverlay();

            //To resolve issue that we cannot run the executable by double click it on linux, we need to find out the absolute path by reflection.
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sqliteFullFileName = Path.GetFullPath(Path.Combine(baseDirectory, "../../App_Data/USStates.sqlite"));

            SqliteFeatureLayer sqliteFeatureLayer = new SqliteFeatureLayer($"Data Source={sqliteFullFileName};Version=3;", "table_name", "id", "geometry");
			sqliteFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.State1;
			sqliteFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            overlay.Layers.Add(sqliteFeatureLayer);

            winformsMap1.Overlays.Add(overlay);

            sqliteFeatureLayer.Open();
            winformsMap1.CurrentExtent = sqliteFeatureLayer.GetBoundingBox();

            winformsMap1.Refresh();
        }
    }
}