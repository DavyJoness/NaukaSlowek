using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using Dapper;

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

        private string ConnStr = "Data Source=words.db";
        SettingType settingType;
        List<Item> settingItems;

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
            GridValues.ItemsSource = settingItems;
            GridValues.DataContext = settingItems;
        }

        private void GetParts()
        {
            settingItems = new List<Item>();
            string lanId = ((int)MainWindow.lang).ToString();
            string sqlQuery = $"select par_id as Id, par_lanid as LanId, par_name as Value from Parts where par_lanid = {lanId}";

            using (var SQLiteConnection = new SQLiteConnection(ConnStr))
            {
                try
                {
                    settingItems = SQLiteConnection.Query<Item>(sqlQuery).AsList<Item>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd przy funkcji GetParts: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GetCategories()
        {
            settingItems = new List<Item>();
            string lanId = ((int)MainWindow.lang).ToString();
            string sqlQuery = $"select cat_id as Id, cat_lanid as LanId, cat_name as Value from Categories where cat_lanId = {lanId}";

            using (var SQLiteConnection = new SQLiteConnection(ConnStr))
            {
                try
                {
                    settingItems = SQLiteConnection.Query<Item>(sqlQuery).AsList<Item>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd przy funkcji GetCategories: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool SetItems()
        {
            bool res = true;
            string sqlQuery = queryPrepare();

            using (var SQLiteConnection = new SQLiteConnection(ConnStr))
            {
                try
                {
                    SQLiteConnection.Execute(sqlQuery);
                }
                catch (Exception ex)
                {
                    string type = (settingType == SettingType.Categories) ? "kategorii" : "części mowy";
                    MessageBox.Show($"Wystąpił błąd przy funkcji SetItems dla {type}: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
                    res = false;
                }
            }

            return res;
        }

        private string queryPrepare()
        {
            string query = "";
            string lanId = ((int)MainWindow.lang).ToString();

            if (settingType == SettingType.Parts)
            {
                query = $@"delete from Parts where par_lanid = {lanId};
insert into Parts(par_lanid, par_name)
values ";

                foreach (Item i in settingItems)
                {
                    string s = $"({lanId}, '{i.Value}'),";
                    query += s;
                }

                query = query.Substring(0, query.Length - 1) + ";";
            }
            else if (settingType == SettingType.Categories)
            {
                query = $@"delete from Categories where cat_lanid = {lanId};
insert into Categories(cat_lanid, cat_name)
values ";

                foreach (Item i in settingItems)
                {
                    string s = $"({lanId}, '{i.Value}'),";
                    query += s;
                }

                query = query.Substring(0, query.Length - 1) + ";";
            }

            return query;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            SetItems();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Item i = new Item
            {
                LanId = ((int)MainWindow.lang),
                Value = TextBoxNewItem.Text
            };

            settingItems.Add(i);
        }
    }
}
