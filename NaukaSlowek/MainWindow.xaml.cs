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
            WordList,
            MakeTest
        }

        public int page = 0;
        public Lang lang;
        public Option option;
        public static MainWindow mw;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (page == 0)
            {
                Button b = (Button)e.Source;
                switch (b.Name)
                {
                    case "ButtonEng":
                        lang = Lang.English;
                        LabelLangSelected.Content = "Wybrano angielski";
                        LabelLangSelected2.Content = "Wybrano angielski";
                        LabelLangSelected3.Content = "Wybrano angielski";
                        break;
                    case "ButtonFre":
                        lang = Lang.French;
                        LabelLangSelected.Content = "Wybrano francuski";
                        LabelLangSelected2.Content = "Wybrano francuski";
                        LabelLangSelected3.Content = "Wybrano francuski";
                        break;
                    case "ButtonLat":
                        lang = Lang.Latin;
                        LabelLangSelected.Content = "Wybrano łaciński";
                        LabelLangSelected2.Content = "Wybrano łaciński";
                        LabelLangSelected3.Content = "Wybrano łaciński";
                        break;
                }

                showOptions();
            }
            else if (page == 1)
            {
                showTests();
            }
            else if (page == 2)
            {
                showMakeTest();
            }
            page++;
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            if (page == 3)
                showTests();
            else if (page == 2)
                showOptions();
            else if (page == 1)
                showLangs();

            page--;
        }

        private void showOptions()
        {
            StackLangs.Visibility = Visibility.Hidden;
            StackOptions.Visibility = Visibility.Visible;
            StackTests.Visibility = Visibility.Hidden;
            StackMakeTest.Visibility = Visibility.Hidden;
        }

        private void showLangs()
        {
            StackLangs.Visibility = Visibility.Visible;
            StackOptions.Visibility = Visibility.Hidden;
            StackTests.Visibility = Visibility.Hidden;
            StackMakeTest.Visibility = Visibility.Hidden;
        }

        private void showTests()
        {
            StackLangs.Visibility = Visibility.Hidden;
            StackOptions.Visibility = Visibility.Hidden;
            StackTests.Visibility = Visibility.Visible;
            StackMakeTest.Visibility = Visibility.Hidden;
        }

        private void showMakeTest()
        {
            StackLangs.Visibility = Visibility.Hidden;
            StackOptions.Visibility = Visibility.Hidden;
            StackTests.Visibility = Visibility.Hidden;
            StackMakeTest.Visibility = Visibility.Visible;
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


    }
}
