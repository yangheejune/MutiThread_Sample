﻿#pragma checksum "..\..\..\..\..\MVVM\View\LoginPerformance.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E0FCBB4F8B1B8125B35795303A630B890ECA05F9"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using PerformanceTestTools.Core;
using PerformanceTestTools.MVVM.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace PerformanceTestTools.MVVM.View {
    
    
    /// <summary>
    /// LoginPerformance
    /// </summary>
    public partial class LoginPerformance : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UserID;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox UserPW;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PerformanceTestTools.Core.ImageButton startBtn;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ThreadCount;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PerformanceTestTools.Core.ImageButton UpCount;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PerformanceTestTools.Core.ImageButton DownCount;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PerformanceTestTools;component/mvvm/view/loginperformance.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.UserPW = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.startBtn = ((PerformanceTestTools.Core.ImageButton)(target));
            return;
            case 4:
            this.ThreadCount = ((System.Windows.Controls.TextBox)(target));
            
            #line 72 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
            this.ThreadCount.GotFocus += new System.Windows.RoutedEventHandler(this.ThreadCount_GotFocus);
            
            #line default
            #line hidden
            
            #line 72 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
            this.ThreadCount.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ThreadCount_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 72 "..\..\..\..\..\MVVM\View\LoginPerformance.xaml"
            this.ThreadCount.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ThreadCount_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.UpCount = ((PerformanceTestTools.Core.ImageButton)(target));
            return;
            case 6:
            this.DownCount = ((PerformanceTestTools.Core.ImageButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
