namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixbuf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Repostas", "IdPergunta", "dbo.Pergunta");
            DropForeignKey("dbo.UpVoteResposta", "IdResposta", "dbo.Repostas");
            AddForeignKey("dbo.Repostas", "IdPergunta", "dbo.Pergunta", "Id");
            AddForeignKey("dbo.UpVoteResposta", "IdResposta", "dbo.Repostas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UpVoteResposta", "IdResposta", "dbo.Repostas");
            DropForeignKey("dbo.Repostas", "IdPergunta", "dbo.Pergunta");
            AddForeignKey("dbo.UpVoteResposta", "IdResposta", "dbo.Repostas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Repostas", "IdPergunta", "dbo.Pergunta", "Id", cascadeDelete: true);
        }
    }
}
