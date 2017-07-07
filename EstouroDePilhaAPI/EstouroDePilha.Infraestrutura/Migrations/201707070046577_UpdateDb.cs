using System;
using System.Data.Entity.Migrations;

namespace EstouroDePilha.Infraestrutura.Migrations
{
  
    public partial class UpdateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pergunta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        DataPergunta = c.DateTime(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        Tag_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.Tag_Id)
                .Index(t => t.IdUsuario)
                .Index(t => t.Tag_Id);
            
            CreateTable(
                "dbo.Repostas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        DataResposta = c.DateTime(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        IdPergunta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Pergunta", t => t.IdPergunta)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdPergunta);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Endereco = c.String(),
                        Descricao = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                        UrlFotoPerfil = c.String(),
                        Senha = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagPergunta",
                c => new
                    {
                        IdPergunta = c.Int(nullable: false),
                        IdTag = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPergunta, t.IdTag })
                .ForeignKey("dbo.Pergunta", t => t.IdPergunta, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.IdTag, cascadeDelete: true)
                .Index(t => t.IdPergunta)
                .Index(t => t.IdTag);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPergunta", "IdTag", "dbo.Tag");
            DropForeignKey("dbo.TagPergunta", "IdPergunta", "dbo.Pergunta");
            DropForeignKey("dbo.Pergunta", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.Repostas", "IdPergunta", "dbo.Pergunta");
            DropForeignKey("dbo.Repostas", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Pergunta", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.TagPergunta", new[] { "IdTag" });
            DropIndex("dbo.TagPergunta", new[] { "IdPergunta" });
            DropIndex("dbo.Repostas", new[] { "IdPergunta" });
            DropIndex("dbo.Repostas", new[] { "IdUsuario" });
            DropIndex("dbo.Pergunta", new[] { "Tag_Id" });
            DropIndex("dbo.Pergunta", new[] { "IdUsuario" });
            DropTable("dbo.TagPergunta");
            DropTable("dbo.Tag");
            DropTable("dbo.Usuario");
            DropTable("dbo.Repostas");
            DropTable("dbo.Pergunta");
        }
    }
}
