using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using System.Reflection;

namespace Saem
{
    [Serializable]
    public class JsonList<T>
    {
        public List<T> items;
    }
    public partial class CommonParser
    {
        public static List<T> csvToJsonList<T>(string csv, int maxIndex = 30)
        {
            string jsonString = "[";
            List<string> lines = csv.Replace("\r", "").Split('\n').ToList();

            List<string> keys = lines[0].Split(',').ToList(); ;

            lines.RemoveAt(0);
            int index = 0;
            lines.ForEach(line =>
            {
                List<string> values = line.Split(',').ToList();

                int valueIndex = 0;
                if (index != 0) jsonString += ",";
                jsonString += "{";
                keys.ForEach(key =>
                {
                    if (valueIndex > maxIndex) return;
                    if (valueIndex != 0) jsonString += ",";

                    jsonString += "\"" + key + "\":";
                    jsonString += "\"" + values[valueIndex] + "\"";
                    valueIndex++;
                });
                jsonString += "}";

                index++;
            });
            jsonString += "]";

            return getJsonList<T>(jsonString);
        }

        public static string[] getPropertiesNames(Type t)
        {
            var properties = t.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance |
                BindingFlags.Public);
            List<string> keyValueList = new List<string>();
            foreach (var property in properties)
            {

                if (!property.Name.Equals(".ctor"))
                {
                    keyValueList.Add(property.Name);
                }

            }
            return keyValueList.ToArray();
        }

        public static List<T> csvToModelList<T>(string csvString, int skipRow = 1, int[] excludeArray = null) where T : class
        {
            string jsonString = csvToJson(getPropertiesNames(typeof(T)), csvString, skipRow, excludeArray);
            return getJsonList<T>(jsonString);
        }
        public static string[] fromCSV(string dataString)
        {
            return dataString.Split('\n');
        }
        public static string csvToJson(string[] keyArray, string csvString, int skipRow, int[] excludeArray)
        {
            return csvToJson(keyArray, fromCSV(csvString), skipRow, excludeArray);
        }
        public static string csvToJson(string[] keyArray, string[] valueArray, int skipRow = 1, int[] excludeArray = null)
        {
            string jsonString = "[";
            for (int rowIndex = skipRow; rowIndex < valueArray.Length; rowIndex++)
            {
                string row = valueArray[rowIndex];
                jsonString += rowToJson(row, keyArray, excludeArray);

                if (rowIndex < valueArray.Length - 1)
                    jsonString += ",";
            }
            jsonString += "]";

            return jsonString;
        }

        public static void csvMakeModelClass(string csv, string name)
        {

            List<string> lines = csv.Replace("\r", "").Split('\n').ToList();
            List<string> keys = lines[0].Split(',').ToList(); ;
            lines.RemoveAt(0);

            string classUsingString = "using System;" + "\n";
            string classString = "[Serializable]" + "\n";
            classString += "public class " + name + "{";



            List<string> values = lines[0].Split(',').ToList();

            int testInt;
            int index = 0;
            keys.ForEach(key =>
            {
                classString += "public ";

                if (Int32.TryParse(values[index], out testInt) == true)
                {
                    classString += "int ";
                }
                else
                {
                    classString += "string ";
                }

                classString += key.Replace("\"", "");
                classString += ";";
                classString += "\n";

                index++;
            });
            classString += "}";

            System.IO.File.WriteAllText(name + ".cs", classUsingString + classString);
        }

        private static List<T> getJsonList<T>(string json)
        {

            string jsonString = "{\"items\":" + json + "}";
            JsonList<T> jsonList = JsonUtility.FromJson<JsonList<T>>(jsonString);
            return jsonList.items;
        }

        public static T enumParse<T>(string name)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), name.ToUpper());
            }
            catch
            {
                return (T)Enum.Parse(typeof(T), "NONE");
            }


        }

        public static string getPascalName(string name)
        {
            List<string> nameArray = (name).Split("_"[0]).ToList();
            string pascalName = "";
            nameArray.ForEach(_ => pascalName += _.ToUpper().Substring(0, 1) + _.Substring(1));
            return pascalName;
        }
    }
}