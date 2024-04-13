namespace SilkCMS.Data;

public class AppDbContext : ILiteDbContext
{

   public LiteDatabase LiteDatabase { get; private set ; }
   public AppDbContext(LiteDbOption liteDbOption)
   {
      var connectionString = $"Filename={liteDbOption.Database}; Connection={liteDbOption.Shared}";
      LiteDatabase = new LiteDatabase(connectionString);
   }

}

