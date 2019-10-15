namespace dCC_GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<dCC_GroupCapstone.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(dCC_GroupCapstone.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Activities.AddOrUpdate(
                new Models.Activity { Name = "Milwaukee Library", PlaceId = "ChIJTz4iYJ0ZBYgRVz0qCWmDuEY", Description = "Place where I bury Ryan's Body", Price = 3.00 },
                new Models.Activity { Name = "Cool Acquarium Thing", PlaceId = "sadfsdf", Description = "Place where I drowned Ryan", Price = 50.00 }
                );

            context.Interests.AddOrUpdate(
                new Models.Interest { Name = "Murdering Ryan" },
                new Models.Interest { Name = "Silencing Greg" }
                );

            context.Hotels.AddOrUpdate(
                new Models.Hotel { Name = "No Tell Motel", PlaceId = "FDJFLDJFiiejfiwf", Price = 100 }
                );

            context.Customers.AddOrUpdate(
                new Models.Customer { FirstName = "Jake", LastName = "Gambino" }
                );

            context.SaveChanges();
            Models.Hotel hotel = context.Hotels.Where(h => h.Name == "No Tell Motel").FirstOrDefault();
            Models.Customer customer = context.Customers.Where(c => c.FirstName == "Jake" && c.LastName == "Gambino").FirstOrDefault();
            
            customer.SavedVacations.Add(new Models.Vacation() { IsPrivate = false, LocationQueried = "Flurb", Cost = 5.00, SavedHotel = hotel.Id });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
