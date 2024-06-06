using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace RandomApp.UserControlApp
{
    /// <summary>
    /// Interaction logic for BonusList.xaml
    /// </summary>
    public partial class BonusList : UserControl
    {
        public ObservableCollection<User> BonusList1 { get; set; }
        public BonusList()
        {
            InitializeComponent();
            DataContext = this;
        }
        public void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var selectedItem = datagrid1.SelectedItem as User;
                if (selectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Bạn chắc chắn muốn xóa: {selectedItem.Name} mã NV {selectedItem.Code} ra khỏi danh sách", "Cảnh báo!", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        BonusList1.Remove(selectedItem);
                    }
                }
                e.Handled = true;
            }
        }
    }
}
