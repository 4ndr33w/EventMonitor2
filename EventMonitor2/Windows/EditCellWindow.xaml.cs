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
using System.Windows.Shapes;

namespace EventMonitor2.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditCellWindow.xaml
    /// </summary>
    public partial class EditCellWindow : Window
    {
        public EditCellWindow()
        {
            InitializeComponent();
        }

        public string EditingResult;

        private void EditCellOkButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Form1 = this.Owner as MainWindow;

            if (EditCellTextBox.Text == string.Empty)
            {
                EditCellCancelButton_Click(sender, e);
            }
            else
            {
                EditingResult = EditCellTextBox.Text;
            }
        }

        private void EditCellCancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Form1 = this.Owner as MainWindow;

            Form1.flagfix = false;
            Form1.GridData.CancelEdit();
            Form1.GridData.CancelEdit();
            Form1.flagfix = false;
        }
    }
}
