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
using System.Data.SQLite;
using Dapper;

namespace NaukaSlowek
{
    /// <summary>
    /// Interaction logic for CreateTest.xaml
    /// </summary>
    /// 
    public class Word
    {
        public int Id { get; set; }
        public string SimpleWord { get; set; }
        public string Translate { get; set; }
        public Item Category { get; set; }
        public Item Part { get; set; }
    }

    public partial class CreateTest : Window
    {
        private string ConnStr = "Data Source=words.db";

        public List<Word> words;
        public List<Item> parts;
        public List<Item> categories;

        public CreateTest()
        {
            GetSettings();
            InitializeComponent();
            ComboBoxColumnParts.ItemsSource = parts;
            ComboBoxColumnCategory.ItemsSource = categories;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mw.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            prepareGrid();
        }

        private void GetSettings()
        {
            if (parts != null) parts.Clear();
            else parts = new List<Item>();

            if (categories != null) categories.Clear();
            else categories = new List<Item>();

            string lanId = ((int)MainWindow.lang).ToString();
            string sqlQueryParts = $"select par_id as Id, par_lanid as LanId, par_name as Value from Parts where par_lanid = {lanId}";
            string sqlQueryCategories = $"select cat_id as Id, cat_lanid as LanId, cat_name as Value from Categories where cat_lanId = {lanId}";

            using (var SQLiteConnection = new SQLiteConnection(ConnStr))
            {
                try
                {
                    parts = SQLiteConnection.Query<Item>(sqlQueryParts).AsList<Item>();
                    categories = SQLiteConnection.Query<Item>(sqlQueryCategories).AsList<Item>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd przy funkcji GetSettings: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonNewTest_Click(object sender, RoutedEventArgs e)
        {
            prepareGrid(true);
        }

        private void prepareGrid(bool clear = false)
        {
            if (clear)
            {
                if (words != null) words.Clear();
                GridWords.ItemsSource = words;
            }
            else
            {
                words = new List<Word>();

                GridWords.ItemsSource = words;
                GridWords.DataContext = words;
            }
        }

        private void ButtonDeleteWord_Click(object sender, RoutedEventArgs e)
        {
            if (words.Count > 0)
            {
                int i = GridWords.SelectedIndex;
                words.RemoveAt(i);
                GridWords.ItemsSource = null;
                GridWords.ItemsSource = words;
            }
        }

        //private void ButtonSaveTest_Click(object sender, RoutedEventArgs e)
        //{
        //    string testName = "";//TextBoxTitle.Text;
        //    if(testName != "")
        //    {
        //        string path = getLangPath() + @"\" + testName + ".csv";

        //        //if test exists delete
        //        if (File.Exists(path))
        //        {
        //            File.Delete(path);
        //        }

        //        try
        //        {
        //            using (StreamWriter outputFile = new StreamWriter(path, true))
        //            {
        //                foreach (Word w in words)
        //                {
        //                    string line = testName + ";" + w.SimpleWord + ";" + w.Translate;
        //                    outputFile.WriteLine(line);
        //                }

        //            }
        //            if (MessageBox.Show("Test poprawnie zapisany. Czy chcesz utworzyc nowy?", "Nauka słówek", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
        //            {
        //                //Nowy test
        //                prepareGrid(true);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Problem podczas zapisywania testu:" + Environment.NewLine + ex.Message, "Nauka słówek", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Tytuł testu nie może być pusty", "Nauka słówek", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        //    }
        //}

        //public string getLangPath()
        //{
        //    switch (MainWindow.mw.lang)
        //    {
        //        case MainWindow.Lang.English:
        //            return "Eng";
        //        case MainWindow.Lang.French:
        //            return "Fre";
        //        case MainWindow.Lang.Latin:
        //            return "Lat";
        //    }

        //    return "";
        //}

        //private void ButtonOpenTest_Click(object sender, RoutedEventArgs e)
        //{
        //    SelectTest st = new SelectTest();

        //    if (st.ShowDialog() == true)
        //    {
        //        prepareGrid(true);
        //        //TextBoxTitle.Text = st.selectedTest.Item1;

        //        loadWords(st.selectedTest.Item2);
        //    }
        //}

        //private void loadWords(string file)
        //{
        //    string path = getLangPath() + @"\" + file;
        //    Word word;

        //    using (StreamReader sr = new StreamReader(path))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            string line = sr.ReadLine();
        //            word = new Word();
        //            word.SimpleWord = line.Split(';')[1];
        //            word.Translate = line.Split(';')[2];

        //            words.Add(word);
        //        }
        //    }
        //    GridWords.ItemsSource = null;
        //    GridWords.ItemsSource = words;
        //}

        private void SettingsParts(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings settings = new Settings(Settings.SettingType.Parts);
                settings.ShowDialog();
                if (settings.DialogResult.HasValue && settings.DialogResult.Value)
                {
                    //Jeśli coś zmieniono to trzeba pobrac na nowo dane
                }
            }
            catch (Exception ex)
            { }
        }

        private void SettingsCategories_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings settings = new Settings(Settings.SettingType.Categories);
                settings.ShowDialog();
                if (settings.DialogResult.HasValue && settings.DialogResult.Value)
                {
                    //Jeśli coś zmieniono to trzeba pobrac na nowo dane
                }
            }
            catch (Exception ex)
            { }
        }

        private void ButtonInsertWords_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
