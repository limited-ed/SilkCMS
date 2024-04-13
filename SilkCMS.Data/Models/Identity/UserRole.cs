namespace SilkCMS.Data;

public class UserRole : IdentityUserRole<ObjectId>
{
    public ObjectId Id { get; set; }
}
