﻿#pragma checksum "..\..\CreateTest.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CD53BA3245B57A986F48815951FDE32262B63A36"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NaukaSlowek;
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


namespace NaukaSlowek {
    
    
    /// <summary>
    /// CreateTest
    /// </summary>
    public partial class CreateTest : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridWords;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn ComboBoxColumnCategory;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn ComboBoxColumnParts;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelSettings;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonParts;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCategories;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelEdit;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonInsertWords;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\CreateTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonDeleteWord;
        
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
            System.Uri resourceLocater = new System.Uri("/NaukaSlowek;component/createtest.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CreateTest.xaml"
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
            
            #line 8 "..\..\CreateTest.xaml"
            ((NaukaSlowek.CreateTest)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            
            #line 8 "..\..\CreateTest.xaml"
            ((NaukaSlowek.CreateTest)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GridWords = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.ComboBoxColumnCategory = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 4:
            this.ComboBoxColumnParts = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 5:
            this.LabelSettings = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.ButtonParts = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\CreateTest.xaml"
            this.ButtonParts.Click += new System.Windows.RoutedEventHandler(this.SettingsParts);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonCategories = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\CreateTest.xaml"
            this.ButtonCategories.Click += new System.Windows.RoutedEventHandler(this.SettingsCategories_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.LabelEdit = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.ButtonInsertWords = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\CreateTest.xaml"
            this.ButtonInsertWords.Click += new System.Windows.RoutedEventHandler(this.ButtonInsertWords_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ButtonDeleteWord = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\CreateTest.xaml"
            this.ButtonDeleteWord.Click += new System.Windows.RoutedEventHandler(this.ButtonDeleteWord_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

