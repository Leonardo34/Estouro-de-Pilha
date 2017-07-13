namespace EstouroDePilha.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarCampoTipoNaBadge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Badge", "Tipo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Badge", "Tipo");
        }
    }
}
