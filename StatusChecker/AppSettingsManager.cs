using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json.Linq;

namespace StatusChecker
{
    public class AppSettingsManager
    {
        private static AppSettingsManager _instance;
        private JObject _settings;

        private const string Namespace = "StatusChecker";
        private const string FileName = "appsettings.json";


        private AppSettingsManager()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;
                var stream = assembly.GetManifestResourceStream($"{ Namespace }.{ FileName }");
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    _settings = JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string> {
                    { "Method", "AppSettingsManager" },
                    { "Event", "Unable to load AppSettings File" }
                };

                // TODO: Outsource to Service
                if(App.PermissionTrackErrors)
                {
                    Crashes.TrackError(ex, properties);
                }


                Analytics.TrackEvent(ex.Message);
                Debug.WriteLine("Unable to load appsettings file");
            }
        }


        public static AppSettingsManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettingsManager();
                }

                return _instance;
            }
        }


        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    var tempPath = path[0];

                    JToken node = _settings[tempPath];

                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception ex)
                {
                    var properties = new Dictionary<string, string> {
                        { "Method", "AppSettingsManager" },
                        { "Event", "Could not find AppSetting" }
                    };

                    if(App.PermissionTrackErrors)
                    {
                        Crashes.TrackError(ex, properties);
                    }


                    Debug.WriteLine($"Unable to retrieve appsetting '{ name }'");

                    return string.Empty;
                }
            }
        }
    }
}