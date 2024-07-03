using System;
using UnityEngine;
using Saem;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UniRx;
using System.Reflection;

namespace Saem
{
    public partial class CommonParser
    {

        static string rowToJson(string row, string[] keyArray, int[] excludeArray = null)
        {
            string jsonString = "{";
            string rowString = row.Replace("\",\"", "\"\t\"");
            string[] values = rowString.Split('\t');
            int exclude = 0;
            int keyIndex = 0;
            for (int index = 0; index < values.Length; index++)
            {
                if (excludeArray != null && excludeArray.Length > exclude)
                {
                    if (excludeArray[exclude] == index)
                    {
                        exclude++;
                        continue;
                    }
                }



                string key = keyArray[keyIndex];
                string value = "\"\"";

                value = values[index];

                if (index != 0)
                    jsonString += ",\n";
                jsonString += "\"" + key + "\":" + value;

                keyIndex++;
                if (keyIndex >= keyArray.Length) break;
            }

            jsonString += "}";

            return jsonString;
        }
    }
}