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
using wjq_hw2.module1;
using Windows.ApplicationModel;
using Windows.Graphics.Imaging;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace wjq_hw2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EditPage : Page {
        private StorageFile async;
        module ViewModule = new module();
        public object Scenario2Image { get; private set; }

        public EditPage() {
            InitializeComponent();
            Application.Current.Suspending += OnSuspending;
            Application.Current.Resuming += OnResuming;
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            /*var coreTitleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            var appTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;*/
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            ViewModule.i = "EditPage";
            ViewModule.Date_Time = DateTime.Today;
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack) {
               
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            } else {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
            module m = e.Parameter as module;
            if (m != null) {
                if (m.title != null)
                    title_block.Text = m.title;
                if (m.detail != null)
                    detail_block.Text = m.detail;
                if (m.Date_Time != null)
                    date.Date = m.Date_Time;
            }
        }
        private void App_BackRequested(object sender, BackRequestedEventArgs e) {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false) {
                e.Handled = true;
                rootFrame.GoBack();
               
            }
        }

        private void create_button_Click(object sender, RoutedEventArgs e) {
            if (title_block.Text == "") {
                var messagedialog = new MessageDialog("title can not be empty").ShowAsync();

            } else if (detail_block.Text == "") {
                var messagedialog = new MessageDialog("detail can not be empty").ShowAsync();
            } else if (date.Date < DateTime.Today) {
                var messagedialog = new MessageDialog("date is not correct").ShowAsync();
            }
        }

        private void check_data(object sender, DatePickerValueChangedEventArgs e) {
            ViewModule.Date_Time = date.Date.DateTime;
        }

        private void cancle(object sender, RoutedEventArgs e) {
            title_block.Text = "";
            detail_block.Text = "";
            date.Date = DateTime.Today;
        }

        private async void select(object sender, RoutedEventArgs e) {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null) {
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read)) {
                    // Set the image source to the selected bitmap 
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelWidth = 100; //match the target Image.Width, not shown
                    await bitmapImage.SetSourceAsync(fileStream);
                    images.Source = bitmapImage;

                    

                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile sampleFile = await storageFolder.CreateFileAsync("sample.txt", CreationCollisionOption.ReplaceExisting);

                    Stream x = await sampleFile.OpenStreamForWriteAsync();
                    /////totototototododododododod
                }

               
            }

        }
        
        private void save_title(object sender, TextChangedEventArgs e) {
            ViewModule.title = title_block.Text;
        }
        private void OnSuspending(object sender, SuspendingEventArgs e) {
            ApplicationData.Current.LocalSettings.Values["title"] = ViewModule.title;
            ApplicationData.Current.LocalSettings.Values["detail"] = ViewModule.detail;
            ApplicationData.Current.LocalSettings.Values["date"] = ViewModule.Date_Time.GetDateTimeFormats('r')[0];
            ApplicationData.Current.LocalSettings.Values["WhichPage"] = ViewModule.i;
        }
        private void OnResuming(object sender, object e) {
            ViewModule.title = ApplicationData.Current.LocalSettings.Values["title"] as string;
            title_block.Text = ViewModule.title;
            detail_block.Text = ApplicationData.Current.LocalSettings.Values["detail"] as string;
            date.Date = DateTime.Parse(ApplicationData.Current.LocalSettings.Values["date"] as string);

        }
        private void save_detail(object sender, TextChangedEventArgs e) {
            ViewModule.detail = detail_block.Text;
        }

        private void save_image(object sender, RoutedEventArgs e) {

        }

        //public void SavePhoto(string ImagePath) {
        //    BitmapImage objImage = new BitmapImage(new Uri(ImagePath, UriKind.RelativeOrAbsolute));
        //    objImage.DownloadCompleted += objImage_DownloadCompleted;
        //}
        //private void objImage_DownloadCompleted(object sender, EventArgs e) {
        //    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        //    Guid photoID = System.Guid.NewGuid();
        //    String photolocation = photoID.ToString() + ".jpg";  //file name 

        //    encoder.Frames.Add(BitmapFrame.Create((BitmapImage)sender));

        //    using (var filestream = new FileStream(photolocation, FileMode.Create))
        //        encoder.Save(filestream);
        //}


    }
   
}

