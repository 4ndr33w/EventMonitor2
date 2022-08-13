using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;
using System.Windows;

namespace EventMonitor2.Data
{
    internal class DbConnection
    {
        public bool SqlConnectionState_Check(MySql.Data.MySqlClient.MySqlConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            else return false;
        }

        string divisionTable = Properties.Resources.DivisionList;
        string UserListTable = Properties.Resources.UserListTableName;

        public static MySqlConnectionStringBuilder ConnStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = Properties.Resources.SqlServer,
            Database = Properties.Resources.DataBase,
            UserID = Properties.Resources.DataBase,
            Password = Properties.Resources.Password
        };

        public MySqlConnection sqlConnection = new MySqlConnection(ConnStringBuilder.ConnectionString);

        public List<string> DivisionList(string sqlString)
        {
            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet();
            List<string> _divisions = new List<string>();

            try
            {
                sqlConnection.Open();
                while(SqlConnectionState_Check(sqlConnection) == false)
                {
                    SqlConnectionState_Check(sqlConnection);
                    Thread.Sleep(200);
                }

                MySqlCommand command = new MySqlCommand(sqlString, sqlConnection);
                MySqlDataAdapter _adapter = new MySqlDataAdapter(command);

                _adapter.Fill(dataSet, divisionTable);
                dataTable = dataSet.Tables[divisionTable];
                sqlConnection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("проблемы с подключение\nПри попытке скачать список подразделений");
                return null;
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string division = dataTable.Rows[i]["DivisionName"].ToString();
                _divisions.Add(division);
            }           
            return _divisions;
        }

        public List<Models.Users> GetUserList(string sqlString)
        {
            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet();
            List<Models.Users> _userList = new List<Models.Users>();

            try
            {
                sqlConnection.Open();
                while (SqlConnectionState_Check(sqlConnection) == false)
                {
                    SqlConnectionState_Check(sqlConnection);
                    Thread.Sleep(200);
                }

                MySqlCommand command = new MySqlCommand(sqlString, sqlConnection);
                MySqlDataAdapter _adapter = new MySqlDataAdapter(command);

                _adapter.Fill(dataSet, UserListTable);
                dataTable = dataSet.Tables[UserListTable];
                sqlConnection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("проблемы с подключение\nПри попытке скачать таблицу результатов");
                return null;
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Models.Users _user = new Models.Users
                {
                    Name = dataTable.Rows[i]["name"].ToString(),
                    Id = Convert.ToInt64(dataTable.Rows[i]["id"]),
                    Division = dataTable.Rows[i]["company"].ToString(),
                    Result = Convert.ToDecimal(dataTable.Rows[i]["result"]),
                    Age = Convert.ToInt32(dataTable.Rows[i]["age"]),
                    Gender = dataTable.Rows[i]["gender"].ToString(),
                    CreatedAt = Convert.ToDateTime(dataTable.Rows[i]["createdAt"]),
                    UpdatedAt = Convert.ToDateTime(dataTable.Rows[i]["updatedAt"]),
                    Position = 0
                };

                _userList.Add(_user);

            }

            return _userList;
        }

        public List<Models.Users> GetVeloUserList(string sqlString)
        {
            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet();
            List<Models.Users> _userList = new List<Models.Users>();

            try
            {
                sqlConnection.Open();
                while (SqlConnectionState_Check(sqlConnection) == false)
                {
                    SqlConnectionState_Check(sqlConnection);
                    Thread.Sleep(200);
                }

                MySqlCommand command = new MySqlCommand(sqlString, sqlConnection);
                MySqlDataAdapter _adapter = new MySqlDataAdapter(command);

                _adapter.Fill(dataSet, UserListTable);
                dataTable = dataSet.Tables[UserListTable];
                sqlConnection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("проблемы с подключение\nПри попытке скачать таблицу результатов");
                return null;
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Models.Users _user = new Models.Users
                {
                    Name = dataTable.Rows[i]["name"].ToString(),
                    Id = Convert.ToInt64(dataTable.Rows[i]["id"]),
                    Division = dataTable.Rows[i]["company"].ToString(),
                    Result = Convert.ToDecimal(dataTable.Rows[i]["veloresult"]), // veloresult // "veloresult"
                    //Veloresult = Convert.ToDecimal(dataTable.Rows[i]["velorresult"]),
                    Age = Convert.ToInt32(dataTable.Rows[i]["age"]),
                    Gender = dataTable.Rows[i]["gender"].ToString(),
                    CreatedAt = Convert.ToDateTime(dataTable.Rows[i]["createdAt"]),
                    UpdatedAt = Convert.ToDateTime(dataTable.Rows[i]["updatedAt"]),
                    Position = 0
                };

                _userList.Add(_user);

            }

            return _userList; //.OrderByDescending(u => u.Veloresult);
        }


    }
}
