using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace NaukaSlowek
{
    /// <summary>
    /// Interaction logic for SelectTest.xaml
    /// </summary>
    /// 

    public partial class Settings : Window
    {
        public enum SettingType
        {
            Parts,
            Categories
        }

        SettingType settingType;
        public Tuple<string, string> selectedTest;
        private DataTable tableTests;

        public Settings(SettingType s)
        {
            InitializeComponent();
            settingType = s;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getSettingValuesList();
            //PrepareGrid();
        }

        private void getSettingValuesList()
        {
            throw new NotImplementedException();
        }

        //private void PrepareGrid()
        //{
        //    DataGridTextColumn column;

        //    tableTests = new DataTable();
        //    GridValues.AutoGenerateColumns = false;
        //    GridValues.ItemsSource = tests;

        //    column = new DataGridTextColumn();
        //    column.Header = "Testy";
        //    column.Width = new DataGridLength(100, DataGridLengthUnitType.Star);
        //    column.IsReadOnly = true;
        //    column.Binding = new Binding("Name");
        //    GridValues.Columns.Add(column);

        //    GridValues.DataContext = tableTests.DefaultView;
        //}

        //private void getTestList()
        //{
        //    Test test;
        //    string path = getLangPath() + @"\";

        //    try
        //    {
        //        DirectoryInfo d = new DirectoryInfo(path);

        //        foreach (FileInfo file in d.GetFiles("*.csv"))
        //        {
        //            test = new Test();
        //            test.FileName = file.Name;

        //            using (StreamReader fs = file.OpenText())
        //            {
        //                test.Name = fs.ReadLine().Split(';')[0];
        //            }

        //            tests.Add(test);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Problem podczas pobierania listy testów:" + Environment.NewLine + ex.Message, "Nauka słówek", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        //{
        //    selectedTest = selectedTestFromList();

        //    if (selectedTest.Item1 != "" && selectedTest.Item2 != "")
        //    {
        //        this.DialogResult = true;
        //        this.Close();
        //    }
        //    else
        //    {
        //        //this.DialogResult = false;
        //    }
        //}

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
