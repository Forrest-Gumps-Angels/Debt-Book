using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Debt_Book
{
    /// <summary>
    /// Interaction logic for AddDebtorView.xaml
    /// </summary>
    public partial class AddClientView : Window
    {
        public AddClientView()
        {
            InitializeComponent();
        }

        public static bool ValidateBindings(DependencyObject parent)
        {
            // Validate all the bindings on the parent
            bool valid = true;
            LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                LocalValueEntry entry = localValues.Current;
                if (BindingOperations.IsDataBound(parent, entry.Property))
                {
                    Binding binding = BindingOperations.GetBinding(parent, entry.Property);
                    foreach (ValidationRule rule in binding.ValidationRules)
                    {
                        // TODO: where to get correct culture info?
                        ValidationResult result = rule.Validate(parent.GetValue(entry.Property), null);
                        if (!result.IsValid)
                        {
                            BindingExpression expression = BindingOperations.GetBindingExpression(parent, entry.Property);
                            Validation.MarkInvalid(expression, new ValidationError(rule, expression, result.ErrorContent, null));
                            valid = false;
                        }
                    }
                }
            }

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!ValidateBindings(child)) { valid = false; }
            }

            return valid;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBindings(this))
            {
                DialogResult = true;
            }
        }
    }
}
