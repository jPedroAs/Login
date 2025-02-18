namespace Login.Entity;

public class Register : BaseEntity
{
    public Register()
    {
        Name = string.Empty;
        Email = string.Empty;
        RA = string.Empty;
        Password = string.Empty;
    }
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string RA { get; set; }
    public string Password { get; set; }
}