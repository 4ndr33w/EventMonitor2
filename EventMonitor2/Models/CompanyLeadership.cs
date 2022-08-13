using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor2.Models
{
    public class CompanyLeadership
    {
        private int _position;
        private string _division;
        private decimal _result;
        private int _peopleCount;

        public CompanyLeadership(CompanyLeadership company)
        {
            this._position = company._position;
            this._division = company._division;
            this._result = company._result;
            this._peopleCount = company._peopleCount;
        }
        public CompanyLeadership()
        {
            this._position = default;
            this._division = default;
            this._result = default;
            this._peopleCount = default;
        }

        public int Position { get { return this._position; } set { _position = value; } }
        public decimal Result { get { return this._result; } set { _result = value; } }
        public string Division { get { return this._division; } set { _division = value; } }
        public int PeopleCount { get { return this._peopleCount; } 
            set
            {
                _peopleCount = value;
            } }
    }
}
