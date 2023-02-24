using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyGroupPlugin
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopyGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uIDocument = commandData.Application.ActiveUIDocument;
            Document document = uIDocument.Document;

            Reference reference = uIDocument.Selection.PickObject(ObjectType.Element, "Выберите элементы");
            Element element = document.GetElement(reference);
            Group group = element as Group;

            XYZ point = uIDocument.Selection.PickPoint( "Выберите точку");

            Transaction transaction = new Transaction(document);
            transaction.Start("Копирование группы");
            document.Create.PlaceGroup(point, group.GroupType);
            transaction.Commit();

            return Result.Succeeded;
        }
    }
}
