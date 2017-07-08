namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MapearDownVotesNaResposta : DbMigration
    {
        public override void Up()
        {
            /*
            DropForeignKey("dbo.DownVoteResposta", "IdResposta", "dbo.Repostas");
            DropForeignKey("dbo.DownVoteResposta", "Resposta_Id", "dbo.Repostas");
            DropIndex("dbo.DownVoteResposta", new[] { "Resposta_Id" });
            DropIndex("dbo.DownVoteResposta", new[] { "IdResposta" });
            DropColumn("dbo.DownVoteResposta", "IdResposta");
            RenameColumn(table: "dbo.DownVoteResposta", name: "Resposta_Id", newName: "IdResposta");
            AlterColumn("dbo.DownVoteResposta", "IdResposta", c => c.Int(nullable: false));
            CreateIndex("dbo.DownVoteResposta", "IdResposta");
            AddForeignKey("dbo.DownVoteResposta", "IdResposta", "dbo.Repostas", "Id", cascadeDelete: true);
            */
        }
        
        public override void Down()
        {
            /*
            DropForeignKey("dbo.DownVoteResposta", "IdResposta", "dbo.Repostas");
            DropIndex("dbo.DownVoteResposta", new[] { "IdResposta" });
            AlterColumn("dbo.DownVoteResposta", "IdResposta", c => c.Int());
            RenameColumn(table: "dbo.DownVoteResposta", name: "IdResposta", newName: "Resposta_Id");
            AddColumn("dbo.DownVoteResposta", "IdResposta", c => c.Int(nullable: false));
            CreateIndex("dbo.DownVoteResposta", "IdResposta");
            CreateIndex("dbo.DownVoteResposta", "Resposta_Id");
            AddForeignKey("dbo.DownVoteResposta", "Resposta_Id", "dbo.Repostas", "Id");
            AddForeignKey("dbo.DownVoteResposta", "IdResposta", "dbo.Repostas", "Id");
            */
        }
    }
}
