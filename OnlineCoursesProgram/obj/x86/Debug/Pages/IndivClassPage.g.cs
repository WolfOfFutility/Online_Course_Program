﻿#pragma checksum "C:\Users\ceeji\Documents\Personal Coding Projects\.Net\OnlineCoursesProgram\OnlineCoursesProgramApp\OnlineCoursesProgram\Pages\IndivClassPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6CE90FC4060BA88D4922DEA6351C8D3B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineCoursesProgram
{
    partial class IndivClassPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\IndivClassPage.xaml line 17
                {
                    this.lessonTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3: // Pages\IndivClassPage.xaml line 32
                {
                    this.videoPlayer = (global::Windows.UI.Xaml.Controls.MediaPlayerElement)(target);
                }
                break;
            case 4: // Pages\IndivClassPage.xaml line 28
                {
                    this.jumpBackButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.jumpBackButton).Click += this.JumpBack;
                }
                break;
            case 5: // Pages\IndivClassPage.xaml line 29
                {
                    this.playVideoButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.playVideoButton).Click += this.PlayVideo;
                }
                break;
            case 6: // Pages\IndivClassPage.xaml line 30
                {
                    this.jumpForwardButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.jumpForwardButton).Click += this.JumpForward;
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
