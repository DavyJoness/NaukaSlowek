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
			GetWords();
			prepareGrid();
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
				//words = new List<Word>();

				GridWords.ItemsSource = words;
				GridWords.DataContext = words;
			}
		}

		public void GetWords()
		{
			if (words != null)
				words.Clear();
			else
				words = new List<Word>();

			string lanId = ((int)MainWindow.lang).ToString();
			string sqlQuery = $@"select w.word_id as Id, w.word_value as SimpleWord, w.word_translate as [Translate], w.word_parid as [ParId], w.word_catid as [CatId],
p.par_id as ParId, p.par_lanId as ParLanId, p.par_name as ParName,
c.cat_id as CatId, c.cat_lanId as CatLanId, c.cat_name as CatName
from Words w
inner join Parts p on w.word_parid = p.par_id
inner join Categories c on w.word_catid = c.cat_id
where w.word_lanid = {lanId}";

			using (var SQLiteConnection = new SQLiteConnection(ConnStr))
			{
				try
				{
					var x = SQLiteConnection.Query<Word, Item, Item, Word>(sqlQuery, 
						(w, p, c) =>

						{
									w.Part = p;
									w.Category = c;
									return w;
								}, splitOn: "ParId, CatId")
					.AsQueryable();

					words = x.ToList();
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Wystąpił błąd przy funkcji GetWords: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
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

		private void ButtonDeleteWord_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (DeleteWord())
				{
					prepareGrid(true);
				}
			}
			catch (Exception ex)
			{

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
			try
			{
				if (InsertNewWords())
				{
					prepareGrid(true);
				}
			}
			catch (Exception ex)
			{

			}

		}

		public bool DeleteWord()
		{
			bool res = true;
			Word word = GridWords.SelectedItem as Word;
			string sqlQuery = QueryDeletePrepare(word.Id);

			using (var SQLiteConnection = new SQLiteConnection(ConnStr))
			{
				try
				{
					SQLiteConnection.Execute(sqlQuery);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Wystąpił błąd przy funkcji DeleteWord: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
					res = false;
				}
			}


			return res;
		}

		public bool InsertNewWords()
		{
			bool res = true;
			List<Word> words = (List<Word>)GridWords.ItemsSource;
			string sqlQuery = QueryInsertPrepare(words.Where(n => n.Id == 0).ToList());

			using (var SQLiteConnection = new SQLiteConnection(ConnStr))
			{
				try
				{
					SQLiteConnection.Execute(sqlQuery);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Wystąpił błąd przy funkcji InsertNewWords: {ex.Message}", "NaukaSłówek", MessageBoxButton.OK, MessageBoxImage.Error);
					res = false;
				}
			}

			return res;
		}

		private string QueryDeletePrepare(int id)
		{
			return $@"delete from Words where word_id = {id}";
		}

		private string QueryInsertPrepare(List<Word> words)
		{
			string query = "insert into Words(word_lanid, word_parid, word_catid, word_value, word_translate) values {0};";
			string lanId = ((int)MainWindow.lang).ToString();           

			foreach (Word w in words)
			{
				string item = $"({lanId}, {w.Part.Id}, {w.Category.Id}, '{w.SimpleWord}', '{w.Translate}')," + "{0}";
				query = String.Format(query, item);
			}

			query = query.Replace(",{0};", ";");

			return query;
		}
	}
}
