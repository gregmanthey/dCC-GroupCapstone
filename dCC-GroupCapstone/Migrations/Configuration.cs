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
                new Models.Activity { Name = "Milwaukee Library", PlaceId = "ChIJTz4iYJ0ZBYgRVz0qCWmDuEY", Description = "Milwaukee Public Library", Price = 0.00 },
                new Models.Activity { Name = "Milwaukee Museum", PlaceId = "ChIJ5Q1PzncZBYgR7fctbILQwVs", Description = "Milwaukee Museum", Price = 100.00 },
                new Models.Activity { Name = "The Milwaukee Club", PlaceId = "ChIJw7B-9qcZBYgRAqvpoAMii-U", Description = "The club in Milwaukee", Price = 50 }
                );

            context.Interests.AddOrUpdate(
                new Models.Interest { Name = "Historical Landmarks" },
                new Models.Interest { Name = "Hiking" },
                new Models.Interest { Name = "Science"},
                new Models.Interest { Name = "Religion"}
                );

            context.Hotels.AddOrUpdate(
                new Models.Hotel { Name = "Milwaukee Hilton", PlaceId = "ChIJEyMHFJ4ZBYgR9m-Eb6B_fqw", Price = 100 },
                new Models.Hotel { Name = "Hilton Garden Inn Milwaukee ", PlaceId = "ChIJq6qqqgYZBYgROBdo-2DxlNg", Price = 75}
                );


            context.Customers.AddOrUpdate(
                new Models.Customer { FirstName = "Jake", LastName = "Gambino" },
                new Models.Customer { FirstName = "Greg", LastName = "Manthey"}
                );

            context.Vacations.AddOrUpdate(
                new Models.Vacation { IsPrivate = false, LocationQueried = "Milwaukee", Cost = 200, SavedHotel = 1},
                new Models.Vacation { IsPrivate = false, LocationQueried = "Milwaukee", Cost = 400, SavedHotel = 2}
                );
            
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
