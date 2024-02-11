using Models;
using Models.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Repositories
{

    public class PatientRepository : IPatientRepository
    {
        private readonly string xmlFilePath;
        private readonly string encryptionKey;
        private readonly string encryptionVector;

        public PatientRepository(string filePath, string encryptionKey, string encryptionVector)
        {
            xmlFilePath = filePath;
            this.encryptionKey = encryptionKey;
            this.encryptionVector = encryptionVector;
        }

        public void AddPatient(Patient patient)
        {
            XDocument doc = GetXmlDocument();
            XElement patientsElement = doc.Root;
            patientsElement.Add(CreatePatientElement(patient));
            SaveXmlDocument(doc);
        }

        public void UpdatePatient(Patient updatedPatient)
        {
            XDocument doc = GetXmlDocument();
            XElement patientElement = FindPatientElement(doc, updatedPatient.Email);
            if (patientElement != null)
            {
                patientElement.SetElementValue("FirstName", Encrypt(updatedPatient.FirstName));
                patientElement.SetElementValue("LastName", Encrypt(updatedPatient.LastName));
                patientElement.SetElementValue("Phone", Encrypt(updatedPatient.Phone));
                patientElement.SetElementValue("Gender", Encrypt(updatedPatient.Gender));
                patientElement.SetElementValue("Notes", Encrypt(updatedPatient.Notes));
                patientElement.SetElementValue("LastUpdatedDate", DateTime.Now.ToString());
                SaveXmlDocument(doc);
            }
        }

        public void DeletePatient(string email)
        {
            XDocument doc = GetXmlDocument();
            XElement patientElement = FindPatientElement(doc, email);
            if (patientElement != null)
            {
                patientElement.SetElementValue("IsDeleted", true);
                SaveXmlDocument(doc);
            }
        }

        public List<Patient> GetAllPatients()
        {
            XDocument doc = GetXmlDocument();
            return doc.Root
                .Elements("Patient")
                .Where(p => Convert.ToBoolean(p.Element("IsDeleted").Value) == false)
                .Select(p => new Patient
                {
                    FirstName = Decrypt(p.Element("FirstName").Value),
                    LastName = Decrypt(p.Element("LastName").Value),
                    Phone = Decrypt(p.Element("Phone").Value),
                    Email = Decrypt(p.Element("Email").Value),
                    Gender = Decrypt(p.Element("Gender").Value),
                    Notes = Decrypt(p.Element("Notes").Value),
                    CreatedDate = DateTime.Parse(p.Element("CreatedDate").Value),
                    LastUpdatedDate = DateTime.Parse(p.Element("LastUpdatedDate").Value),
                    IsDeleted = Convert.ToBoolean(p.Element("IsDeleted").Value)
                })
                .ToList();
        }

        private XDocument GetXmlDocument()
        {
            if (!File.Exists(xmlFilePath))
            {
                return new XDocument(new XElement("Patients"));
            }
            else
            {
                return XDocument.Load(xmlFilePath);
            }
        }

        private void SaveXmlDocument(XDocument doc)
        {
            doc.Save(xmlFilePath);
        }

        private XElement CreatePatientElement(Patient patient)
        {
            return new XElement("Patient",
                new XElement("FirstName", Encrypt(patient.FirstName)),
                new XElement("LastName", Encrypt(patient.LastName)),
                new XElement("Phone", Encrypt(patient.Phone)),
                new XElement("Email", Encrypt(patient.Email)),
                new XElement("Gender", Encrypt(patient.Gender)),
                new XElement("Notes", Encrypt(patient.Notes)),
                new XElement("CreatedDate", DateTime.Now.ToString()),
                new XElement("LastUpdatedDate", DateTime.Now.ToString()),
                new XElement("IsDeleted", false)
            );
        }

        private XElement FindPatientElement(XDocument doc, string email)
        {
            return doc.Root.Elements("Patient")
                .FirstOrDefault(p => Decrypt(p.Element("Email").Value) == email);
        }

        private string Encrypt(string input)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(encryptionKey);
                aesAlg.IV = Convert.FromBase64String(encryptionVector);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        private string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(encryptionKey);
                aesAlg.IV = Convert.FromBase64String(encryptionVector);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public Patient GetPatient(string email)
        {
            XDocument doc = GetXmlDocument();
            XElement patientElement = FindPatientElement(doc, email);

            if(patientElement == null) return null;

            return new Patient
            {
                FirstName = Decrypt(patientElement.Element("FirstName").Value),
                LastName = Decrypt(patientElement.Element("LastName").Value),
                Phone = Decrypt(patientElement.Element("Phone").Value),
                Email = Decrypt(patientElement.Element("Email").Value),
                Gender = Decrypt(patientElement.Element("Gender").Value),
                Notes = Decrypt(patientElement.Element("Notes").Value),
                CreatedDate = DateTime.Parse(patientElement.Element("CreatedDate").Value),
                LastUpdatedDate = DateTime.Parse(patientElement.Element("LastUpdatedDate").Value),
                IsDeleted = Convert.ToBoolean(patientElement.Element("IsDeleted").Value)
            };
        }
    }
}
