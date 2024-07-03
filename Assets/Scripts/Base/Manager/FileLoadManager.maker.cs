using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Collections.Generic;
using UnityEngine;
using Saem;
using UnityEngine.UI;

namespace Saem
{

    public partial class FileLoadManager : MonoBehaviour
    {
        List<string> stateStringList = new List<string>();

        static string scriptFolderPath = "assets/scripts/";
        string enumFolderPath = scriptFolderPath + "enum/";
        string streamFolderPath = scriptFolderPath + "stream/";
        string serviceFolderPath = scriptFolderPath + "service/";

        static string uiFolderPath = scriptFolderPath + "UI/";
        string activeFolderPath = uiFolderPath + "active/";
        string buttonFolderPath = uiFolderPath + "button/";

        void makeUIClasses()
        {

            makeServiceClass(sceneName);
            makeStreamClass(sceneName);
            makeStateEnum(sceneName);
            makeStateActive(sceneName);
            makeButtonTypeEnum(sceneName);
            makeButtonStreamClass(sceneName);
            makeDefaultButtonClass(sceneName);
        }

        void makeClassFile(string classPath, string name, string classText)
        {
            DirectoryInfo serviceFolder = new DirectoryInfo(classPath);
            if (!serviceFolder.Exists)
            {
                serviceFolder.Create();
            }

            FileInfo file = new FileInfo(classPath + "/" + name);
            if (!file.Exists)
            {
                File.WriteAllText(classPath + "/" + name, classText);
            }

        }

        void replaceClassFile(string classPath, string name, string classText)
        {
            DirectoryInfo serviceFolder = new DirectoryInfo(classPath);
            if (!serviceFolder.Exists)
            {
                serviceFolder.Create();
            }

            FileInfo file = new FileInfo(classPath + "/" + name);
            File.WriteAllText(classPath + "/" + name, classText);

        }

        string makeUsing()
        {
            string classUsingString = "using System;" + "\n";
            classUsingString += "using Saem;" + "\n";
            classUsingString += "using UniRx;" + "\n";
            classUsingString += "using UnityEngine;" + "\n";

            return classUsingString;
        }

        string makeReactiveProperty<T>(string propertyName, string firstValue)
        {
            return "\nReactiveProperty<" + typeof(T).ToString() + "> reactive" + propertyName + " = new ReactiveProperty<" + typeof(T).ToString() + ">(" + firstValue + ");";
        }

        string makeGetRxPropertyFunction<T>(string propertyName)
        {
            string classString = "";
            classString += "\npublic IObservable<" + typeof(T).ToString() + "> get" + propertyName + "Stream()";
            classString += "\n{";
            classString += "\nreturn reactive" + propertyName + ";";
            classString += "\n}";
            return classString;
        }

    }
}