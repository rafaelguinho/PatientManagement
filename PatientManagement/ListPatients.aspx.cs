using Models.Interfaces.Repositories;
using System;
using System.Web.UI.WebControls;

public partial class ListPatients : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utils.CreateXmlFileIfNotExists();
            BindPatients();
        }
    }

    protected void GridViewPatients_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string email = e.CommandArgument.ToString();

        IPatientRepository patientRepository = PatientRepositoryFactory.Create();

        if (e.CommandName == "Edit")
        {
            Response.Redirect($"UpsertPatient.aspx?email={email}");
        }
        else if (e.CommandName == "Delete")
        {
            patientRepository.DeletePatient(email);
        }

        BindPatients();
    }

    private void BindPatients()
    {
        IPatientRepository patientRepository = PatientRepositoryFactory.Create();

        var patients = patientRepository.GetAllPatients();

        GridViewPatients.DataSource = patients;
        GridViewPatients.DataBind();
    }

    protected void GridViewPatients_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}