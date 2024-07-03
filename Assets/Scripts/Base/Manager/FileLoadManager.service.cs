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

        public void makeServiceClass(string pascalName)
        {
            makeServiceClassMain(pascalName);
            makeServiceClassState(pascalName);
        }

        public void makeServiceClassMain(string pascalName)
        {
            string serviceName = pascalName + "Service";
            string classUsingString = makeUsing();

            string classString = classUsingString;
            classString += "public partial class " + serviceName + " : Service<" + serviceName + ">";
            classString += "\n{";
            classString += "\n";
            classString += "\n}";

            makeClassFile(serviceFolderPath + pascalName, serviceName + ".cs", classString);
        }

        public void makeServiceClassState(string pascalName)
        {
            string serviceName = pascalName + "Service";
            string classUsingString = makeUsing();

            string classString = classUsingString;
            classString += "public partial class " + serviceName + " : Service<" + serviceName + ">";
            classString += "\n{";
            classString += makeFlagStateCode(pascalName);
            classString += "\n}";

            makeClassFile(serviceFolderPath + pascalName, serviceName + ".state.cs", classString);
        }

        string makeFlagStateCode(string pascalName)
        {
            string codeString = "";
            string stateName = pascalName + "State";
            string stateFlagName = stateName + "Flag";
            string stateFlagPropertyName = "reactive" + stateFlagName;
            codeString += makeReactiveProperty<int>(stateFlagName, "1");
            codeString += makeGetRxPropertyFunction<int>(stateFlagName);

            codeString += "\npublic IObservable<bool> get" + stateName + "ActiveStream(" + stateName + " state)";
            codeString += "\n{";
            codeString += "\nreturn " + stateFlagPropertyName + ".Select(currentState => CommonFlag.isState<" + stateName + ">(currentState, state));";
            codeString += "\n}";

            codeString += "\npublic void onState(" + stateName + " state)";
            codeString += "\n{";
            codeString += "\n" + stateFlagPropertyName + ".Value = CommonFlag.onState<" + stateName + ">(" + stateFlagPropertyName + ".Value, state);";
            codeString += "\n}";

            codeString += "\npublic void offState(" + stateName + " state)";
            codeString += "\n{";
            codeString += "\n" + stateFlagPropertyName + ".Value = CommonFlag.offState<" + stateName + ">(" + stateFlagPropertyName + ".Value, state);";
            codeString += "\n}";

            codeString += "\npublic void setState(" + stateName + " state)";
            codeString += "\n{";
            codeString += "\n" + stateFlagPropertyName + ".Value = CommonFlag.onState<" + stateName + ">(0, state);";
            codeString += "\n}";
            return codeString;
        }

    }
}