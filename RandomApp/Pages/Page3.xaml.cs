using OfficeOpenXml;
using RandomApp.UserControlApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace RandomApp.Pages
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public class Str
    {
        public string str { get; set; }
    }
    public partial class Page3 : Page
    {
        public ObservableCollection<User> Personnel =new ObservableCollection<User>();
        public string Text {  get; set; }
        public string s { get; set; }
        public Page3(string message)
        {
            InitializeComponent();
            this.DataContext=this;
            s= message;
            GetData();
            ShowList();
        }
        public void ShowList()
        {
            Leftbar leftbar = new Leftbar();
            leftbar.ListPersonnel.ItemsSource = Personnel;
            if (Personnel.Count > 0) { 
                ListPersonnel1.Children.Add(leftbar);
                switch (s)
                {
                    case "list1":
                        HeadeBar.Text = "Danh sách DL đoàn 1";
                        break;
                    case "list2":
                        HeadeBar.Text = "Danh sách DL đoàn 2";
                        break;
                }
            }
        }
        public void GetData()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var fileInfo = new FileInfo(@"D:\setup\PortableGit\wpf-randomapp\RandomApp\Static\PersonnelList.xlsx");

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

    }
}
