namespace AbstractShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SClients", "Mail");
            DropColumn("dbo.SOrders", "ImplementerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOrders", "ImplementerId", c => c.Int());
            AddColumn("dbo.SClients", "Mail", c => c.String());
        }
    }
}
