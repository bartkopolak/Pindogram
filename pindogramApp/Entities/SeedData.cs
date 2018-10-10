using System.Collections.Generic;
using System.Linq;

namespace pindogramApp.Entities
{
    /// <summary>
    /// Static class to fill empty databse with initial data
    /// </summary>
    public static class SeedData
    {
        public static void SetData(this PindogramDataContext context)
        {
            if (context.Groups.Any())
            {
                return;
            }

            var groups = new List<Group>
            {
                new Group
                {
                    Name = "ADMIN"
                },
                new Group
                {
                    Name = "USER"
                }
            };

            context.AddRange(groups);

            context.SaveChanges();
        }
    }
}
