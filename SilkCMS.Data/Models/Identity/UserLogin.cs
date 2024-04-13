namespace SilkCMS.Data;

public class UserLogin : IdentityUserLogin<ObjectId>
{
    public ObjectId Id { get; set; }
}