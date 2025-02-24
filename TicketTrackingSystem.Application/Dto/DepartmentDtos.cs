using System.ComponentModel.DataAnnotations;

namespace TicketTrackingSystem.Application.Dto;
public class CreateDepartmentDto
{
    //[MinLength(2, ErrorMessage = "The Name is to short")]
    //[EmailAddress]
    [Required]
    [MaxLength(100, ErrorMessage = "The Name is to long")]
    public string Name { get; set; }

    [Required]
    [MaxLength(500, ErrorMessage = "The Description is to long")]
    public string Description { get; set; }
}

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}
public class UpdateDepartmentDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "The Name is to long")]
    public string Name { get; set; }
    [Required]
    [MaxLength(500, ErrorMessage = "The Description is to long")]
    public string Description { get; set; }
}
