namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarMapeamentoDeComentariosPergunta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ComentarioPerguntas", "Pergunta_Id", "dbo.Pergunta");
            DropForeignKey("dbo.ComentarioPerguntas", "Usuario_Id", "dbo.Usuario");
            DropIndex("dbo.ComentarioPerguntas", new[] { "Pergunta_Id" });
            DropIndex("dbo.ComentarioPerguntas", new[] { "Usuario_Id" });
            RenameColumn(table: "dbo.ComentarioPerguntas", name: "Pergunta_Id", newName: "IdPergunta");
            RenameColumn(table: "dbo.ComentarioPerguntas", name: "Usuario_Id", newName: "IdUsuario");
            AlterColumn("dbo.ComentarioPerguntas", "IdPergunta", c => c.Int(nullable: false));
            AlterColumn("dbo.ComentarioPerguntas", "IdUsuario", c => c.Int(nullable: false));
            CreateIndex("dbo.ComentarioPerguntas", "IdUsuario");
            CreateIndex("dbo.ComentarioPerguntas", "IdPergunta");
            AddForeignKey("dbo.ComentarioPerguntas", "IdPergunta", "dbo.Pergunta", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ComentarioPerguntas", "IdUsuario", "dbo.Usuario", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComentarioPerguntas", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.ComentarioPerguntas", "IdPergunta", "dbo.Pergunta");
            DropIndex("dbo.ComentarioPerguntas", new[] { "IdPergunta" });
            DropIndex("dbo.ComentarioPerguntas", new[] { "IdUsuario" });
            AlterColumn("dbo.ComentarioPerguntas", "IdUsuario", c => c.Int());
            AlterColumn("dbo.ComentarioPerguntas", "IdPergunta", c => c.Int());
            RenameColumn(table: "dbo.ComentarioPerguntas", name: "IdUsuario", newName: "Usuario_Id");
            RenameColumn(table: "dbo.ComentarioPerguntas", name: "IdPergunta", newName: "Pergunta_Id");
            CreateIndex("dbo.ComentarioPerguntas", "Usuario_Id");
            CreateIndex("dbo.ComentarioPerguntas", "Pergunta_Id");
            AddForeignKey("dbo.ComentarioPerguntas", "Usuario_Id", "dbo.Usuario", "Id");
            AddForeignKey("dbo.ComentarioPerguntas", "Pergunta_Id", "dbo.Pergunta", "Id");
        }
    }
}
