using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NumberOfWallsByFloor
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            string floor1 = "Level 1 HVAC Plan";
            string floor2 = "Level 2 HVAC Plan";
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            List<View> vievList = new FilteredElementCollector(doc)
               .OfCategory(BuiltInCategory.OST_Views)
               .WhereElementIsNotElementType()
               .Cast<View>()
               .ToList();
          
            NumWalls(floor1, vievList);
            NumWalls(floor2 , vievList);

            void NumWalls(string floor, List<View> list)
            {
                foreach (View v in list)
                {
                    if (v.Name.ToString() == floor)
                    {
                        List<Element> m = new FilteredElementCollector(doc, v.Id)
                        .OfCategory(BuiltInCategory.OST_Walls)
                         .WhereElementIsNotElementType()
                        .Cast<Element>()
                         .ToList();
                        TaskDialog.Show("?", $"Количество стен: {m.Count.ToString()} на {floor}");

                    }
                }
            }
            return Result.Succeeded;
        }
    }
}
