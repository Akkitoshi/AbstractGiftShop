namespace AbstractShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirtsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GiftMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GiftId = c.Int(nullable: false),
                        MaterialsId = c.Int(nullable: false),
                        MaterialsName = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialsId, cascadeDelete: true)
                .Index(t => t.MaterialsId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialsName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SStockId = c.Int(nullable: false),
                        MaterialsId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialsId, cascadeDelete: true)
                .ForeignKey("dbo.SStocks", t => t.SStockId, cascadeDelete: true)
                .Index(t => t.SStockId)
                .Index(t => t.MaterialsId);
            
            CreateTable(
                "dbo.SStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SStockName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GiftName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SClientId = c.Int(nullable: false),
                        GiftId = c.Int(nullable: false),
                        ImplementerId = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gifts", t => t.GiftId, cascadeDelete: true)
                .ForeignKey("dbo.Implementers", t => t.ImplementerId)
                .ForeignKey("dbo.SClients", t => t.SClientId, cascadeDelete: true)
                .Index(t => t.SClientId)
                .Index(t => t.GiftId)
                .Index(t => t.ImplementerId);
            
            CreateTable(
                "dbo.Implementers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImplementerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SClientFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOrders", "SClientId", "dbo.SClients");
            DropForeignKey("dbo.SOrders", "ImplementerId", "dbo.Implementers");
            DropForeignKey("dbo.SOrders", "GiftId", "dbo.Gifts");
            DropForeignKey("dbo.StockMaterials", "SStockId", "dbo.SStocks");
            DropForeignKey("dbo.StockMaterials", "MaterialsId", "dbo.Materials");
            DropForeignKey("dbo.GiftMaterials", "MaterialsId", "dbo.Materials");
            DropIndex("dbo.SOrders", new[] { "ImplementerId" });
            DropIndex("dbo.SOrders", new[] { "GiftId" });
            DropIndex("dbo.SOrders", new[] { "SClientId" });
            DropIndex("dbo.StockMaterials", new[] { "MaterialsId" });
            DropIndex("dbo.StockMaterials", new[] { "SStockId" });
            DropIndex("dbo.GiftMaterials", new[] { "MaterialsId" });
            DropTable("dbo.SClients");
            DropTable("dbo.Implementers");
            DropTable("dbo.SOrders");
            DropTable("dbo.Gifts");
            DropTable("dbo.SStocks");
            DropTable("dbo.StockMaterials");
            DropTable("dbo.Materials");
            DropTable("dbo.GiftMaterials");
        }
    }
}
