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
using Windows.UI.Popups;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.UI;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace wjq_hw2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EditPage : Page
    {
        public ImageSource img;
        private StorageFile async;

        public object Scenario2Image { get; private set; }

        public EditPage()
        {
            InitializeComponent();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            /*var coreTitleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            var appTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;*/
        }
        private view_module.view_module view_Module;
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

            view_Module = ((view_module.view_module)e.Parameter);
            if (view_Module.Select_item == null)
            {
                create_button.Visibility = Visibility.Visible;
                update_button.Visibility = Visibility.Collapsed;
            }
            else
            {
                create_button.Visibility = Visibility.Collapsed;
                update_button.Visibility = Visibility.Visible;
                title_block.Text = view_Module.Select_item.title;
                detail_block.Text = view_Module.Select_item.detail;
                images.Source = view_Module.Select_item.image;
                date.Date = view_Module.Select_item.date;
            }
        }
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private void create_button_Click(object sender, RoutedEventArgs e)
        {
            if (title_block.Text == "")
            {
                var messagedialog = new MessageDialog("title can not be empty").ShowAsync();

            }
            else if (detail_block.Text == "")
            {
                var messagedialog = new MessageDialog("detail can not be empty").ShowAsync();
            }
            else if (date.Date < DateTime.Today)
            {
                var messagedialog = new MessageDialog("date is not correct").ShowAsync();
            } else
            {
                view_Module.add_item(title_block.Text, detail_block.Text, date.Date.DateTime, images.Source);
                Frame.Navigate(typeof(MainPage), view_Module);
            }
           
        }

       private void check_data(object sender, DatePickerValueChangedEventArgs e)
        {
            /* if (date.Date < DateTime.Today)
            {
                var messagedialog = new MessageDialog("date is not correct").ShowAsync();
            }*/
        }

        private void cancle(object sender, RoutedEventArgs e)
        {
            title_block.Text = "";
            detail_block.Text = "";
            date.Date = DateTime.Today;
        }

        private async void select(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap 
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelWidth = 100; //match the target Image.Width, not shown
                    await bitmapImage.SetSourceAsync(fileStream);
                    images.Source = bitmapImage;
                    img = bitmapImage;
                }
            }

        }

        private void update_button_click(object sender, RoutedEventArgs e)
        {
            if (view_Module.Select_item != null)
            {
                if (title_block.Text == "")
                {
                    var messagedialog = new MessageDialog("title can not be empty").ShowAsync();

                }
                else if (detail_block.Text == "")
                {
                    var messagedialog = new MessageDialog("detail can not be empty").ShowAsync();
                }
                //view_Module.select_item.title = title_block.Text;
                //view_Module.select_item.detail = detail_block.Text;
                //view_Module.select_item.date = date;
                view_Module.update_item(title_block.Text, detail_block.Text, date.Date.DateTime, images.Source);
                Frame.Navigate(typeof(MainPage), view_Module);
            }
        }

        private void delete_button_click(object sender, RoutedEventArgs e)
        {
            if (view_Module.Select_item != null)
            {
                view_Module.remove_item(view_Module.select_item.id);
                Frame.Navigate(typeof(MainPage),view_Module);
            }
        }

        
    }
}

