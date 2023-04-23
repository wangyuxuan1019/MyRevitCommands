using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;


namespace MyRevitCommands
{
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class TotalLength : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get Document
            Document doc = uidoc.Document;

            try
            {
                //Pick Object
                IList<Reference> pickedObjs = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element).ToList();


                if (pickedObjs != null)
                {
                    double totalLength = 0;

                    foreach (Reference obj in pickedObjs)
                    {
                        //Retrieve Element
                        ElementId eleId = obj.ElementId;
                        Element ele = doc.GetElement(eleId);

                        //Get Parameter
                        Parameter param = ele.LookupParameter("Length");
                        if (param != null)
                        {
                            double length = param.AsDouble();
                            totalLength += length;
                        }                   
                        
                    }                                         

                    TaskDialog.Show("TotalLength", totalLength.ToString());
                }
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }

        }
    }
}
