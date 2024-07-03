using UnityEngine;
using Saem;
using System;
using System.IO;
using System.Collections.Generic;
namespace Saem
{
    public class CommonUtility
    {
        static System.Random random = new System.Random();
        public static int getDigitNumber(int number, int digit)
        {

            int result = (number / (int)Mathf.Pow(10, digit - 1)) % 10;
            return result;
        }


        public static void fakeModelMaker<T>(T model, string url)
        {
            string jsonString = JsonUtility.ToJson(model);
            jsonFileMaker(url, jsonString);
        }

        public static void jsonFileMaker(string url, string jsonString)
        {
            string[] hierarchy = url.ToLower().Split("/"[0]);
            hierarchy[hierarchy.Length - 1] = null;
            if (hierarchy.Length > 1)
            {
                string path = "Assets/Resources/server" + string.Join("/", hierarchy);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }



            File.WriteAllText("Assets/Resources/server" + url + ".json", jsonString);
        }

        public static string listToJsonList<T>(List<T> list)
        {
            string jsonString = "[" + JsonUtility.ToJson(list[0]);
            for (int index = 1; index < list.Count; index++)
            {
                jsonString += ",\n";
                jsonString += JsonUtility.ToJson(list[index]);
            }


            jsonString += "]";
            return jsonString;
        }

        public static int getInt(string key, int defaultValue = 0)
        {

            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static float getFloat(string key, float defaultValue = 0)
        {

            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static string getString(string key, string defaultValue = "")
        {

            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static void setInt(string key, int value)
        {

            PlayerPrefs.SetInt(key, value);
        }

        public static void setFloat(string key, float value)
        {

            PlayerPrefs.SetFloat(key, value);
        }

        public static void setString(string key, string value)
        {

            PlayerPrefs.SetString(key, value);
        }

        public static T randomEnum<T>()
        {
            Array enumValues = Enum.GetValues(typeof(T));
            return randomEnum<T>(enumValues.Length);
        }

        public static T randomEnum<T>(int maxExclusive)
        {

            return randomEnum<T>(0, maxExclusive);
        }

        public static T randomEnum<T>(int minInclusive, int maxExclusive)
        {
            Array enumValues = Enum.GetValues(typeof(T));
            return (T)enumValues.GetValue(random.Next(minInclusive, maxExclusive));
        }

        public static int getEnumValue<T>(T value)
        {

            return (int)Enum.Parse(value.GetType(), value.ToString());
        }
    }
}

