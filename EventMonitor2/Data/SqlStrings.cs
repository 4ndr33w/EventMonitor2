using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor2.Data
{
    internal class SqlStrings
    {
        public readonly string _userList = $"SELECT * FROM {Properties.Resources.UserListTableName}";
        public readonly string _divisionList = $"SELECT DivisionName FROM {Properties.Resources.DivisionList}";

    }
}
