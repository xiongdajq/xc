using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net;
using Windows.UI.ViewManagement;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(700, 800));
        }
        private async void GetPosition(string number)
        {

            string GetPos = "http://apis.baidu.com/apistore/mobilenumber/mobilenumber?phone=" + PhoneNumber.Text;
            HttpWebRequest request; 
            request = (HttpWebRequest)WebRequest.Create(GetPos); //创建一个HttpWebRequest对象 
            request.Method = "GET"; //设置请求的类型
            request.Headers["apikey"] = "4c86c76e669262c052d04fd6828e1525"; //增加一个apikey
            WebResponse response = await request.GetResponseAsync(); //异步返回响应的数据流
            Stream s;
            s = response.GetResponseStream();
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            string StrDate = "";
            string strValue = "";
            while ((StrDate = Reader.ReadLine()) != null) //读取数据流
            {
                strValue += StrDate ;
            }
            JsonObject root = JsonValue.Parse(strValue).GetObject(); //将得到的数据转化为JsonObject对象
            city.Text = root.GetNamedObject("retData").GetNamedString("city");
            supplier.Text = root.GetNamedObject("retData").GetNamedString("supplier");
            province.Text = root.GetNamedObject("retData").GetNamedString("province");
        }

        private void search_click(object sender, RoutedEventArgs e)
        {
            supplier.Text = "";
            city.Text = "";
            province.Text = "";
            GetPosition(PhoneNumber.Text);
        }

    
    }
}
