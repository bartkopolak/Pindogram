using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pindogramApp.Model
{
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
