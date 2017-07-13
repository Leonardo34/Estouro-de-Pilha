namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixBugbu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComentarioPerguntas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        DataComentario = c.DateTime(nullable: false),
                        Pergunta_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pergunta", t => t.Pergunta_Id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id)
                .Index(t => t.Pergunta_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComentarioPerguntas", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.ComentarioPerguntas", "Pergunta_Id", "dbo.Pergunta");
            DropIndex("dbo.ComentarioPerguntas", new[] { "Usuario_Id" });
            DropIndex("dbo.ComentarioPerguntas", new[] { "Pergunta_Id" });
            DropTable("dbo.ComentarioPerguntas");
        }
    }
}
