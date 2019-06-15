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

namespace NaukaSlowek
{
    /// <summary>
    /// Interaction logic for CreateTest.xaml
    /// </summary>
    /// 
    public class Word
    {
        public string SimpleWord { get; set; }
        public string Translate { get; set; }
        public string Category { get; set; }
        public string Part { get; set; }

    }
    public partial class CreateTest : Window
    {
        DataTable TableWords;
        public List<Word> words = new List<Word>();
        private List<string> parts = new List<string>() { "part1", "part2" };
        private List<string> categories = new List<string>() { "cat1", "cat2" };

        public CreateTest()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mw.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            prepareGrid();
            //TextBoxTitle.Focus();
        }

        private void ButtonNewTest_Click(object sender, RoutedEventArgs e)
        {
            prepareGrid(true);
        }

        private void prepareGrid(bool clear = false)
        {
            if (clear)
            {
                words = new List<Word>();
                GridWords.ItemsSource = words;
            }
            else
            { 
                DataGridTextColumn column;
                DataGridComboBoxColumn column_c;

                TableWords = new DataTable();
                GridWords.AutoGenerateColumns = false;
                GridWords.ItemsSource = words;

                column = new DataGridTextColumn();
                column.Header = "Słówko";
                column.IsReadOnly = false;
                column.Width =  new DataGridLength(25, DataGridLengthUnitType.Star);
                column.Binding = new Binding("SimpleWord");
                GridWords.Columns.Add(column);

                column = new DataGridTextColumn();
                column.Header = "Tłumaczenie";
                column.Width = new DataGridLength(25, DataGridLengthUnitType.Star);
                column.Binding = new Binding("Translate");
                GridWords.Columns.Add(column);

                column_c = new DataGridComboBoxColumn();
                column_c.Header = "Kategoria";
                column_c.Width = new DataGridLength(25, DataGridLengthUnitType.Star);
                column_c.SelectedValueBinding = new Binding("Category");
                column_c.ItemsSource = categories;
                GridWords.Columns.Add(column_c);

                column_c = new DataGridComboBoxColumn();
                column_c.Header = "Cz. mowy";
                column_c.Width = new DataGridLength(25, DataGridLengthUnitType.Star);
                column_c.SelectedValueBinding = new Binding("Part");
                column_c.ItemsSource = parts;
                GridWords.Columns.Add(column_c);

                GridWords.DataContext = TableWords.DefaultView;
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

                }
            }
            catch (Exception ex)
            { }
        }
    }
}
