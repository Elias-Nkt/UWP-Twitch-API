﻿#pragma checksum "C:\Users\Илья-2\documents\visual studio 2017\Projects\UWPTwitch\UWPTwitch\PivotPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F11D1B976D214E9F859806227983DF9D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UWPTwitch
{
    partial class PivotPage : 
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
            case 1: // PivotPage.xaml line 11
                {
                    this.txtbutton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.txtbutton).Click += this.txtbutton_Click;
                }
                break;
            case 2: // PivotPage.xaml line 12
                {
                    this.MyPivot = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                }
                break;
            case 3: // PivotPage.xaml line 24
                {
                    this.GridVideo = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // PivotPage.xaml line 21
                {
                    this.GridChannels = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5: // PivotPage.xaml line 15
                {
                    this.GridGame = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

