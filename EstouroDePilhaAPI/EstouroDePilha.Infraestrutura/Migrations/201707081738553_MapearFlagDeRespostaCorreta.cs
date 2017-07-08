namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MapearFlagDeRespostaCorreta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Repostas", "EhRespostaCorreta", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Repostas", "EhRespostaCorreta");
        }
    }
}
