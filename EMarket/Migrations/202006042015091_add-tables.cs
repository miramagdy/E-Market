namespace EMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        added_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        number_of_products = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "product_id", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Carts", new[] { "product_id" });
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
