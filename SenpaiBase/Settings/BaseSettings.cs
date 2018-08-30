using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SenpaiBase.Settings
{
    public class BaseSettings
    {
        /// <summary>
        /// checks if the settings hold a specific key
        /// </summary>
        protected bool ContainsKey(String key)
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey(key);
        }

        /// <summary>
        /// adds a new setting if its not already there
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void AddOption<T>(String key, T value)
        {
            if (!ContainsKey(key))
            {
                ApplicationData.Current.LocalSettings.Values.Add(key, value);
            }
        }
    }
}
