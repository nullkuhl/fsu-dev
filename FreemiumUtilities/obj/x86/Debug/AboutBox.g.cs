﻿#pragma checksum "..\..\..\AboutBox.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D05D2F279F259BDF36C861636FF20E50"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.468
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FreemiumUtilities;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLocalizeExtension.Engine;
using WPFLocalizeExtension.Extensions;


namespace FreemiumUtilities {
    
    
    /// <summary>
    /// AboutBox
    /// </summary>
    public partial class AboutBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 5 "..\..\..\AboutBox.xaml"
        internal FreemiumUtilities.AboutBox AboutUs;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\AboutBox.xaml"
        internal System.Windows.Controls.Border Inner;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FreemiumUtilities;component/aboutbox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AboutBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.AboutUs = ((FreemiumUtilities.AboutBox)(target));
            return;
            case 2:
            
            #line 32 "..\..\..\AboutBox.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Inner = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            
            #line 49 "..\..\..\AboutBox.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GoToSite);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

