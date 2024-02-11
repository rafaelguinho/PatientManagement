using System.IO;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for Utils
/// </summary>
public static class Utils
{
    public static string XML_FILE_PATH = "~/App_Data/patients.xml";

    public static void CreateXmlFileIfNotExists()
    {
        string filePath = HttpContext.Current.Server.MapPath(Utils.XML_FILE_PATH);

        // Check if the file already exists
        if (!File.Exists(filePath))
        {
            // Create a new XML document with root element
            XDocument doc = new XDocument(new XElement("Patients"));

            // Save the document to the file
            doc.Save(filePath);
        }
    }
}