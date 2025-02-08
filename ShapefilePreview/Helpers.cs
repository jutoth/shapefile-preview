using MaxRev.Gdal.Core;
using OSGeo.OGR;
using OSGeo.OSR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapefilePreview
{
    internal class Helpers
    {
        private static bool isInitialized = false;

        public static void InitializeGdal()
        {
            if (!isInitialized)
            {
                GdalBase.ConfigureAll();
                isInitialized = true;
            }
        }

        public static string ReadAndDisplayLayerInfo(string filePath)
        {
            InitializeGdal();
            StringBuilder info = new StringBuilder();

            using (DataSource ds = Ogr.Open(filePath, 0))
            {
                if (ds == null)
                {
                    return "Error: Unable to open the file.";
                }

                info.AppendLine($"File: {Path.GetFileName(filePath)}\n");

                for (int i = 0; i < ds.GetLayerCount(); i++)
                {
                    Layer layer = ds.GetLayerByIndex(i);
                    info.AppendLine($"## Layer {i + 1}: {layer.GetName()}\n");
                    info.AppendLine($"**Feature Count:** {layer.GetFeatureCount(1)}");
                    info.AppendLine($"**Geometry Type:** {layer.GetGeomType()}");

                    info.AppendLine("\n**Spatial Reference:**");
                    SpatialReference spatialRef = layer.GetSpatialRef();
                    if (spatialRef != null)
                    {
                        string wkt;
                        spatialRef.ExportToPrettyWkt(out wkt, 0);
                        info.AppendLine(wkt);
                    }
                    else
                    {
                        info.AppendLine("No spatial reference found");
                    }

                    info.AppendLine("\n**Fields:**");
                    FeatureDefn featureDefn = layer.GetLayerDefn();
                    for (int j = 0; j < featureDefn.GetFieldCount(); j++)
                    {
                        FieldDefn fieldDefn = featureDefn.GetFieldDefn(j);
                        info.AppendLine($"- {fieldDefn.GetName()} ({fieldDefn.GetFieldTypeName(fieldDefn.GetFieldType())})");
                    }

                    info.AppendLine();
                }
            }

            return info.ToString();
        }
    }
}


