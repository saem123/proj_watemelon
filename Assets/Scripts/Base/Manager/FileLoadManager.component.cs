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


        public void makeStateActive(string pascalName)
        {
            string className = pascalName + "StateActive";
            string streamName = pascalName + "Stream";
            string classUsingString = makeUsing();
            string classString = classUsingString;
            classString += "public partial  class " + className + " : StateActive<" + pascalName + "State>";
            classString += "\n{";
            classString += "\nprotected override void initViewStream()";
            classString += "\n{";
            classString += "\nviewStream = " + streamName + ".instance.get" + className + "Stream(activeState);";
            classString += "\n}";
            classString += "\n}";

            makeClassFile(activeFolderPath + pascalName, className + ".cs", classString);
        }
        public RxActive makeStateActiveUI(GameObject target, string state, string activeClassName, string stateEnumName)
        {
            RxActive active = target.AddComponent(Type.GetType(activeClassName)) as RxActive;
            //active.setActiveState(CommonParser.enumParse<U>(state.ToUpper()));

            return active;
        }

        public Component makeComponent(GameObject target, string name)
        {
            string[] nameArray = name.Split("_"[0]);
            string optionString = nameArray[nameArray.Length - 1].ToLower();
            string categoryString = nameArray[0].ToLower();
            string nameString = "";
            string activeClassName = sceneName + "StateActive";
            string stateEnumName = sceneName + "State";
            switch (categoryString)
            {
                case "btn":
                    nameString = nameArray[1];
                    return makeButton(target, nameString);


                case "panel":
                case "popup":
                case "command":
                    nameString = nameArray[1];


                    addState(nameString);
                    return makeStateActiveUI(target, nameString, activeClassName, stateEnumName);

            }

            switch (optionString)
            {
                case "on":
                    nameString = nameArray[1];
                    addState(nameString);
                    return makeStateActiveUI(target, nameString, activeClassName, stateEnumName);

            }


            return null;
        }


    }
}