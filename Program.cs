using Xbim.Ifc;

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
        txn.Commit();
    }
    model.SaveAs("SampleHouse_Modified.ifc");
}