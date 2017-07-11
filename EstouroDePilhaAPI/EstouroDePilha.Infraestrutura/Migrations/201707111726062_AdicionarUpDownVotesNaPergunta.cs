namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarUpDownVotesNaPergunta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DownVotePerguntas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPergunta = c.Int(nullable: false),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pergunta", t => t.IdPergunta, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id)
                .Index(t => t.IdPergunta)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.UpVotePerguntas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usuario_Id = c.Int(),
                        IdPergunta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id)
                .ForeignKey("dbo.Pergunta", t => t.IdPergunta, cascadeDelete: true)
                .Index(t => t.Usuario_Id)
                .Index(t => t.IdPergunta);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DownVotePerguntas", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.UpVotePerguntas", "IdPergunta", "dbo.Pergunta");
            DropForeignKey("dbo.UpVotePerguntas", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.DownVotePerguntas", "IdPergunta", "dbo.Pergunta");
            DropIndex("dbo.UpVotePerguntas", new[] { "IdPergunta" });
            DropIndex("dbo.UpVotePerguntas", new[] { "Usuario_Id" });
            DropIndex("dbo.DownVotePerguntas", new[] { "Usuario_Id" });
            DropIndex("dbo.DownVotePerguntas", new[] { "IdPergunta" });
            DropTable("dbo.UpVotePerguntas");
            DropTable("dbo.DownVotePerguntas");
        }
    }
}
