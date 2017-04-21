using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tester.Data;

namespace tester.Data
{
    public class DbInitializer
    {
        public static void Initialize(ProjectContext context)
        {
            context.Database.EnsureCreated();

            context.SaveChangesAsync();
        }
    }
}
