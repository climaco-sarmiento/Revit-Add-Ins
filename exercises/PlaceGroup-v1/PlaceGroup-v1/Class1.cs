using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
namespace PlaceGroup_v1
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData com
            , ref string message, ElementSet elements)
        {
            //Get application and document objects
            UIApplication uiapp = ExternalCommandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            //Define a referene Object to accept the pick result

            Reference pickedref = null;

            //Pick a group
            Selection sel = uiapp.ActiveUIDocument.Selection;
            pickedref = sel.PickObject(ObjectType.Element, "Please select a group");
            Element elem = doc.GetElement(pickedref);
            Group group = elem as Group;

            //pick point
            XYZ point = sel.PickPoint("Please pick a poiint to place group");

            //Place the group
            Transaction trans = new Transaction(doc);
            trans.Start("Lab");
            doc.Create.PlaceGroup(point, group.GroupType);
            trans.Commit();

            return Result.Succeeded;
        }
    }
}