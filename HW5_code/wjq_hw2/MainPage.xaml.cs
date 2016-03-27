using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Popups;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;




//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace wjq_hw2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
       

        public object SuspensionManager { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(300, 200));

            /// TODO
            /// 
            /// 标题栏与内容融合， 类似酷狗。 然而要重写return， 留坑， 以后填。
            /*var coreTitleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            var appTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;*/

            var view = ApplicationView.GetForCurrentView();
            view.TitleBar.BackgroundColor = Colors.PaleTurquoise;
            view.TitleBar.ButtonBackgroundColor = Colors.PaleTurquoise;
            this.view_Module = new view_module.view_module();
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            //dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
    //DataRequestedEventArgs>(this.ShareImageHandler);

        }
        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args) {
            DataRequest request = args.Request;

            //request.Data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromFile(thumbnailFile);

            if (view_Module.Select_item != null) {
                request.Data.SetText(view_Module.Select_item.title + " " + view_Module.Select_item.detail);

            }
            request.Data.Properties.Title = "Share Text";
            request.Data.Properties.Description = "Share Todos";
        }
        private void add_buttonclick(object sender, RoutedEventArgs e)
        {
            //view_Module.select_item.title = "";
            //view_Module.select_item.detail = "";
            //view_Module.select_item.date = DateTime.Today;
            Frame rootF = Window.Current.Content as Frame;
            if (rootF.ActualWidth < 700)
                Frame.Navigate(typeof(EditPage),view_Module);
        }
        view_module.view_module view_Module
        {
            get; set;
        }
       

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

            if (e.Parameter.GetType() == typeof(view_module.view_module))
            {
                this.view_Module = (view_module.view_module)(e.Parameter);
            }
        }


        
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            view_Module.Select_item = (module.module)(e.ClickedItem);
            Frame rootF = Window.Current.Content as Frame;
            if (rootF.ActualWidth >= 700)
            {
                tb_t.Text = view_Module.select_item.title;
                tb_d.Text = view_Module.select_item.detail;
                dp.Date = view_Module.select_item.date;
                right_image.Source = view_Module.select_item.image;
                createButton.Visibility = Visibility.Collapsed;
                updateButton.Visibility = Visibility.Visible;
               
            } else
            {
               
                Frame.Navigate(typeof(EditPage), view_Module);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cb_click(object sender, RoutedEventArgs e)
        {
            if (tb_t.Text == "")
            {
                var messagedialog = new MessageDialog("title can not be empty").ShowAsync();

            }
            else if (tb_d.Text == "")
            {
                var messagedialog = new MessageDialog("detail can not be empty").ShowAsync();
            }
            else if (dp.Date < DateTime.Today)
            {
                var messagedialog = new MessageDialog("date is not correct").ShowAsync();
            }
            else
            {
                view_Module.add_item(tb_t.Text, tb_d.Text, dp.Date.DateTime, right_image.Source);


                Frame.Navigate(typeof(MainPage), view_Module);
            }
        }

        private void ud_click(object sender, RoutedEventArgs e)
        {
            if (view_Module.Select_item != null)
            {
                if (tb_t.Text == "")
                {
                    var messagedialog = new MessageDialog("title can not be empty").ShowAsync();

                }
                else if (tb_d.Text == "")
                {
                    var messagedialog = new MessageDialog("detail can not be empty").ShowAsync();
                }
                //view_Module.select_item.title = title_block.Text;
                //view_Module.select_item.detail = detail_block.Text;
                //view_Module.select_item.date = date;
                view_Module.update_item(tb_t.Text, tb_d.Text, dp.Date.DateTime, right_image.Source);
                Frame.Navigate(typeof(MainPage), view_Module);
            }
        }

        private void cc_click(object sender, RoutedEventArgs e)
        {
            tb_t.Text = "";
            tb_d.Text = "";
            dp.Date = DateTime.Today;
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            
            App.change_tile(view_Module.All_items.Last());
        }

        private void share_click(object sender, RoutedEventArgs e) {
            DataTransferManager.ShowShareUI();
        }

        private void setting_click(object sender, RoutedEventArgs e) {
            AppBarButton mf = sender as AppBarButton;
            if (mf != null) {
                view_Module.Select_item = mf.DataContext as module.module;
            }
        }

        //private void edit(object sender, RoutedEventArgs e)
        //{
        //    //view_Module.Select_item = (module.module)(e.);

        //    Frame.Navigate(typeof(EditPage), view_Module);
        //}

        //private void delete(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
