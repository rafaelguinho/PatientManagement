using System.ComponentModel.DataAnnotations;

/// <summary>
/// Summary description for CreatePatientViewModel
/// </summary>
public class CreatePatientViewModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    public string Gender { get; set; }
    public string Notes { get; set; }
}