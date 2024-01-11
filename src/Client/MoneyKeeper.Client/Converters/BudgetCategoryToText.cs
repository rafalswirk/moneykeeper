using MoneyKeeper.Client.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Converters
{
    public class BudgetCategoryToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is not BudgetCategoryDto)
                return null;
            var category = (BudgetCategoryDto)value;
            return $"{category.Category} / {category.Group}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
