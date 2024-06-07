using MaterialDesignThemes.Wpf;
using OfficeOpenXml;
using RandomApp.UserControlApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;
using static MaterialDesignThemes.Wpf.Theme;

namespace RandomApp.Pages
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        public ObservableCollection<User> Personnel =new ObservableCollection<User>();
        public ObservableCollection<User> prize1 = new ObservableCollection<User>();
        public ObservableCollection<User> prize2 = new ObservableCollection<User>();
        public ObservableCollection<User> prize3 = new ObservableCollection<User>();
        public ObservableCollection<User> prize4 = new ObservableCollection<User>();
        public ObservableCollection<User> prize5 = new ObservableCollection<User>();

        Random random = new Random();
        int count = 10;
        bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, clean = true;
        public string s { get; set; }
        public Page3(string message)
        {
            InitializeComponent();
            DataContext=this;
            s = message;
            GetData();
            timer1.Interval = TimeSpan.FromMilliseconds(150);
            timer1.Tick += TextRun;
            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += TextStart;
            ShowList();
            CombApp.ItemsSource = ListComboBox();
            this.PreviewKeyDown += Page3_PreviewKeyDown;

        }

        private void UpdateHeader()
        {
            foreach (BonusList item in ListPersonnel1.Children)
            {
                string? str = item.expander1.Header.ToString();
                if (str.Length >= 6)
                {
                    string s1 = str.Substring(0, str.Length - 6);
                    switch (s1)
                    {
                        case "Giải đặc biệt":
                            item.expander1.Header = $"{s1} ({item.BonusList1.Count}/1)";
                            break;
                        case "Giải nhất":
                            item.expander1.Header = $"{s1} ({item.BonusList1.Count}/2)";
                            break;
                        case "Giải nhì":
                            item.expander1.Header = $"{s1} ({item.BonusList1.Count}/3)";
                            break;
                        case "Giải ba":
                            item.expander1.Header = $"{s1} ({item.BonusList1.Count}/4)";
                            break;
                        case "Giải khuyến khích":
                            item.expander1.Header = $"{s1} ({item.BonusList1.Count}/5)";
                            break;
                    }
                }

            }
        }
        private void Page3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            foreach (BonusList item in ListPersonnel1.Children)
            {
                item.DataGrid_KeyDown(sender, e);
                string? str = item.expander1.Header.ToString();
                if (str.Length >=6 )
                {
                    string s = str.Substring(0, str.Length - 6);
                    switch (s)
                    {
                        case "Giải đặc biệt":
                            item.expander1.Header = $"{s} ({item.BonusList1.Count}/1)";
                            break;
                        case "Giải nhất":
                            item.expander1.Header = $"{s} ({item.BonusList1.Count}/2)";
                            break;
                        case "Giải nhì":
                            item.expander1.Header = $"{s} ({item.BonusList1.Count}/3)";
                            break;
                        case "Giải ba":
                            item.expander1.Header = $"{s} ({item.BonusList1.Count}/4)";
                            break;
                        case "Giải khuyến khích":
                            item.expander1.Header = $"{s} ({item.BonusList1.Count}/5)";
                            break;
                    }
                }
            }
        }

        void TextStart(object o, EventArgs e)
        {
            count--;
            if (count == 0)
            {
                Btn1.Content = "Bắt đầu";
                count = 10;
            }
            else
            {
                Btn1.Content = count.ToString();
            }
        }

        private string[] ListComboBox()
        {
            string[] strArray =
            {
                "Giải đặc biệt",
                "Giải nhất",
                "Giải nhì",
                "Giải ba",
                "Giải khuyến khích"
            };
            return strArray;
        }

        public void TextRun(object o, EventArgs e)
        {
            var personnelArray = Personnel.ToArray();
            int i = Personnel.Count;
            int j = random.Next(0, i);
            txt3.Text = personnelArray[j].Code.ToString();
            txt4.Text = personnelArray[j].Name.ToString();
            txt5.Text = "-";
            txt6.Text = personnelArray[j].Workshop.ToString();
        }
        public void ShowList()
        {
            Leftbar leftbar = new Leftbar();
            leftbar.datagrid1.ItemsSource = Personnel;
            if (Personnel.Count > 0) { 
                ListPersonnel1.Children.Add(leftbar);
                switch (s)
                {
                    case "list1":
                        txt7.Text = "Danh sách DL đoàn 1";
                        break;
                    case "list2":
                        txt7.Text = "Danh sách DL đoàn 2";
                        break;
                }
            }
        }
        public void GetData()
        {
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var fileInfo = new FileInfo(@"Static\PersonnelList.xlsx");

            if (!fileInfo.Exists)
            {
                MessageBox.Show("File not found.");
                return;
            }

            try
            {
                using (var package = new ExcelPackage(fileInfo))
                {
                    switch (s)
                    {
                        case "list1":
                            var worksheet1 = package.Workbook.Worksheets["List1"];
                            ReadSheetData(worksheet1);
                            break;
                        case "list2":
                            var worksheet2 = package.Workbook.Worksheets["List2"];
                            ReadSheetData(worksheet2);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        public void ReadSheetData(ExcelWorksheet worksheet)
        {
            if (worksheet == null)
            {
                MessageBox.Show("Worksheet not found.");
                return;
            }
            int rowCount = worksheet.Dimension.End.Row;
            for (int i = 1; i < rowCount - 1; i++)
            {
                string code = worksheet.Cells[i, 1].Text;
                string name = worksheet.Cells[i, 2].Text;
                string workshop = worksheet.Cells[i, 3].Text;
                Personnel.Add(new User { Code = code, Name = name, Workshop = workshop });
            }
        }

        private void CombApp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CombApp.SelectedValue != null)
            { 

                txt1.Text = "Chào mừng đến với vòng quay " + CombApp.SelectedValue.ToString().ToLower();
                Btn1.IsEnabled = true;

                if (prize5.Count < 5)
                {
                    CombApp.SelectedValue = ListComboBox()[4];
                }
                else if (prize4.Count < 4)
                {
                    CombApp.SelectedValue = ListComboBox()[3];
                }
                else if (prize3.Count < 3)
                {
                    CombApp.SelectedValue = ListComboBox()[2];
                }
                else if (prize2.Count < 2)
                {
                    CombApp.SelectedValue = ListComboBox()[1];
                }
                else if (prize1.Count < 1)
                {
                    CombApp.SelectedValue = ListComboBox()[0];
                }
                else 
                {
                    Btn1.IsEnabled = false;
                    CombApp.SelectedItem = null;
                }
            }
            else{
                txt1.Text = "Vòng quay quay thưởng đã kết thúc. Chúc may mắn lần sau!";
            }
        }

        public async void UpdateText()
        {
            txt1.Text = "Chào mừng đến với vòng quay " + CombApp.SelectedValue.ToString().ToLower();
            Btn1.IsEnabled = false;
            Btn2.IsEnabled = false;
            CombApp.IsEnabled = false;
            Btn1.Content = "10";
            count = 10;
            timer1.Start();
            timer2.Start();
            await Task.Delay(10000);
            timer1.Stop();
            timer2.Stop();
            txt1.Text = "Xin chúc mừng nhân viên có mã số:";
            Btn1.IsEnabled = true;
            Btn2.IsEnabled = true;
            CombApp.IsEnabled = true;
            Btn1.Content = "Bắt đầu";
        }
        public void ShowMessenger()
        {
            MessageBox.Show("Số lượng giải đã đủ !");
        }
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            string str = CombApp.Text;
            if(str != "")
            {
                switch (str)
                {
                    case "Giải đặc biệt":
                        (prize1.Count >= 1 ? (Action)ShowMessenger : UpdateText)();
                        break;
                    case "Giải nhất":
                        (prize2.Count >= 2 ? (Action)ShowMessenger : UpdateText)();
                        break;
                    case "Giải nhì":
                        (prize3.Count >= 3 ? (Action)ShowMessenger : UpdateText)();
                        break;
                    case "Giải ba":
                        (prize4.Count >= 4 ? (Action)ShowMessenger : UpdateText)();
                        break;
                    case "Giải khuyến khích":
                        (prize5.Count >= 5 ? (Action)ShowMessenger : UpdateText)();
                        break;
                }
            }
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Btn2.IsEnabled = false;
            txt7.Text = "Danh sách giải thưởng";
            string str = CombApp.Text;
            string c = txt3.Text;
            string n = txt4.Text;
            string w = txt6.Text;
            BonusList bonusList = new BonusList();
            
            if (str !="" && c!=""&& n!="" && w!="")
            {
                
                if (clean == true)
                {
                    ListPersonnel1.Children.Clear();
                    clean = false;
                    switch (str) {
                        
                        case "Giải khuyến khích":
                            {
                                prize5.Add(new User() { Code = c, Name = n, Workshop = w });
                                bonusList.expander1.Header = $"{str} ({prize5.Count}/5)";
                                bonusList.BonusList1 = prize5;
                                ListPersonnel1.Children.Insert(0, bonusList);
                            }
                            break;
                        }
                }
                else
                {
                    switch (str)
                    {
                        case "Giải đặc biệt":
                            prize1.Add(new User() { Code = c, Name = n, Workshop = w });
                            bonusList.expander1.Header = $"{str} ({prize1.Count}/1)";
                            bonusList.BonusList1 = prize1;
                            if (b1 == false)
                            {
                                ListPersonnel1.Children.Insert(0, bonusList);
                                b1 = true;
                            }
                            break;
                        case "Giải nhất":
                            
                            if (prize2.Count()>=2)
                            {
                                ShowMessenger();
                            }
                            else
                            {
                                prize2.Add(new User() { Code = c, Name = n, Workshop = w });
                                bonusList.expander1.Header = $"{str} ({prize2.Count}/2)";
                                bonusList.BonusList1 = prize2;
                                if (b2 == false)
                                {
                                    ListPersonnel1.Children.Insert(0, bonusList);
                                    b2 = true;
                                }
                            }

                            break;
                        case "Giải nhì":
                            if (prize3.Count > 3)
                            {
                                ShowMessenger();
                            }
                            else
                            {
                                prize3.Add(new User() { Code = c, Name = n, Workshop = w });
                                bonusList.expander1.Header = $"{str} ({prize3.Count}/3)";
                                bonusList.BonusList1 = prize3;
                                if (b3 == false)
                                { 
                                    ListPersonnel1.Children.Insert(0, bonusList);
                                    b3 = true;
                                }
                            }
                            break;
                        case "Giải ba":
                            if (prize4.Count > 4)
                            {
                                ShowMessenger();
                            }
                            else
                            {
                                prize4.Add(new User() { Code = c, Name = n, Workshop = w });
                                bonusList.expander1.Header = $"{str} ({prize4.Count}/4)";
                                bonusList.BonusList1 = prize4;
                                if (b4 == false)
                                {
                                    ListPersonnel1.Children.Insert(0, bonusList);
                                    b4 = true;
                                }
                            }
                            break;
                        case "Giải khuyến khích":
                            if (prize5.Count > 5)
                            {
                                ShowMessenger();
                            }
                            else
                            {
                                prize5.Add(new User() { Code = c, Name = n, Workshop = w });
                                bonusList.BonusList1 = prize5;
                            }
                            break;
                    }
                }
                bonusList.datagrid1.ItemsSource = bonusList.BonusList1;

                for (int i = 0; i < Personnel.Count; i++)
                {
                    if (Personnel[i].Code == c)
                    {
                        Personnel.Remove(Personnel[i]);
                    }
                }
                txt1.Text = "Chào mừng đến với vòng quay " + CombApp.SelectedValue.ToString().ToLower();
                txt3.Text = "----";
                txt5.Text = "----";
                txt4.Text = "";
                txt6.Text = "";
                UpdateHeader();
            }
        }
    }
}
