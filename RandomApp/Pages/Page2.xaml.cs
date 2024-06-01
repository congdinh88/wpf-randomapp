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

namespace RandomApp.Pages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }
        private void BtnPage2_Click1(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Page2ToPage3("list1");
            ((MainWindow)Application.Current.MainWindow).NavigateToPage3();
        }

        private void BtnPage2_Click2(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Page2ToPage3("list2");
            ((MainWindow)Application.Current.MainWindow).NavigateToPage3();
        }
    }
}
