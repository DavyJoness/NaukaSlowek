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

namespace NaukaSlowek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Lang
        {
            English,
            French,
            Latin
        }

        public enum Option
        {
            CreateTest,
            MakeTest
        }

        public Lang lang;
        public Option option;
        public static MainWindow mw;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)e.Source;
            switch (b.Name)
            {
                case "ButtonEng":
                    lang = Lang.English;
                    LabelLangSelected.Content = "Wybrano angielski";
                    break;
                case "ButtonFre":
                    lang = Lang.French;
                    LabelLangSelected.Content = "Wybrano francuski";
                    break;
                case "ButtonLat":
                    lang = Lang.Latin;
                    LabelLangSelected.Content = "Wybrano łaciński";
                    break;
            }

            showOptions();
        }

        private void showOptions()
        {
            StackLangs.Visibility = Visibility.Hidden;
            StackOptions.Visibility = Visibility.Visible;
        }

        private void showLangs()
        {
            StackLangs.Visibility = Visibility.Visible;
            StackOptions.Visibility = Visibility.Hidden;
        }

        private void Button_Make(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            ExecuteTest et = new ExecuteTest();
            mw = this;
            et.Show();
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            CreateTest et = new CreateTest();
            mw = this;
            et.Show();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            showLangs();
        }
    }
}
