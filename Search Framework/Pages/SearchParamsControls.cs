using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Search_Framework.Pages
{
    internal class SearchParamsControls : StackPanel
    {
        private page_design ParentWindow;
        public ComboBox Operator_Combobox { get; set; }
        public ComboBox Field_Combobox { get; set; }
        public ComboBox ComparisonOperators_Combobox { get; set; }
        public TextBox ValueField { get; set; }
        public Button AddFieldButton { get; set; }

        public SearchParamsControls(page_design parentWindow, StackPanel g, String [] operators, String [] fields, String [] comparison_operators)
        {
            this.ParentWindow = parentWindow;
            Operator_Combobox = new ComboBox();
            Operator_Combobox.Width = 50;

            if (operators == null)
            {
                Operator_Combobox.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                Operator_Combobox.ItemsSource = operators;
                Operator_Combobox.SelectedIndex = 0;
            }

            if (comparison_operators == null)
                throw new Exception("Comparison operators must not be null");

            if (fields == null)
                throw new Exception("Fields must not be null");

            Field_Combobox = new ComboBox();
            Field_Combobox.ItemsSource = fields;
            Field_Combobox.Width = 100;
            Field_Combobox.SelectedIndex = 0;

            ValueField = new TextBox();
            ValueField.Width = 200;

            AddFieldButton = new Button();
            AddFieldButton.Content = "+";
            AddFieldButton.Width = 20;
            AddFieldButton.Click += new RoutedEventHandler(addButtonClick);

            ComparisonOperators_Combobox = new ComboBox();
            ComparisonOperators_Combobox.ItemsSource = comparison_operators;
            ComparisonOperators_Combobox.Width = 100;
            ComparisonOperators_Combobox.SelectedIndex = 0;

            this.Orientation = System.Windows.Controls.Orientation.Horizontal;
            this.Children.Add(Operator_Combobox);
            this.Children.Add(Field_Combobox);
            this.Children.Add(ComparisonOperators_Combobox);
            this.Children.Add(ValueField);
            this.Children.Add(AddFieldButton);
            this.Height = 25;
            

            g.Children.Add(this);
        }

        private void addButtonClick(object sender, EventArgs e)
        {
            AddFieldButton.Visibility = System.Windows.Visibility.Hidden;
            ParentWindow.addSearchField();
        }

    }
}
