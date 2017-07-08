namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarVotosEmRespostas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DownVoteResposta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Resposta_Id = c.Int(),
                        IdResposta = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Repostas", t => t.Resposta_Id)
                .ForeignKey("dbo.Repostas", t => t.IdResposta)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .Index(t => t.Resposta_Id)
                .Index(t => t.IdResposta)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.UpVoteResposta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdResposta = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        Resposta_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Repostas", t => t.IdResposta)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .ForeignKey("dbo.Repostas", t => t.Resposta_Id1)
                .Index(t => t.IdResposta)
                .Index(t => t.IdUsuario)
                .Index(t => t.Resposta_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DownVoteResposta", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.DownVoteResposta", "IdResposta", "dbo.Repostas");
            DropForeignKey("dbo.UpVoteResposta", "Resposta_Id1", "dbo.Repostas");
            DropForeignKey("dbo.UpVoteResposta", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.UpVoteResposta", "IdResposta", "dbo.Repostas");
            DropForeignKey("dbo.DownVoteResposta", "Resposta_Id", "dbo.Repostas");
            DropIndex("dbo.UpVoteResposta", new[] { "Resposta_Id1" });
            DropIndex("dbo.UpVoteResposta", new[] { "IdUsuario" });
            DropIndex("dbo.UpVoteResposta", new[] { "IdResposta" });
            DropIndex("dbo.DownVoteResposta", new[] { "IdUsuario" });
            DropIndex("dbo.DownVoteResposta", new[] { "IdResposta" });
            DropIndex("dbo.DownVoteResposta", new[] { "Resposta_Id" });
            DropTable("dbo.UpVoteResposta");
            DropTable("dbo.DownVoteResposta");
        }
    }
}
