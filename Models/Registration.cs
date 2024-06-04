namespace CRM.Models;

public class Registration
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public int IsActive { get; set; }
    public int IsApproved { get; set; }
}