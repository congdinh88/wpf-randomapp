using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public string [] Str2 {  get; set; }
        public BonusList()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
