﻿#pragma checksum "..\..\..\Views\CompactingProgress.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E2D4C9A6C10D392F05895A6C8EEDF08B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.468
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RegistryCompactor;
using RegistryCompactor.Properties;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace RegistryCompactor {
    
    
    /// <summary>
    /// CompactingProgress
    /// </summary>
    public partial class CompactingProgress : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\Views\CompactingProgress.xaml"
        internal System.Windows.Controls.Label CurrentStep;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Views\CompactingProgress.xaml"
        internal System.Windows.Controls.ProgressBar progressBar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Views\CompactingProgress.xaml"
        internal System.Windows.Controls.StackPanel CompactingTitle;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Views\CompactingProgress.xaml"
        internal System.Windows.Controls.TextBlock CurrentValue;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Views\CompactingProgress.xaml"
        internal RegistryCompactor.BottomControl footer;
        
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
            System.Uri resourceLocater = new System.Uri("/RegistryOptimizer;component/views/compactingprogress.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CompactingProgress.xaml"
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
            
            #line 17 "..\..\..\Views\CompactingProgress.xaml"
            ((RegistryCompactor.CompactingProgress)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CurrentStep = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.progressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 4:
            this.CompactingTitle = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.CurrentValue = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.footer = ((RegistryCompactor.BottomControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

