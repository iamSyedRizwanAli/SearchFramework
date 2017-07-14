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
using Search_Framework.Pages;
using DataAccess;
using Entities;

namespace Search_Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String[] operators = {"OR", "AND" };
        List<Operators> comp_operators;
        public MainWindow()
        {
            InitializeComponent();
            comp_operators = SearchFrameworkHelper.GetOperators();
        }

        private void StudentPage_btn_Click(object sender, RoutedEventArgs e)
        {
            List<Columns> columns = SearchFrameworkHelper.GetColumns(1);
            Entities.Pages page = SearchFrameworkHelper.GetPage(1);
            new Page1(page, operators, columns, comp_operators).Show();
        }

        private void TeacherPage_btn_Click(object sender, RoutedEventArgs e)
        {
            List<Columns> columns = SearchFrameworkHelper.GetColumns(2);
            Entities.Pages page = SearchFrameworkHelper.GetPage(2);
            new Page1(page, operators, columns, comp_operators).Show();
        }

        private void CoursePage_btn_Click(object sender, RoutedEventArgs e)
        {
            List<Columns> columns = SearchFrameworkHelper.GetColumns(3);
            Entities.Pages page = SearchFrameworkHelper.GetPage(3);
            new Page1(page, operators, columns, comp_operators).Show();
        }
    }
}
