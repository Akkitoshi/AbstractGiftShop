namespace AbstractShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Implementers", "ImplementerFIO", c => c.String(nullable: false));
            DropColumn("dbo.Implementers", "ImplementerFIO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Implementers", "ImplementerFIO", c => c.String(nullable: false));
            DropColumn("dbo.Implementers", "ImplementerFIO");
        }
    }
}
