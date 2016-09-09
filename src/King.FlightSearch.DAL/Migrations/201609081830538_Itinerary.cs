namespace King.FlightSearch.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Itinerary : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlightEntries", "SearchEntryId", "dbo.SearchEntries");
            DropIndex("dbo.FlightEntries", new[] { "SearchEntryId" });
            CreateTable(
                "dbo.ItineraryEntries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SearchEntryId = c.Guid(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SearchEntries", t => t.SearchEntryId)
                .Index(t => t.SearchEntryId);
            
            AddColumn("dbo.FlightEntries", "ItineraryEntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.FlightEntries", "Type", c => c.Int(nullable: false));
            CreateIndex("dbo.FlightEntries", "ItineraryEntryId");
            AddForeignKey("dbo.FlightEntries", "ItineraryEntryId", "dbo.ItineraryEntries", "Id");
            DropColumn("dbo.FlightEntries", "SearchEntryId");
            DropColumn("dbo.FlightEntries", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FlightEntries", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.FlightEntries", "SearchEntryId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.FlightEntries", "ItineraryEntryId", "dbo.ItineraryEntries");
            DropForeignKey("dbo.ItineraryEntries", "SearchEntryId", "dbo.SearchEntries");
            DropIndex("dbo.ItineraryEntries", new[] { "SearchEntryId" });
            DropIndex("dbo.FlightEntries", new[] { "ItineraryEntryId" });
            DropColumn("dbo.FlightEntries", "Type");
            DropColumn("dbo.FlightEntries", "ItineraryEntryId");
            DropTable("dbo.ItineraryEntries");
            CreateIndex("dbo.FlightEntries", "SearchEntryId");
            AddForeignKey("dbo.FlightEntries", "SearchEntryId", "dbo.SearchEntries", "Id");
        }
    }
}
