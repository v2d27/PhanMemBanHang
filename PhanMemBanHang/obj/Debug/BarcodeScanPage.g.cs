﻿#pragma checksum "..\..\BarcodeScanPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F9A96E4EB5365DC79976B71477063A0135F24B20"
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
    /// BarcodeScanPage
    /// </summary>
    public partial class BarcodeScanPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\BarcodeScanPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PhanMemBanHang.BarcodeScanPage page;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\BarcodeScanPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MaVach;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\BarcodeScanPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TenSanPham;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\BarcodeScanPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GiaTien;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\BarcodeScanPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackPanel;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\BarcodeScanPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Save;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\BarcodeScanPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Message;
        
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
            System.Uri resourceLocater = new System.Uri("/PhanMemBanHang;component/barcodescanpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BarcodeScanPage.xaml"
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
            this.page = ((PhanMemBanHang.BarcodeScanPage)(target));
            return;
            case 2:
            this.MaVach = ((System.Windows.Controls.TextBox)(target));
            
            #line 63 "..\..\BarcodeScanPage.xaml"
            this.MaVach.KeyDown += new System.Windows.Input.KeyEventHandler(this.InputMaVach_KeyDown);
            
            #line default
            #line hidden
            
            #line 63 "..\..\BarcodeScanPage.xaml"
            this.MaVach.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.InputMaVach_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TenSanPham = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\BarcodeScanPage.xaml"
            this.TenSanPham.KeyDown += new System.Windows.Input.KeyEventHandler(this.InputMaVach_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.GiaTien = ((System.Windows.Controls.TextBox)(target));
            
            #line 71 "..\..\BarcodeScanPage.xaml"
            this.GiaTien.KeyDown += new System.Windows.Input.KeyEventHandler(this.InputMaVach_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.stackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            
            #line 75 "..\..\BarcodeScanPage.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Refresh_MouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Save = ((System.Windows.Controls.Grid)(target));
            
            #line 78 "..\..\BarcodeScanPage.xaml"
            this.Save.KeyDown += new System.Windows.Input.KeyEventHandler(this.Save_KeyDown);
            
            #line default
            #line hidden
            
            #line 78 "..\..\BarcodeScanPage.xaml"
            this.Save.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Save_MouseUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Message = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
