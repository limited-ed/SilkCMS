namespace SilkCMS.Data;
public class UserToken : IdentityUserToken<ObjectId>
{
    public ObjectId Id { get; set; }
}