using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Serilog.Editor
{
    static class SerilogSettingsProvider
    {
        public static readonly string SETTINGS_DIRECTORY = "Settings/";
        public static readonly string SETTINGS_FILE = $"Assets/{SETTINGS_DIRECTORY}SerilogSettings.asset";

        private static SerilogSettings settings;

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("Project/Serilog Logging", SettingsScope.Project)
            {
                label = "Serilog Logging",

                activateHandler = (searchContext, rootElement) =>
                {
                    settings = SerilogSettings.Instance;
                    var logLevelField = new EnumField("Log Level", settings.LogLevel);
                    logLevelField.RegisterCallback<ChangeEvent<Enum>>(SerilogSettings_OnValueChanged);
                    rootElement.Add(logLevelField);
                },
                keywords = new HashSet<string>(new[] { "Logging", "Serilog","Log Level" })
            };

            return provider;
        }

        public static SerilogSettings GetSettings()
        {
            return AssetDatabase.LoadAssetAtPath<SerilogSettings>(SETTINGS_FILE);
        }

        private static SerilogSettings GetOrCreateSettings()
        {
            settings = GetSettings();
            if (settings == null)
            {
                settings = SerilogSettings.CreateInstance<SerilogSettings>();

                string settingsPath = Application.dataPath + "/" + SETTINGS_DIRECTORY;
                if (!Directory.Exists(settingsPath)) Directory.CreateDirectory(settingsPath);
                AssetDatabase.CreateAsset(settings, SETTINGS_FILE);
                AssetDatabase.SaveAssets();

                var preloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets().ToList();
                preloadedAssets.Add(settings);
                UnityEditor.PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            }
            return settings;
        }

        private static void SerilogSettings_OnValueChanged( ChangeEvent<Enum> evt )
        {
            settings.LogLevel = (LogEventLevel)evt.newValue;
        }
    }

}
 