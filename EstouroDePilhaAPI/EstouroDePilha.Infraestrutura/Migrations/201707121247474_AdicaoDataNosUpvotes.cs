namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicaoDataNosUpvotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DownVotePerguntas", "Data", c => c.DateTime(nullable: false));
            AddColumn("dbo.DownVoteResposta", "Data", c => c.DateTime(nullable: false));
            AddColumn("dbo.UpVoteResposta", "Data", c => c.DateTime(nullable: false));
            AddColumn("dbo.UpVotePerguntas", "Data", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UpVotePerguntas", "Data");
            DropColumn("dbo.UpVoteResposta", "Data");
            DropColumn("dbo.DownVoteResposta", "Data");
            DropColumn("dbo.DownVotePerguntas", "Data");
        }
    }
}
