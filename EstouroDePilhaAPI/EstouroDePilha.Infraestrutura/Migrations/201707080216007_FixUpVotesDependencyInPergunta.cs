namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUpVotesDependencyInPergunta : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.UpVoteResposta", new[] { "IdResposta" });
            //DropIndex("dbo.UpVoteResposta", new[] { "Resposta_Id1" });
            //DropColumn("dbo.UpVoteResposta", "IdResposta");
            //RenameColumn(table: "dbo.UpVoteResposta", name: "Resposta_Id1", newName: "IdResposta");
            //AlterColumn("dbo.UpVoteResposta", "IdResposta", c => c.Int(nullable: false));
            //CreateIndex("dbo.UpVoteResposta", "IdResposta");
        }
        
        public override void Down()
        {
            //DropIndex("dbo.UpVoteResposta", new[] { "IdResposta" });
            //AlterColumn("dbo.UpVoteResposta", "IdResposta", c => c.Int());
            //RenameColumn(table: "dbo.UpVoteResposta", name: "IdResposta", newName: "Resposta_Id1");
            //AddColumn("dbo.UpVoteResposta", "IdResposta", c => c.Int(nullable: false));
            //CreateIndex("dbo.UpVoteResposta", "Resposta_Id1");
            //CreateIndex("dbo.UpVoteResposta", "IdResposta");
        }
    }
}
