using FinalPRN221.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public DateTime? Dob { get; set; }
    public bool? Gender { get; set; }
    public string? Address { get; set; }
    public int? DepartmentId { get; set; }
    public int? PositionId { get; set; }
    public DateTime? StartDate { get; set; }
    public string? AvatarId { get; set; }
    public Position? Position { get; set; } // Navigation property
    public Department? Department { get; set; }
}