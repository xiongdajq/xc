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
using wjq_hw2.module1;
using Windows.ApplicationModel;
using Windows.Storage;


//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace wjq_hw2 {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page {
        module ViewModule = new module();
        private object coreTitleBar;

        public object SuspensionManager { get; private set; }

        public MainPage() {
            this.InitializeComponent();
            Application.Current.Suspending += OnSuspending;
            Application.Current.Resuming += OnResuming;
            //public module1.module ViewModule = new module1.module();
            /// TODO
            /// 
            /// 标题栏与内容融合， 类似酷狗。 然而要重写return.
            /*var coreTitleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            var appTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;*/

            var view = ApplicationView.GetForCurrentView();
            view.TitleBar.BackgroundColor = Colors.PaleTurquoise;
            view.TitleBar.ButtonBackgroundColor = Colors.PaleTurquoise;

        }

        private void add_buttonclick(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(EditPage));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
           
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
            module vm = e.Parameter as module;
            if (vm != null) {
                if (vm.ifchecked1 == "true") {
                    line.Visibility = Visibility.Visible;
                    check_box1.IsChecked = true;
                } else {
                    line.Visibility = Visibility.Collapsed;
                    check_box1.IsChecked = false;
                }
                if (vm.ifchecked2 == "true") {
                    line1.Visibility = Visibility.Visible;
                    check_box2.IsChecked = true;
                } else {
                    line1.Visibility = Visibility.Collapsed;
                    check_box2.IsChecked = false;
                }
            }
            
        }

        private void add_line(object sender, RoutedEventArgs e) {
            line.Visibility = Visibility.Visible;
            ViewModule.ifchecked1 = "true";
        }

        private void del_line(object sender, RoutedEventArgs e) {
            line.Visibility = Visibility.Collapsed;
            ViewModule.ifchecked1 = "false";
        }

        private void add_line1(object sender, RoutedEventArgs e) {
            line1.Visibility = Visibility.Visible;
            ViewModule.ifchecked2 = "true";
        }

        private void del_line1(object sender, RoutedEventArgs e) {
            line1.Visibility = Visibility.Collapsed;
            ViewModule.ifchecked2 = "false";
        }
        private void OnSuspending(object sender, SuspendingEventArgs e) {
            ApplicationData.Current.LocalSettings.Values["ifchecked1"] = ViewModule.ifchecked1;
            ApplicationData.Current.LocalSettings.Values["ifchecked2"] = ViewModule.ifchecked2;
            ViewModule.i = "MainPage";
            ApplicationData.Current.LocalSettings.Values["WhichPage"] = ViewModule.i;
        }
        private void OnResuming(object sender, object e) {
            ViewModule.ifchecked1 = ApplicationData.Current.LocalSettings.Values["ifcjecked1"] as string;
            ViewModule.ifchecked2 = ApplicationData.Current.LocalSettings.Values["ifchecked2"] as string;
        }
    }
}
