namespace King.FlightSearch.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlightEntryChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlightEntries", "AirportFromId", "dbo.Airports");
            DropForeignKey("dbo.FlightEntries", "AirportToId", "dbo.Airports");
            DropIndex("dbo.FlightEntries", new[] { "AirportFromId" });
            DropIndex("dbo.FlightEntries", new[] { "AirportToId" });
            CreateTable(
                "dbo.SearchEntries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AirportFromId = c.Guid(nullable: false),
                        AirportToId = c.Guid(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        ArrivalDate = c.DateTime(nullable: false),
                        AdultsCount = c.Int(nullable: false),
                        ChildrenCount = c.Int(nullable: false),
                        Currency = c.Int(nullable: false),
                        SearchEntryHash = c.String(maxLength: 255),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airports", t => t.AirportFromId)
                .ForeignKey("dbo.Airports", t => t.AirportToId)
                .Index(t => t.AirportFromId)
                .Index(t => t.AirportToId)
                .Index(t => t.SearchEntryHash);
            
            AddColumn("dbo.FlightEntries", "SearchEntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.FlightEntries", "DepartureTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.FlightEntries", "ArrivalTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.FlightEntries", "SearchEntryId");
            AddForeignKey("dbo.FlightEntries", "SearchEntryId", "dbo.SearchEntries", "Id");
            DropColumn("dbo.FlightEntries", "AirportFromId");
            DropColumn("dbo.FlightEntries", "AirportToId");
            DropColumn("dbo.FlightEntries", "DepartureDate");
            DropColumn("dbo.FlightEntries", "ArrivalDate");
            DropColumn("dbo.FlightEntries", "PassengersCount");
            DropColumn("dbo.FlightEntries", "Currency");
            DropColumn("dbo.FlightEntries", "SearchEntryHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FlightEntries", "SearchEntryHash", c => c.String(maxLength: 255));
            AddColumn("dbo.FlightEntries", "Currency", c => c.Int(nullable: false));
            AddColumn("dbo.FlightEntries", "PassengersCount", c => c.Int(nullable: false));
            AddColumn("dbo.FlightEntries", "ArrivalDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FlightEntries", "DepartureDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FlightEntries", "AirportToId", c => c.Guid(nullable: false));
            AddColumn("dbo.FlightEntries", "AirportFromId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.FlightEntries", "SearchEntryId", "dbo.SearchEntries");
            DropForeignKey("dbo.SearchEntries", "AirportToId", "dbo.Airports");
            DropForeignKey("dbo.SearchEntries", "AirportFromId", "dbo.Airports");
            DropIndex("dbo.SearchEntries", new[] { "AirportToId" });
            DropIndex("dbo.SearchEntries", new[] { "AirportFromId" });
            DropIndex("dbo.FlightEntries", new[] { "SearchEntryId" });
            DropColumn("dbo.FlightEntries", "ArrivalTime");
            DropColumn("dbo.FlightEntries", "DepartureTime");
            DropColumn("dbo.FlightEntries", "SearchEntryId");
            DropTable("dbo.SearchEntries");
            CreateIndex("dbo.FlightEntries", "AirportToId");
            CreateIndex("dbo.FlightEntries", "AirportFromId");
            AddForeignKey("dbo.FlightEntries", "AirportToId", "dbo.Airports", "Id");
            AddForeignKey("dbo.FlightEntries", "AirportFromId", "dbo.Airports", "Id");
        }
    }
}
