﻿#pragma checksum "C:\Users\Cindy\Documents\Visual Studio 2015\Projects\xc\HW2_code\wjq_hw2\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4E89CD2C966E4B23366B06BB62B4F905"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wjq_hw2
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.textBlocks = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 2:
                {
                    this.scoll = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 3:
                {
                    this.addbutton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 75 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.addbutton).Click += this.add_buttonclick;
                    #line default
                }
                break;
            case 4:
                {
                    this.check_box2 = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    #line 54 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.check_box2).Checked += this.add_line1;
                    #line 54 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.check_box2).Unchecked += this.del_line1;
                    #line default
                }
                break;
            case 5:
                {
                    this.line1 = (global::Windows.UI.Xaml.Shapes.Line)(target);
                }
                break;
            case 6:
                {
                    this.check_box1 = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    #line 31 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.check_box1).Checked += this.add_line;
                    #line 31 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.check_box1).Unchecked += this.del_line;
                    #line default
                }
                break;
            case 7:
                {
                    this.line = (global::Windows.UI.Xaml.Shapes.Line)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
