using System.Globalization;

namespace FlamingForkAdmin.Helper.Converters
{
    public class TupleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is string orderId && values[1] != null)
            {
                return Tuple.Create(orderId, values[1].ToString());
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
