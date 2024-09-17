using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Login.Entity;

public class Register : BaseEntity
{
    public Register()
    {
        Name = string.Empty;
        Email = string.Empty;
        Birthday = string.Empty;
        Password = string.Empty;
    }
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Birthday { get; set; }
    public string Password { get; set; }
}