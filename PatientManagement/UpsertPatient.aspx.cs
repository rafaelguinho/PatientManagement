using Models;
using Models.Interfaces.Repositories;
using System;

public partial class UpsertPatient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utils.CreateXmlFileIfNotExists();

            if (Request.QueryString["email"] != null)
            {
                IPatientRepository patientRepository = PatientRepositoryFactory.Create();
                Patient patient = patientRepository.GetPatient(Request.QueryString["email"]);
                
                if (patient != null)
                {
                    txtFirstName.Text = patient.FirstName;
                    txtLastName.Text = patient.LastName;
                    txtEmail.Text = patient.Email;
                    txtPhone.Text = patient.Phone;
                    ddlGender.SelectedValue = patient.Gender;
                    txtNotes.Text = patient.Notes;
                }
            }
        }

    }

    protected void FormUpsertPatient_Submit(object sender, EventArgs e)
    {
        string firstName = txtFirstName.Text.Trim();
        string lastName = txtLastName.Text.Trim();
        string phone = txtPhone.Text.Trim();
        string email = txtEmail.Text.Trim();
        string notes = txtNotes.Text.Trim();
        string gender = ddlGender.SelectedValue;

        Patient newPatient = new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            Phone = phone,
            Email = email,
            Notes = notes,
            Gender = gender
        };

        IPatientRepository patientRepository = PatientRepositoryFactory.Create();

        if (Request.QueryString["email"] != null)
        {
            patientRepository.UpdatePatient(newPatient);
        }
        else
        {
            patientRepository.AddPatient(newPatient);
        }

        

        Response.Redirect("ListPatients.aspx");
    }
}