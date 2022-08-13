using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMonitor2.Data;

namespace EventMonitor2.Models
{
    public class CompanyList
    {
        List<Users> users = new List<Users>();
        List<Users> velousers = new List<Users>();
        CompanyLeadership oneCompany = new CompanyLeadership();

        DbConnection _connection = new DbConnection();
        SqlStrings _sqlStrings = new SqlStrings();

        public List<CompanyLeadership> RunLeadership ()
        {
            users = _connection.GetUserList(_sqlStrings._userList);
            oneCompany = new CompanyLeadership();

            List<CompanyLeadership> _tempData = new List<CompanyLeadership>();

            for (int i = 0; i < users.Count; i++)
            {
                int index = _tempData.FindIndex(x => x.Division == users[i].Division);
                oneCompany.Division = users[i].Division;
                if  (index < 0)
                {
                    oneCompany.Division = users[i].Division;
                    oneCompany.Result = users[i].Result;
                    oneCompany.PeopleCount = 1;
                    _tempData.Add(new CompanyLeadership(oneCompany));
                }
                else if (index > -1 || _tempData[index].Result != users[i].Result)
                {
                    _tempData[index].Result += users[i].Result;
                    _tempData[index].PeopleCount += 1;
                }
            }
            _tempData.OrderByDescending(p => p.Result);
            return _tempData;
        }

        public List<CompanyLeadership> VeloLeadership()
        {
            users = _connection.GetVeloUserList(_sqlStrings._userList);
            oneCompany = new CompanyLeadership();

            List<CompanyLeadership> _tempData = new List<CompanyLeadership>();

            for (int i = 0; i < users.Count; i++)
            {
                int index = _tempData.FindIndex(x => x.Division == users[i].Division);
                oneCompany.Division = users[i].Division;
                if (index < 0)
                {
                    oneCompany.Division = users[i].Division;
                    oneCompany.Result = users[i].Result;
                    oneCompany.PeopleCount = 1;
                    _tempData.Add(new CompanyLeadership(oneCompany));
                }
                else if (index > -1 || _tempData[index].Result != users[i].Result)
                {
                    _tempData[index].Result += users[i].Result;
                    _tempData[index].PeopleCount += 1;
                }
            }
            _tempData.OrderByDescending(p => p.Result);
            return _tempData;
        }
    }
}
