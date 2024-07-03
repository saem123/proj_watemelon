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


        public void makeStreamClass(string pascalName)
        {

            string streamName = pascalName + "Stream";
            string classUsingString = makeUsing();
            string classString = classUsingString;
            classString += "public partial  class " + streamName + " : Stream<" + streamName + ">";
            classString += "\n{";
            classString += makeStateStreamCode(pascalName);
            classString += "\n}";

            makeClassFile(streamFolderPath + pascalName, streamName + ".cs", classString);
        }

        string makeStateStreamCode(string pascalName)
        {
            string codeString = "";
            string stateName = pascalName + "State";
            string stateFlagName = stateName + "Flag";
            string stateFlagPropertyName = "reactive" + stateFlagName;
            string serviceName = pascalName + "Service";
            codeString += "\npublic IObservable<bool> get" + stateName + "ActiveStream(" + stateName + " state)";
            codeString += "\n{";
            codeString += "\nreturn " + serviceName + ".instance.get" + stateName + "ActiveStream(state);";
            codeString += "\n}";

            return codeString;
        }

    }
}