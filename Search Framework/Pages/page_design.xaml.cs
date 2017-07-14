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
using Entities;
using DataAccess;

namespace Search_Framework.Pages
{
    /// <summary>
    /// Interaction logic for page_design.xaml
    /// </summary>
    public abstract partial class page_design : Window
    {
        protected StackPanel stackPanel;
        protected String[] operators, fields, comparison_operators;

        protected List<Operators> ComparisonOperators;
        protected List<Columns> Columns;
        protected Entities.Pages RunningPage;

        public page_design(Entities.Pages pageInfo, String []operators, List<Columns> columns, List<Operators> compOperators)
        {
            InitializeComponent();
            this.operators = operators;

            this.RunningPage = pageInfo;

            this.fields = (from c in columns select c.DisplayName).ToArray<String>();
            this.comparison_operators = (from c in compOperators select c.OperatorName).ToArray<String>();

            this.ComparisonOperators = compOperators;
            this.Columns = columns;

            stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            param_grid.Content = stackPanel;
            new SearchParamsControls(this, stackPanel, null, fields, comparison_operators);
            dataGrid.ItemsSource = SearchFrameworkHelper.FetchAllDataOfPage(RunningPage).DefaultView;

        }

        protected abstract void search_btn_Click(object sender, RoutedEventArgs e);

        protected virtual void return_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void addSearchField()
        {
            new SearchParamsControls(this, stackPanel, operators, fields,comparison_operators);
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
            new SearchParamsControls(this, stackPanel, null, fields, comparison_operators);
            dataGrid.ItemsSource = SearchFrameworkHelper.FetchAllDataOfPage(RunningPage).DefaultView;
        }


    }
}
