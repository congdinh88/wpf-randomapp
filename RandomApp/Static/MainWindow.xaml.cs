using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using RandomApp.UsercontrolApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
using System.Xml.Linq;
using static RandomApp.UsercontrolApp.ListPersonnelChoice;

namespace RandomApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        Random random = new Random();
        int count = 15;
        bool start = false, b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, clean = true;
        public List<User> prize1 = new List<User>();
        public List<User> prize2 = new List<User>();
        public List<User> prize3 = new List<User>();
        public List<User> prize4 = new List<User>();
        public List<User> prize5 = new List<User>();
        public List<User> personnelNMCD = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            GetData();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += TextRun;
            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += TextStart;
            comboBox.ItemsSource = ListComboBox();
            ShowPersonnel();
        }
       
        public void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startBtn.IsEnabled = true;
            lb1.Text = "Chào mừng đến với vòng quay may mắn:";
            lb1.FontSize = 30;
            lb1.Width = 600;
            lb1.Margin = new Thickness(80, 0, 0, 0);
            lb3.Content = comboBox.SelectedValue.ToString();
            main.Children.Remove(lb2);
            main.Children.Remove(lb3);
            main.Children.Insert(1, lb3);
            codeShow.Content = "";
            nameShow.Content = "";
            workshopShow.Content = "";
            underLine.Content = "";
        }
        
        public void GetData()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo("PersonnelNMCD.xlsx")))
            {
                var workSheet = package.Workbook.Worksheets["Personnel"];
                int rowCount = workSheet.Dimension.End.Row;
                for (int i = 1; i < rowCount-1; i++)
                {
                    string code = workSheet.Cells[i, 1].Text;
                    string name = workSheet.Cells[i, 2].Text;
                    string workshop = workSheet.Cells[i, 3].Text;
                    personnelNMCD.Add(new User() { Code = code, Name = name, Workshop = workshop });
                }
            }
        }
        private void ShowPersonnel()
        {
            ListPersonnelChoice listPersonnelChoice = new ListPersonnelChoice();
            listPersonnelChoice.dataGrid.ItemsSource = personnelNMCD;
            listPersonnelChoice.Text = "Danh sách CBCNV";
            leftBar.Children.Add(listPersonnelChoice);
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
        void TextRun(object o, EventArgs e)
        {
            var personnelArray = personnelNMCD.ToArray();
            int i = personnelNMCD.Count;
            int j = random.Next(0, i);
            codeShow.Content = personnelArray[j].Code.ToString();
            nameShow.Content = personnelArray[j].Name.ToString();
            workshopShow.Content = personnelArray[j].Workshop.ToString();
            underLine.Content = "-";
        }
        void TextStart(object o, EventArgs e)
        {
            count--;
            if (count == 0)
            {
                startBtn.Content = "Bắt đầu";
                count = 15;
            }
            else
            {
                startBtn.Content = count.ToString();
            }
        }
        private async void startClick(object sender, RoutedEventArgs e)
        {
            start = true;
            startBtn.Content = "15";
            string str1 = comboBox.Text;
            lb1.Text = "Chào mừng đến với vòng quay may mắn:";
            lb1.FontSize = 30;
            lb1.Width = 600;
            lb1.Margin = new Thickness(80, 0, 0, 0);
            if (str1 != "")
            {
                timer.Start();
                timer2.Start();
                main.Children.Remove(lb3);
                main.Children.Insert(1,lb3);
                startBtn.IsEnabled = false;
                updateBtn.IsEnabled = false;
                comboBox.IsEnabled = false;
                await Task.Delay(15000);
                timer.Stop();
                timer2.Stop();
                lb1.Text = "Xin chúc mừng nhân viên có mã số:";
                main.Children.Remove(lb3);
                count = 15;
                startBtn.Content = "Bắt đầu";
                startBtn.IsEnabled = true;
                updateBtn.IsEnabled = true;
                comboBox.IsEnabled = true;
            }
        }
        public void UpdateClick(object sender, RoutedEventArgs e)
        {
            ListPersonnelChoice listPersonnelChoice = new ListPersonnelChoice();
            string str = comboBox.Text;
            string c = codeShow.Content.ToString();
            string n = nameShow.Content.ToString();
            string w = workshopShow.Content.ToString();
            lb1.Text = "Chào mừng đến với vòng quay may mắn:";
            lb1.FontSize = 30;
            lb1.Width = 600;
            lb1.Margin = new Thickness(80, 0, 0, 0);
            lb3.Content = str;
            main.Children.Insert(1, lb3);
            updateBtn.IsEnabled = false;
            if (clean == true)
            {
                clean = false;
                leftBar.Children.Clear();
                if (start == true)
                {
                    switch (str)
                    {
                        case "Giải đặc biệt":
                            if (b1 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize1.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize1;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b1 = true;
                            }
                            else
                            {
                                prize1.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize1;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            }
                            break;
                        case "Giải nhất":
                            if (b2 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize2.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize2;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b2 = true;
                            }
                            else
                            {
                                prize2.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize2;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            }
                            break;
                        case "Giải nhì":
                            if (b3 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize3.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize3;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b3 = true;
                            }
                            else
                            {
                                prize3.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize3;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            }
                            break;
                        case "Giải ba":
                            if (b4 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize4.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize4;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b4 = true;
                            }
                            else
                            {
                                prize4.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize4;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            }
                            break;
                        case "Giải khuyến khích":
                            if (b5 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize5.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize5;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b5 = true;
                            }
                            else
                            {
                                prize5.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize5;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            }
                            break;
                    }
                }
            }
            else
            {
                if (start == true)
                {
                    switch (str)
                    {
                        case "Giải đặc biệt":
                            if (b1 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize1.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize1;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b1 = true;
                            }
                            else
                            {
                                prize1.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize1;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            }
                            break;
                        case "Giải nhất":
                            if (b2 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize2.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize2;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b2 = true;
                            }
                            else
                            {
                                prize2.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize2;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            }
                            break;
                        case "Giải nhì":
                            if (b3 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize3.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize3;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b3 = true;
                            }
                            else
                            {
                                prize3.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize3;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            }
                            break;
                        case "Giải ba":
                            if (b4 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize4.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize4;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b4 = true;
                            }
                            else
                            {
                                prize4.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize4;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            };
                            break;
                        case "Giải khuyến khích":
                            if (b5 == false)
                            {
                                listPersonnelChoice.Text = str;
                                prize5.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize5;
                                leftBar.Children.Insert(0, listPersonnelChoice);
                                start = false;
                                b5 = true;
                            }
                            else
                            {
                                prize5.Add(new User() { Code = c, Name = n, Workshop = w });
                                listPersonnelChoice.dataGrid.ItemsSource = prize5;
                                CollectionViewSource.GetDefaultView(listPersonnelChoice.dataGrid.ItemsSource).Refresh();
                            };
                            break;
                    }
                }
            }
            for (int i = 0; i < personnelNMCD.Count; i++)
            {
                if (personnelNMCD[i].Code == c)
                {
                    personnelNMCD.Remove(personnelNMCD[i]);
                }
            }
            codeShow.Content = "";
            nameShow.Content = "";
            workshopShow.Content = "";
            underLine.Content = "";
        }
    }
}
