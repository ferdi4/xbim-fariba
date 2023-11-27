using Xbim.Ifc;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.PresentationAppearanceResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.SharedBldgElements;

const string fileName = "SampleHouse.ifc";

var editor = new XbimEditorCredentials
{
    ApplicationDevelopersName = "Ferdi",
    ApplicationFullName = "",
    ApplicationIdentifier = "",
    ApplicationVersion = "",
    //your user
    EditorsFamilyName = "Fuchs",
    EditorsGivenName = "",
    EditorsOrganisationName = ""
};

using (IfcStore model = IfcStore.Open(fileName, editor))
{
    using (var txn = model.BeginTransaction("Quick start transaction"))
    {
        // load first slab
        var slab = model.Instances.FirstOrDefault<IfcSlab>();

        // TODO: we need connected color what ever
        var color = slab.Model.Instances.FirstOrDefault<IfcColourRgb>();
        // give me all windows in the ifc file
        color.Red = ((255 / 255));
        color.Green = ((0.0 / 255));
        color.Blue = ((255 / 255));

        // draw window names in the console


        // create material with name "Banana"
        var banana = model.Instances.New<IfcMaterial>(p =>
        {
            p.Name = "Banana";
        });

        // create material connection for our new material"
        var associatesMaterial = model.Instances.New<IfcRelAssociatesMaterial>(m =>
        {
            m.GlobalId = Guid.NewGuid(); //define uuid 
            m.RelatingMaterial = banana;
        });

        associatesMaterial.RelatedObjects.Add(slab); // connect our new material with slab slab selected in line 27

        txn.Commit();

        Console.WriteLine($"Slab {slab.Name} has now a new Material: {banana.Name}");
    }
    model.SaveAs("SampleHouse_Modified.ifc");
}
