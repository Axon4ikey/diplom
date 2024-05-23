using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagamentCompanyDiplom.ModelBD;

namespace ManagamentCompanyDiplom.Classes
{
    public static class AppData
    {
        public static DiplomJobEntities db = new DiplomJobEntities();
    }
}
