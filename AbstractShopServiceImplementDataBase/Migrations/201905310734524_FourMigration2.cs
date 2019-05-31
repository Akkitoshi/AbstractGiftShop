namespace AbstractShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Implementers", "ImplementerFIO", c => c.String(nullable: false));
            DropColumn("dbo.Implementers", "ImplementerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Implementers", "ImplementerName", c => c.String(nullable: false));
            DropColumn("dbo.Implementers", "ImplementerFIO");
        }
    }
}
