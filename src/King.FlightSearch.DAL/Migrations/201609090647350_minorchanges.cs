namespace King.FlightSearch.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchEntries", "ReturnDate", c => c.DateTime());
            DropColumn("dbo.SearchEntries", "ArrivalDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SearchEntries", "ArrivalDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.SearchEntries", "ReturnDate");
        }
    }
}
