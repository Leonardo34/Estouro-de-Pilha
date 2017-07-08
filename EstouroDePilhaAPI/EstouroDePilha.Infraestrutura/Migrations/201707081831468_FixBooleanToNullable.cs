namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixBooleanToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Repostas", "EhRespostaCorreta", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Repostas", "EhRespostaCorreta", c => c.Boolean(nullable: false));
        }
    }
}
