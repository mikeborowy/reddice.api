namespace RedDice.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Event");
        }
    }
}
