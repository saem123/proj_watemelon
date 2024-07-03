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
        public void makeStateEnum(string pascalName)
        {
            string className = pascalName + "State";
            string classString = "public enum " + className;
            classString += "\n{";
            stateStringList.ForEach(_ => classString += _ + ",");
            classString += "\n}";

            makeClassFile(enumFolderPath, pascalName + ".cs", classString);
        }

        void addState(string name)
        {
            string stateString = name.ToUpper();
            if (stateStringList.IndexOf(stateString) == -1)
            {
                stateStringList.Add(stateString.Trim());
            }
        }

    }
}