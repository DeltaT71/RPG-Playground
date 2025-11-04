using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utility
{
    public static class PlayerPrefsUtility
    {
        public static void SetString(string key, List<string> value)
        {
            string formatterdValue = String.Join(",", value);

            PlayerPrefs.SetString(key, formatterdValue);
        }

        public static List<string> GetString(string key)
        {
            string unfromattedValue = PlayerPrefs.GetString(key);

            List<string> formattedValue = new List<string>(unfromattedValue.Split(","));

            if (unfromattedValue.Length == 0 && formattedValue.Count == 1)
            {
                formattedValue.RemoveAt(0);
            }

            return formattedValue;
        }
    }
}

