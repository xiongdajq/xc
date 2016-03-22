using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace wjq_hw2.module
{
    class module : INotifyPropertyChanged
    {
        public ImageSource image;
        public ImageSource images { set
            {
                image = value;
                change_property();
            }
            get { return image; }

        }
        bool? ifcompletee;
        public string id { get; }
        public string title
        {
            get;
            set;
        }
        public string detail
        {
            get;
            set;
        }
       
        public bool? ifcomplete
        {
            get { return ifcompletee; }
            set
            {
                ifcompletee = value;
                change_property();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void change_property([CallerMemberName] string property_name = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }
        public DateTime date { set; get; }
        public module (string title, string detail, DateTime date, ImageSource images)
        {
            this.title = title;
            this.detail = detail;
            this.ifcomplete = false;
            this.id = Guid.NewGuid().ToString();
            this.date = date;
            this.image = images;
        }
        //public object convert(object value)
        //{
        //    var b = value as bool?;
        //    if (b == null || b == false) return Visibility.Collapsed;
        //    else return Visibility.Visible;
        //}
    }
    public class B2VConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var b = value as bool?;
            if (b == null || b == false) return Visibility.Collapsed;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var b = value as Visibility?;
            if (b == Visibility.Collapsed) return false;
            return true;
        }
    }
}
