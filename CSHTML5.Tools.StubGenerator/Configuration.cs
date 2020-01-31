﻿using DotNetForHtml5.PrivateTools.AssemblyAnalysisCommon;
using StubGenerator.Common.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace StubGenerator.Common
{
    internal static class Configuration
    {
        // Path of the directory where the files will be generated
        internal static string PathOfDirectoryWhereFileAreGenerated = "";

        // Path of the directory where all DLLs referenced by the assemblies to analyze are located
        internal static string ReferencedAssembliesFolderPath = "";

        // Options for the generated code
        private static OutputOptions _outputOptions = OutputOptions.Default;
		internal static OutputOptions OutputOptions
        {
            get => _outputOptions;
            set => _outputOptions = value ?? throw new ArgumentNullException();
        }

        // Defines the XML or DLL file used to get what stuff is already supported
        private static Product _targetProduct = Product.OPENSILVER;
        internal static Product TargetProduct
        {
            get
            {
                return _targetProduct;
            }
            set
            {
                _targetProduct = value;

                switch (value)
                {
                    case Product.CSHTML5:
                        SupportedElementsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\CSHTML5\SupportedElements.xml");
                        break;
                    case Product.CSHTML5_V2:
                        SupportedElementsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\CSHTML5 V2\Bridge.dll");
                        break;
                    case Product.OPENSILVER:
                        SupportedElementsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\OpenSilver\netstandard.dll");
                        break;
                    default:
                        throw new ArgumentException("This product is not recognized.");
                }
            }
        }

        // Path of supportedElements file
        internal static string SupportedElementsPath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\OpenSilver\netstandard.dll");

        // Path of mscorlib assembly
        internal static string MscorlibFolderPath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\Silverlight\v5.0";

        // Path of SDK folder
        internal static string SdkFolderPath = @"C:\Program Files (x86)\Microsoft SDKs\Silverlight\v5.0\Libraries\Client";

        // Path of folder containing the assemblies we want to analyze
        internal static string AssembliesToAnalyzePath = "";

        // Methods to add manually because mono cecil does not detect them (a method is represented by a Tuple<string, string, string>(string assemblyName, string typeName, string methodName))
        internal static Tuple<string, string, string>[] MethodsToAddManuallyBecauseTheyAreUndetected = new Tuple<string, string, string>[0];

        // Add the option to add code directly into a specified type
        internal static Dictionary<string, Dictionary<string, HashSet<string>>> CodeToAddManuallyBecauseItIsUndetected = new Dictionary<string, Dictionary<string, HashSet<string>>>();

        internal static HashSet<string> ExcludedFiles = new HashSet<string>();

        // Types that do not have the same name in their original implementation and our implementation.
        internal static Dictionary<string, string> TypesThatNeedToBeRenamed = new Dictionary<string, string>()
        {
            { "System.ServiceModel.ClientBase`1", "CSHTML5_ClientBase`1" },
        };

        internal static string[] TypesThatNeedFullName = new string[]
        {
            "System.Windows.PropertyMetadata",
            "Telerik.Windows.PropertyMetadata",
            "System.Windows.Controls.ItemsControl",
            "Telerik.Windows.Controls.ItemsControl",
            "System.Windows.Controls.SelectionMode",
            "Telerik.Windows.Controls.SelectionMode",
        };

        internal static readonly HashSet<string> UrlNamespacesThatBelongToUserCode = new HashSet<string>();

        internal static readonly HashSet<string> AttributesToIgnoreInXamlBecauseTheyAreFromBaseClasses = new HashSet<string>();
    }
}
