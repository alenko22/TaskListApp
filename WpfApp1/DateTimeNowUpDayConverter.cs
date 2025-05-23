﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TaskListApp
{
    //Converter of date for green color of tasks
    class DateTimeNowUpDayConverter : IValueConverter
    {
        public object Convert(object value,
                Type targetType, object parameter, CultureInfo culture)
        {
            var d = value as DateTime?;
            if (d != null)
            {
                return DateTime.Now.Date.AddDays(1) == d.Value;
            }

            return false;
        }

        public object ConvertBack(object value,
            Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
