﻿#pragma checksum "..\..\WindowPrintPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0241EE84F21DB5CB8A7A0B6FBE781F7F7EFB550A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PhanMemBanHang;
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
using System.Windows.Shell;


namespace PhanMemBanHang {
    
    
    /// <summary>
    /// WindowPrintPage
    /// </summary>
    public partial class WindowPrintPage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\WindowPrintPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.FlowDocument flowDocument;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\WindowPrintPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock datetime;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\WindowPrintPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock donhangso;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\WindowPrintPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Table table1;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\WindowPrintPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tongsotien;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\WindowPrintPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock khachhangtra;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\WindowPrintPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tiencondu;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\WindowPrintPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnPrint;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PhanMemBanHang;component/windowprintpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WindowPrintPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 14 "..\..\WindowPrintPage.xaml"
            ((System.Windows.Controls.FlowDocumentScrollViewer)(target)).Loaded += new System.Windows.RoutedEventHandler(this.FlowDocument_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.flowDocument = ((System.Windows.Documents.FlowDocument)(target));
            return;
            case 3:
            this.datetime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.donhangso = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.table1 = ((System.Windows.Documents.Table)(target));
            return;
            case 6:
            this.tongsotien = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.khachhangtra = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.tiencondu = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.BtnPrint = ((System.Windows.Controls.Button)(target));
            
            #line 94 "..\..\WindowPrintPage.xaml"
            this.BtnPrint.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
