using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DontDrownAPI
{
    public static class StaticValues
    {
        public static Dictionary<string, int> userCookies = new Dictionary<string, int>();
        public static string sqlConString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DontDrown;Integrated Security=True;MultipleActiveResultSets=True";
    }
}
