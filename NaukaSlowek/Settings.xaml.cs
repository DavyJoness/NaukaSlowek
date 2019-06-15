using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
    public class Item
    {
        public int Id { get; set; }
        public int LanId { get; set; }
        public string Value { get; set; }
    }

    public partial class Settings : Window
    {
        public enum SettingType
        {
            Parts,
            Categories
        }

        SQLiteConnection SQLiteConnection = new SQLiteConnection("Data Source=words.db");
        SettingType settingType;
        List<Item> settingItems = new List<Item>();
        private DataTable settingsTable;

        public Settings(SettingType s)
        {
            InitializeComponent();
            settingType = s;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getSettingValuesList();
            PrepareGrid();
        }

        private void getSettingValuesList()
        {
            if (settingType == SettingType.Parts)
                GetParts();
            else if (settingType == SettingType.Categories)
                GetCategories();
            else
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        private void PrepareGrid()
        {
            DataGridTextColumn column;

            settingsTable = new DataTable();
            GridValues.AutoGenerateColumns = false;
            GridValues.ItemsSource = settingItems;

            column = new DataGridTextColumn();
            column.Header = "Wartość";
            column.Width = new DataGridLength(100, DataGridLengthUnitType.Star);
            column.Binding = new Binding("Value");
            GridValues.Columns.Add(column);

            GridValues.DataContext = settingsTable.DefaultView;
        }

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

        private void GetParts()
        {
            SQLiteCommand oCommand = SQLiteConnection.CreateCommand();
            SQLiteDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            Item item;
            string lanId = ((int)MainWindow.lang).ToString();
            string query = $"select * from Parts where par_lanid = {lanId}";

            try
            {
                SQLiteConnection.Open();
                oCommand.CommandText = query;

                dataAdapter = new SQLiteDataAdapter(oCommand.CommandText, SQLiteConnection);
                SQLiteCommandBuilder oCommandBuilder = new SQLiteCommandBuilder(dataAdapter);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    item = new Item();
                    item.Id = Convert.ToInt32(dr["par_id"]);
                    item.LanId = Convert.ToInt32(dr["par_lanId"]);
                    item.Value = dr["par_name"].ToString();

                    settingItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd przy funkcji GetParts: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                SQLiteConnection.Close();
            }
        }

        private void GetCategories()
        {
            SQLiteCommand oCommand = SQLiteConnection.CreateCommand();
            SQLiteDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            Item item;
            string lanId = ((int)MainWindow.lang).ToString();
            string query = $"select * from Categories where cat_lanId = {lanId}";

            try
            {
                SQLiteConnection.Open();
                oCommand.CommandText = query;

                dataAdapter = new SQLiteDataAdapter(oCommand.CommandText, SQLiteConnection);
                SQLiteCommandBuilder oCommandBuilder = new SQLiteCommandBuilder(dataAdapter);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    item = new Item();
                    item.Id = Convert.ToInt32(dr["cat_id"]);
                    item.LanId = Convert.ToInt32(dr["cat_lanId"]);
                    item.Value = dr["cat_name"].ToString();

                    settingItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd przy funkcji GetCategories: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                SQLiteConnection.Close();
            }
        }

        private bool SetParts()
        {
            bool res = true;

            SQLiteCommand oCommand = SQLiteConnection.CreateCommand();
            string query = queryPrepare();

            try
            {
                SQLiteConnection.Open();
                oCommand.CommandText = query;
                oCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd przy funkcji SetParts: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
                res = false;
            }
            finally
            {
                SQLiteConnection.Close();
            }

            return res;
        }

        private string queryPrepare()
        {
            string query = "";
            string lanId = ((int)MainWindow.lang).ToString();

            if (settingType == SettingType.Parts)
            {
                query = $@"delete from Parts where par_lanid = {lanId}
insert into Parts(par_lanid, par_value)
values ";

                foreach (Item i in settingItems)
                {
                    string s = $"({i.LanId}, {i.Value}),";
                    query += s;
                }

                query = query.Substring(0, query.Length - 1);
            }
            else if (settingType == SettingType.Categories)
            {
                query = $@"delete from Categories where cat_lanid = {lanId}
insert into Categories(cat_lanid, cat_value)
values ";

                foreach (Item i in settingItems)
                {
                    string s = $"({i.LanId}, {i.Value}),";
                    query += s;
                }

                query = query.Substring(0, query.Length - 1);
            }

            return query;
        }

        private bool SetCategories()
        {
            bool res = true;

            SQLiteCommand oCommand = SQLiteConnection.CreateCommand();
            string query = "";

            try
            {
                SQLiteConnection.Open();
                oCommand.CommandText = query;
                oCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd przy funkcji SetCategories: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
                res = false;
            }
            finally
            {
                SQLiteConnection.Close();
            }

            return res;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            if (settingType == SettingType.Parts)
                SetParts();
            else if (settingType == SettingType.Categories)
                SetCategories();
            else
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
