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

namespace NaukaSlowek
{
    /// <summary>
    /// Interaction logic for ExecuteTest.xaml
    /// </summary>
    public partial class ExecuteTest : Window
    {
        public ExecuteTest()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.mw.Visibility = Visibility.Visible;
        }
    }
}
