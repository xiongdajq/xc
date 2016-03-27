using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace wjq_hw2.view_module
{
    class view_module
    {
        public view_module()
        {
            ImageSource coverSource = new BitmapImage(new Uri("ms-appx://wjq_hw2/Assets/leaf.jpg"));
            this.all_items.Add(new module.module("wjq", "people", DateTime.Today, coverSource));
            module.module m2 = new module.module("wjq1", "people1", DateTime.Today, coverSource);
            m2.ifcomplete = true;
            this.all_items.Add(m2);
        }
        private ObservableCollection<module.module> all_items = new ObservableCollection<module.module>();
        public ObservableCollection<module.module> All_items { get { return this.all_items; } }
        public module.module select_item = default(module.module);
        public module.module Select_item { get { return select_item; } set { this.select_item = value; } }
        public void add_item(string title, string detail,DateTime date, ImageSource im)
        {
            all_items.Add(new module.module(title, detail, date, im));
        }
        public void remove_item(string id)
        {
            all_items.Remove(select_item);
            this.select_item = null;
        }
        public void update_item(string title, string detail, DateTime date, ImageSource im)
        {
            select_item.title = title;
            select_item.detail = detail;
            select_item.date = date;
            select_item.images = im;
            this.select_item = null;
        }
    }
}
