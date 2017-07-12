namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicaoComentariosResposta2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComentarioResposta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        DataComentario = c.DateTime(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        IdResposta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Repostas", t => t.IdResposta, cascadeDelete: false)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdResposta);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComentarioResposta", "IdResposta", "dbo.Repostas");
            DropForeignKey("dbo.ComentarioResposta", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.ComentarioResposta", new[] { "IdResposta" });
            DropIndex("dbo.ComentarioResposta", new[] { "IdUsuario" });
            DropTable("dbo.ComentarioResposta");
        }
    }
}
