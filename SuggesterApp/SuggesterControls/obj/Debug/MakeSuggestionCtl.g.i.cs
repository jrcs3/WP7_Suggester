﻿#pragma checksum "C:\Users\jack\Documents\GitHub\WP7_Suggester\SuggesterApp\SuggesterControls\MakeSuggestionCtl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "484EDD3DDCD283FF77B18BF645838203"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SuggesterControls {
    
    
    public partial class MakeSuggestionCtl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock _txtTitle;
        
        internal System.Windows.Controls.Button _btnSuggest;
        
        internal System.Windows.Controls.TextBlock _txtSuggestion;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SuggesterControls;component/MakeSuggestionCtl.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this._txtTitle = ((System.Windows.Controls.TextBlock)(this.FindName("_txtTitle")));
            this._btnSuggest = ((System.Windows.Controls.Button)(this.FindName("_btnSuggest")));
            this._txtSuggestion = ((System.Windows.Controls.TextBlock)(this.FindName("_txtSuggestion")));
        }
    }
}

