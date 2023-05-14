using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL.Service
{
    public class EmployeeLeaveRepository : IEmployeeLeaveRepository
    {
        private readonly PortalContext db;

        public EmployeeLeaveRepository(PortalContext db)
        {
            this.db = db;
        }
        public int GetEmployeeId(string EmployeeEmail)
        {
            var v = db.Employees.Where(m => m.Email == EmployeeEmail).FirstOrDefault();
            if(v != null)

            return v.Id;
            else
                return 0;
        }
    }
}
