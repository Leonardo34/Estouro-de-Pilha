namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoDoBanco : DbMigration
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
                        IdUsuario = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Repostas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        DataResposta = c.DateTime(nullable: false),
                        IdResposta = c.Guid(nullable: false),
                        IdPergunta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdResposta, cascadeDelete: true)
                .ForeignKey("dbo.Pergunta", t => t.IdPergunta)
                .Index(t => t.IdResposta)
                .Index(t => t.IdPergunta);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Guid(nullable: false),
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
            DropForeignKey("dbo.Repostas", "IdPergunta", "dbo.Pergunta");
            DropForeignKey("dbo.Repostas", "IdResposta", "dbo.Usuario");
            DropForeignKey("dbo.Pergunta", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.TagPergunta", new[] { "IdTag" });
            DropIndex("dbo.TagPergunta", new[] { "IdPergunta" });
            DropIndex("dbo.Repostas", new[] { "IdPergunta" });
            DropIndex("dbo.Repostas", new[] { "IdResposta" });
            DropIndex("dbo.Pergunta", new[] { "IdUsuario" });
            DropTable("dbo.TagPergunta");
            DropTable("dbo.Tag");
            DropTable("dbo.Usuario");
            DropTable("dbo.Repostas");
            DropTable("dbo.Pergunta");
        }
    }
}
