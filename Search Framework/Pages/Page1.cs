using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data;
using DataAccess;
using System.Windows;

namespace Search_Framework.Pages
{
    public class Page1 : page_design
    {
        public Page1(Entities.Pages pageInfo, String [] operators, List<Columns> fields , List<Operators> comparison_operators)
            : base(pageInfo, operators, fields, comparison_operators)
        {
            this.Title = pageInfo.PageName;
        }

        protected override void search_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            List<SearchParams> list = new List<SearchParams>();

            foreach (SearchParamsControls spc in stackPanel.Children)
            {
                String conditionalOperator = spc.Operator_Combobox.ItemsSource == null ? null : spc.Operator_Combobox.SelectedItem.ToString();
                
                int fieldComboBoxidx = spc.Field_Combobox.SelectedIndex;
                int operatoor = spc.ComparisonOperators_Combobox.SelectedIndex;
                String value = spc.ValueField.Text;

                SearchParams temp = new SearchParams();
                temp.FieldID = Columns.ElementAt(fieldComboBoxidx).FieldID;
                temp.ConditionalOperator = conditionalOperator;
                temp.OperatorID = ComparisonOperators.ElementAt(operatoor).OperatorID;
                temp.Value = value;

                list.Add(temp);
            }

            DataTable dt = SearchFrameworkHelper.Search(list);

            if (dt != null)
                dataGrid.ItemsSource = dt.DefaultView;
            else
                MessageBox.Show("Logical Error");
                
        }

    }
}
