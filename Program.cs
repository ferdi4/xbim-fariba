using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.PresentationAppearanceResource;


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

        changeMaterialColor(model, "Brick, Common", 0, 0, 0);
        txn.Commit();
    }
    model.SaveAs("SampleHouse_Modified.ifc");
}

void changeMaterialColor(IfcStore model, string materialName, int red, int green, int blue)
{
    var models = model.Instances.Where<IIfcSurfaceStyle>(s => (s.Name?.ToString() ?? "").Contains(materialName));

    var a = model.Instances.OfType<IIfcSurfaceStyle>();

    foreach(var window in a)
    {
        Console.WriteLine(window.Name);
    }

    var amountOfStyles = models.Count();

    if (amountOfStyles != 1)
    {
        Console.WriteLine($"0 < n < 2 allowed. Found: n={amountOfStyles}");
        return;
    }
    try
    {
        var surefaceStyleRend = (IfcSurfaceStyleRendering)models.First().Styles.First();
        var colour = surefaceStyleRend.SurfaceColour;
        colour.Blue = blue/255.0;
        colour.Red = red/255.0;
        colour.Green = green/255;
     
    } catch(Exception e)
    {
        Console.WriteLine(e.Message);
        return;
    }
    Console.WriteLine("Color sucessfully changed.");

}