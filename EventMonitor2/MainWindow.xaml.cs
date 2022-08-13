using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EventMonitor2.Data;
using EventMonitor2.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.ComponentModel;
using EventMonitor2.Windows;

namespace EventMonitor2
{

    // https://www.cyberforum.ru/wpf-silverlight/thread1065714.html
    // http://wpftutorial.net/Behaviors.html
    // https://de-vraag.com/ru/52903832
    // https://stackoverflow.com/questions/3833536/how-to-perform-single-click-checkbox-selection-in-wpf-datagrid/12244451#12244451
    // https://www.youtube.com/watch?v=zvyQNuuTqks&ab_channel=IAmTimCorey


    ////

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        CompanyLeadership companyLead = new CompanyLeadership();
        Users Users = new Users();
        CompanyList CompanyList = new CompanyList();

        public List<Users> userList = new List<Users>();
        public List<Users> _defaultUserList = new List<Users>();
        public List<Users> _defaultUserVeloList = new List<Users>();
        List<CompanyLeadership> companyRunLeadershipCollection = new List<CompanyLeadership>();
        List<CompanyLeadership> companyVeloLeadershipCollection = new List<CompanyLeadership>();

        public List<string> DivisionList = new List<string>();

        DbConnection _connection = new DbConnection();
        SqlStrings _sqlStrings = new SqlStrings();
        public MainWindow()
        {
            InitializeComponent();

            //GridData.BeginningEdit += (s, ss) => ss.Cancel = true;
        }

        #region блок  загрузки и отображения информации
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            companyRunLeadershipCollection = CompanyList.RunLeadership();
            companyVeloLeadershipCollection = CompanyList.VeloLeadership();

            CompanySupremacyGrid.ItemsSource = companyRunLeadershipCollection.OrderByDescending(c => c.Result);


            DivisionList = _connection.DivisionList(_sqlStrings._divisionList);
            CompanyCmbBox.ItemsSource = DivisionList;
            CompanyCmbBox.SelectedIndex = 0;

            _defaultUserList = _connection.GetUserList(_sqlStrings._userList);
            _defaultUserVeloList = _connection.GetVeloUserList(_sqlStrings._userList);

            _defaultUserList.OrderByDescending(u => u.Result);
            _defaultUserVeloList.OrderByDescending(u => u.Result);

            userList = _connection.GetUserList(_sqlStrings._userList);
            GridData.ItemsSource = userList.OrderByDescending(c => c.Result);

            GenderCmbBox.SelectedIndex = 0;

