﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emojo.Lib.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Emojo.Lib.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 41bb2944418b4e0d97cee44411b35261.
        /// </summary>
        internal static string APIKey {
            get {
                return ResourceManager.GetString("APIKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to b9edde48cbe24132819414d1919d433c.
        /// </summary>
        internal static string APISecret {
            get {
                return ResourceManager.GetString("APISecret", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to qwertyuiop.
        /// </summary>
        internal static string AppId {
            get {
                return ResourceManager.GetString("AppId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to a0929f423c3f4c29af4d0271bd73a205.
        /// </summary>
        internal static string EmotionSubscriptionKey {
            get {
                return ResourceManager.GetString("EmotionSubscriptionKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://localhost.
        /// </summary>
        internal static string RedirectURI {
            get {
                return ResourceManager.GetString("RedirectURI", resourceCulture);
            }
        }
    }
}
