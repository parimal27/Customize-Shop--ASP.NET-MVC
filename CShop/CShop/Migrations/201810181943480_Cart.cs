namespace CShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PartMappings",
                c => new
                    {
                        key = c.Int(nullable: false, identity: true),
                        part_partID = c.Int(),
                        productpartmap_ppId = c.Int(),
                    })
                .PrimaryKey(t => t.key)
                .ForeignKey("dbo.Part", t => t.part_partID)
                .ForeignKey("dbo.ProductPartMap", t => t.productpartmap_ppId)
                .Index(t => t.part_partID)
                .Index(t => t.productpartmap_ppId);
            
            CreateTable(
                "dbo.Part",
                c => new
                    {
                        partID = c.Int(nullable: false),
                        pid = c.Int(nullable: false),
                        desc = c.String(maxLength: 50, unicode: false),
                        image = c.String(maxLength: 50, unicode: false),
                        price = c.Int(),
                        cate = c.String(maxLength: 50, unicode: false),
                        noparts = c.Int(),
                        name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.partID)
                .ForeignKey("dbo.Product", t => t.pid)
                .Index(t => t.pid);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        PId = c.Int(nullable: false),
                        pname = c.String(maxLength: 50, unicode: false),
                        pdsc = c.String(maxLength: 50, unicode: false),
                        pqty = c.Int(),
                        pimage = c.String(maxLength: 50, unicode: false),
                        pcate = c.Int(),
                        price = c.Int(),
                    })
                .PrimaryKey(t => t.PId);
            
            CreateTable(
                "dbo.ProductPartMap",
                c => new
                    {
                        ppId = c.Int(nullable: false),
                        partid1 = c.Int(),
                        partid2 = c.Int(),
                        image = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ppId)
                .ForeignKey("dbo.Part", t => t.partid1)
                .ForeignKey("dbo.Part", t => t.partid2)
                .Index(t => t.partid1)
                .Index(t => t.partid2);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartMappings", "productpartmap_ppId", "dbo.ProductPartMap");
            DropForeignKey("dbo.PartMappings", "part_partID", "dbo.Part");
            DropForeignKey("dbo.ProductPartMap", "partid2", "dbo.Part");
            DropForeignKey("dbo.ProductPartMap", "partid1", "dbo.Part");
            DropForeignKey("dbo.Part", "pid", "dbo.Product");
            DropIndex("dbo.ProductPartMap", new[] { "partid2" });
            DropIndex("dbo.ProductPartMap", new[] { "partid1" });
            DropIndex("dbo.Part", new[] { "pid" });
            DropIndex("dbo.PartMappings", new[] { "productpartmap_ppId" });
            DropIndex("dbo.PartMappings", new[] { "part_partID" });
            DropTable("dbo.ProductPartMap");
            DropTable("dbo.Product");
            DropTable("dbo.Part");
            DropTable("dbo.PartMappings");
        }
    }
}