            if (CompanyCmbBox == null)
            {
                CompanyCmbBox.Text = "Все подразделения";
                CompanyCmbBox.SelectedIndex = 0;
                //while (CompanyCmbBox == null)
                //{
                //    CompanyCmbBox.ItemsSource = DivisionList;
                //    CompanyCmbBox.Items.Refresh();
                //    CompanyCmbBox.SelectedIndex = 0;
                //}
            }
        }

        private void CompanyCmbBox_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void GenderCmbBox_DropDownClosed(object sender, EventArgs e)
        {

            if (ActivityCmbBox.Text == "Бег")
            {
                if (CompanyCmbBox != null)
                {
                    if ((CompanyCmbBox.SelectedIndex < 1 && GenderCmbBox.SelectedIndex < 1))
                    {
                        userList = _defaultUserList;
                    }
                    else if (GenderCmbBox.SelectedIndex > 0 && CompanyCmbBox.SelectedIndex < 1)
                    {
                        userList = _defaultUserList;
                        userList = _defaultUserList.Where(genderFilter).ToList(); // this.userList.Where(genderFilter).ToList();
                    }
                    else if (CompanyCmbBox.SelectedIndex > 0 && GenderCmbBox.SelectedIndex < 1)
                    {
                        userList = _defaultUserList;
                        userList = userList.Where(divisionFilter).ToList();
                    }
                    else if (CompanyCmbBox.SelectedIndex > 0 && GenderCmbBox.SelectedIndex > 0)
                    {
                        userList = _defaultUserList;

                        var one_param_filterList = from p in userList where p.Gender.ToString() == $"{GenderCmbBox.Text}" select p;
                        var FilteredList = from p in one_param_filterList where p.Division == $"{CompanyCmbBox.SelectedItem.ToString()}" select p;
                        userList = FilteredList.ToList();
                    }
                }
                GridData.ItemsSource = userList.OrderByDescending(c => c.Result);
                GridData.Items.Refresh();
                CompanySupremacyGrid.ItemsSource = companyRunLeadershipCollection.OrderByDescending(c => c.Result);
                CompanySupremacyGrid.Items.Refresh();

            }
            else if (ActivityCmbBox.Text == "Велосипед")
            {
                if (CompanyCmbBox != null)
                {
                    if ((CompanyCmbBox.SelectedIndex < 1 && GenderCmbBox.SelectedIndex < 1))
                    {
                        userList = _defaultUserVeloList;
                    }
                    else if (GenderCmbBox.SelectedIndex > 0 && CompanyCmbBox.SelectedIndex < 1)
                    {
                        userList = _defaultUserVeloList;
                        userList = _defaultUserVeloList.Where(genderFilter).ToList(); // this.userList.Where(genderFilter).ToList();
                    }
                    else if (CompanyCmbBox.SelectedIndex > 0 && GenderCmbBox.SelectedIndex < 1)
                    {
                        userList = _defaultUserVeloList;
                        userList = userList.Where(divisionFilter).ToList();
                    }
                    else if (CompanyCmbBox.SelectedIndex > 0 && GenderCmbBox.SelectedIndex > 0)
                    {
                        userList = _defaultUserVeloList;

                        var one_param_filterList = from p in userList where p.Gender.ToString() == $"{GenderCmbBox.Text}" select p;
                        var FilteredList = from p in one_param_filterList where p.Division == $"{CompanyCmbBox.SelectedItem.ToString()}" select p;
                        userList = FilteredList.ToList();
                    }
                }
                GridData.ItemsSource = userList.OrderByDescending(c => c.Result);
                GridData.Items.Refresh();
                CompanySupremacyGrid.ItemsSource = companyVeloLeadershipCollection.OrderByDescending(c => c.Result);
                CompanySupremacyGrid.Items.Refresh();
            }
        }

        private void ActivityCmbBox_DropDownClosed(object sender, EventArgs e)
        {
            if (ActivityCmbBox.Text == "Бег")
            {
                GenderCmbBox.SelectedIndex = 0;
                CompanyCmbBox.SelectedIndex = 0;
                GenderCmbBox_DropDownClosed(sender, e);
            }
            if (ActivityCmbBox.Text == "Велосипед")
            {
                GenderCmbBox.SelectedIndex = 0;
                CompanyCmbBox.SelectedIndex = 0;
                GenderCmbBox_DropDownClosed(sender, e);
            }

        }

        private void GridData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        public bool divisionFilter(Users arg)
        {
            bool result = false;
            if (arg.Division == CompanyCmbBox.SelectedItem.ToString()) result = true;
            return result;
        }

        public bool genderFilter (Users arg)
        {
            bool result = false;
            if (arg.Gender == GenderCmbBox.Text) result = true;
            return result;
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion


        #region Блок редактирования данных
        public bool flagfix = true;

        private void GridData_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            EditCellWindow cellEditindWindow = new EditCellWindow();
            cellEditindWindow.Owner = this;

            cellEditindWindow.Show();

            Users user = e.Row.Item as Users;
            //MessageBox.Show($"{user.Name}, {user.Division}");

            int rowNumber = e.Row.GetIndex();
        }

        private void GridData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Users _editingUser = new Users();
            int RowIndex =  e.Row.GetIndex();

            
        }


        #endregion


    }
}
