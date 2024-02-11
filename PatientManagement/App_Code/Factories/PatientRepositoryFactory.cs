using Models.Interfaces.Repositories;
using Repositories;
using System.Configuration;
using System.Web;

public static class PatientRepositoryFactory
{
    public static IPatientRepository Create()
    {
        string encryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
        string encryptionVector = ConfigurationManager.AppSettings["EncryptionVector"];
        string filePath = HttpContext.Current.Server.MapPath(Utils.XML_FILE_PATH);

        return new PatientRepository(filePath, encryptionKey, encryptionVector);
    }
}