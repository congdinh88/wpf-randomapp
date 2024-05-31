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

namespace RandomApp.UserControlApp
{
    /// <summary>
    /// Interaction logic for BonusList.xaml
    /// </summary>
    public partial class BonusList : UserControl
    {
        public string Text1 {  get; set; }
        public BonusList()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
