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

    public class Test
    {
        public string Name { get; set; }
        public string FileName { get; set; }
    }

    public partial class SelectTest : Window
    {
        public Tuple<string, string> selectedTest;
        private List<Test> tests = new List<Test>();
        private DataTable tableTests;

        public SelectTest()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getTestList();
            PrepareGrid();
        }

        private void PrepareGrid()
        {
            DataGridTextColumn column;

            tableTests = new DataTable();
            GridTests.AutoGenerateColumns = false;
            GridTests.ItemsSource = tests;

            column = new DataGridTextColumn();
            column.Header = "Testy";
            column.Width = new DataGridLength(100, DataGridLengthUnitType.Star);
            column.IsReadOnly = true;
            column.Binding = new Binding("Name");
            GridTests.Columns.Add(column);

            GridTests.DataContext = tableTests.DefaultView;
        }

        private void getTestList()
        {
            Test test;
            string path = getLangPath() + @"\";

            try
            {
                DirectoryInfo d = new DirectoryInfo(path);

                foreach (FileInfo file in d.GetFiles("*.csv"))
                {
                    test = new Test();
                    test.FileName = file.Name;

                    using (StreamReader fs = file.OpenText())
                    {
                        test.Name = fs.ReadLine().Split(';')[0];
                    }

                    tests.Add(test);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem podczas pobierania listy testów:" + Environment.NewLine + ex.Message, "Nauka słówek", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            selectedTest = selectedTestFromList();

            if (selectedTest.Item1 != "" && selectedTest.Item2 != "")
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                //this.DialogResult = false;
            }
        }

        private Tuple<string, string> selectedTestFromList()
        {
            Tuple<string, string> res;

            int i = GridTests.SelectedIndex;
            if (i < tests.Count)
                res = new Tuple<string, string>(tests[i].Name, tests[i].FileName);
            else
                res = new Tuple<string, string>("", "");

            return res;
        }

        public string getLangPath()
        {
            switch (MainWindow.mw.lang)
            {
                case MainWindow.Lang.English:
                    return "Eng";
                case MainWindow.Lang.French:
                    return "Fre";
                case MainWindow.Lang.Latin:
                    return "Lat";
            }

            return "";
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            selectedTest = selectedTestFromList();

            if (selectedTest.Item1 != "" && selectedTest.Item2 != "")
            {
                string path = getLangPath() + @"\" + selectedTest.Item2;

                File.Delete(path);
                tests = new List<Test>();
                getTestList();
                GridTests.ItemsSource = null;
                GridTests.ItemsSource = tests;
            }
            else
            {
                //this.DialogResult = false;
            }
        }
    }
}
