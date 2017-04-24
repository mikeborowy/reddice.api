namespace RedDice.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAutoIncrement : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Event");
            AlterColumn("dbo.Event", "EventId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Event", "EventId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Event");
            AlterColumn("dbo.Event", "EventId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Event", "EventId");
        }
    }
}
