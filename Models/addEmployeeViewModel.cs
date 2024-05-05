using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace car_workshop_csharp.Models; 


[BindProperties]
public class AddEmployeeViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(30, ErrorMessage = "Name is too long")]
    public string Name { get; set; } 
    [Required(ErrorMessage = "Hourly rate is required")]
    [Range(1, 1000, ErrorMessage = "Hourly rate must be between 1 and 1000")]
    public float HourlyRate { get; set; }
}