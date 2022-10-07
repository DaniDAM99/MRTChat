using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRTChat.Converters
{
    public class StringTo2LettersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var def_username = "AN";
            if (value != null && value is string username)
            {
                try
                {
                    if (string.IsNullOrEmpty(username))
                    {
                        return def_username;
                    }
                    else
                    {
                        if(username.Length > 2)
                        {
                            return username.Substring(0, 2);
                        }
                        else
                            return username;
                    }
                }
                catch (Exception e)
                {
                    return def_username;
                }

            }
            else
            {
                return def_username;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
