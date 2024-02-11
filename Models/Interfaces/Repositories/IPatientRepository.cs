using System.Collections.Generic;

namespace Models.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        void AddPatient(Patient patient);
        void UpdatePatient(Patient updatedPatient);
        void DeletePatient(string email);
        List<Patient> GetAllPatients();

        Patient GetPatient(string email);
    }
}
