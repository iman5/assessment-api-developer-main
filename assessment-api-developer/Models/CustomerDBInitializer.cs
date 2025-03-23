using System.Linq;

namespace assessment_platform_developer.Models
{
    public static class CustomerSeeder
    {
        public static void Seed(CustomerDBContext context)
        {
            // Only add seed data if no customers exist.
            if (!context.Customers.Any())
            {
                // Customer 1: Maple Leaf Enterprises (Ontario)
                context.Customers.Add(new Customer
                {
                    Name = "Maple Leaf Enterprises",
                    Address = "456 Maple Road",
                    City = "Toronto",
                    State = ((int)CanadianProvinces.Ontario).ToString(),  
                    Zip = "M5G 2K6",
                    Country = ((int)Countries.Canada).ToString(),
                    Email = "info@mapleleaf.com",
                    Phone = "4165556789",
                    Notes = "Consulting and advisory services.",
                    ContactName = "Robert Johnson",
                    ContactPhone = "4165551122",
                    ContactEmail = "robert.johnson@mapleleaf.com",
                    ContactTitle = "CEO",
                    ContactNotes = "Founder and CEO"
                });

                // Customer 2: Aurora Innovations (Alberta)
                context.Customers.Add(new Customer
                {
                    Name = "Aurora Innovations",
                    Address = "789 Aurora Blvd",
                    City = "Calgary",
                    State = ((int)CanadianProvinces.Alberta).ToString(),
                    Zip = "T2P 3H7",
                    Country = ((int)Countries.Canada).ToString(),
                    Email = "contact@aurorainnovations.ca",
                    Phone = "4035551234",
                    Notes = "Technology and R&D services.",
                    ContactName = "Sarah Thompson",
                    ContactPhone = "4035555678",
                    ContactEmail = "sarah.thompson@aurorainnovations.ca",
                    ContactTitle = "CTO",
                    ContactNotes = "Leads research and product development."
                });

                // Customer 3: Northern Lights Media (British Columbia)
                context.Customers.Add(new Customer
                {
                    Name = "Northern Lights Media",
                    Address = "333 Borealis Ave",
                    City = "Vancouver",
                    State = ((int)CanadianProvinces.BritishColumbia).ToString(),
                    Zip = "V5K 0A1",
                    Country = ((int)Countries.Canada).ToString(),
                    Email = "info@northernlightsmedia.ca",
                    Phone = "6045559876",
                    Notes = "Media and entertainment services.",
                    ContactName = "Mark Lee",
                    ContactPhone = "6045554321",
                    ContactEmail = "mark.lee@northernlightsmedia.ca",
                    ContactTitle = "Managing Director",
                    ContactNotes = "Oversees production and marketing."
                });

                context.SaveChanges();
            }
        }
    }
}
