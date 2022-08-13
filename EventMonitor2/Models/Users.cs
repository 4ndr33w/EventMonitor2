using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitor2.Models
{
    public class Users
    {
        private int _position;
        private long _id;
        private string _name;
        private string _division;
        private string _gender;
        private int _age;
        private DateTime _createdAt;
        private DateTime _updatedAt;
        private decimal _result;
        private decimal _veloresult;

        public int Position { get { return _position; } set { _position = value; } }
        public long Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Gender { get { return _gender; } set { _gender = value; } }
        public int Age { get { return _age; } set { _age = value; } }
        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = value; } }
        public decimal Result { get { return _result; } set { _result = value; } }
        public decimal Veloresult { get { return _veloresult; } set { _veloresult = value; } }
        public string Division { get { return _division; } set {  _division = value; } }
        public DateTime UpdatedAt { get { return _updatedAt; } set { _updatedAt = value; } }

        public Users (Users user)
        {
            this._position = user.Position;
            this._id = user.Id;
            this._name = user.Name;
            this._gender = user.Gender;
            this._age = user.Age;
            this._createdAt = user.CreatedAt;
            this._updatedAt = user.UpdatedAt;
            this._result = user.Result;
            this._veloresult = user.Veloresult;
            this._division = user.Division;
        }
        public Users()
        {
            this._position = default;
            this._id = default;
            this._name = default;
            this._gender = default;
            this._age = default;
            this._createdAt = default;
            this._updatedAt = default;
            this._result = default;
            this._veloresult = default;
            this._division = default;
        }


    }
}
