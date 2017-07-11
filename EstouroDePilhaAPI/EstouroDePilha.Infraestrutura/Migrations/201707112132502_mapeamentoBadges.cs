namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mapeamentoBadges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Badge",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BadgeUsuario",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        IdBadge = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdUsuario, t.IdBadge })
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Badge", t => t.IdBadge, cascadeDelete: true)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdBadge);
            
            AddColumn("dbo.Usuario", "Badge_Id", c => c.Int());
            CreateIndex("dbo.Usuario", "Badge_Id");
            AddForeignKey("dbo.Usuario", "Badge_Id", "dbo.Badge", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "Badge_Id", "dbo.Badge");
            DropForeignKey("dbo.BadgeUsuario", "IdBadge", "dbo.Badge");
            DropForeignKey("dbo.BadgeUsuario", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.BadgeUsuario", new[] { "IdBadge" });
            DropIndex("dbo.BadgeUsuario", new[] { "IdUsuario" });
            DropIndex("dbo.Usuario", new[] { "Badge_Id" });
            DropColumn("dbo.Usuario", "Badge_Id");
            DropTable("dbo.BadgeUsuario");
            DropTable("dbo.Badge");
        }
    }
}
