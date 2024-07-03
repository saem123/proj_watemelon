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

        List<string> buttonNameList = new List<string>();


        public void makeButtonTypeEnum(string pascalName)
        {
            string className = pascalName + "ButtonType";
            string classString = "public enum " + className;
            classString += "\n{";
            buttonNameList.ForEach(_ => classString += _.ToUpper() + ",");
            classString += "\n}";

            makeClassFile(enumFolderPath, pascalName + "ButtonType.cs", classString);
        }
        public void makeButtonStreamClass(string pascalName)
        {

            string streamName = pascalName + "Stream";
            string classUsingString = makeUsing();
            string classString = classUsingString;
            classString += "public partial  class " + streamName + " : Stream<" + streamName + ">";
            classString += "\n{";
            classString += "\npublic void clickButton(" + pascalName + "ButtonType buttonType" + ")";
            classString += "\n{";
            classString += "\nswitch(buttonType)";
            classString += "\n{";
            buttonNameList.ForEach(_ => classString += makeButtonCaseString(_));
            classString += "\n}";
            classString += "\n}";
            buttonNameList.ForEach(_ => classString += makeButtonClickFunctionCode(_));
            classString += "\n}";

            makeClassFile(streamFolderPath + pascalName, streamName + ".click.cs", classString);
        }

        string makeButtonCaseString(string buttonName)
        {
            string caseString = "\ncase " + sceneName + "ButtonType." + buttonName.ToUpper() + ":";
            caseString += "\n" + makeButtonFunctionName(buttonName) + "();";
            caseString += "\nbreak;";
            return caseString;
        }

        string makeButtonFunctionName(string buttonName)
        {
            return "click" + buttonName + "Button";
        }

        string makeButtonClickFunctionCode(string buttonName)
        {
            string codeString = "";
            string functionName = makeButtonFunctionName(buttonName);
            string stateName = buttonName.ToUpper();
            string stateEnum = this.sceneName + "State";
            string serviceName = this.sceneName + "Service";
            codeString += "\npublic void " + functionName + "()";
            codeString += "\n{";
            codeString += "\n" + serviceName + ".instance.onState(" + stateEnum + "." + stateName + ");";
            codeString += "\n}";

            return codeString;
        }

        void addButton(string name)
        {
            string buttonString = CommonParser.getPascalName(name.Replace(" ", "_"));

            if (buttonNameList.IndexOf(buttonString) == -1)
            {
                buttonNameList.Add(buttonString);
            }
        }

        public void makeDefaultButtonClass(string pascalName)
        {
            string className = pascalName + "DefaultButton";
            string streamName = pascalName + "Stream";
            string classUsingString = makeUsing();
            string classString = classUsingString;
            classString += "public class " + className + " : DefaultTypeButton<" + pascalName + "ButtonType>";
            classString += "\n{";
            classString += "\nprotected override void onClick()";
            classString += "\n{";
            classString += "\n" + streamName + ".instance.clickButton(buttonType);";
            classString += "\n}";
            classString += "\n}";

            makeClassFile(buttonFolderPath + pascalName, className + ".cs", classString);
        }

        public RxButton makeButton(GameObject target, string name)
        {
            addButton(name);
            return makeRxActiveListButton(target);
        }

        public RxActiveListButton makeRxActiveListButton(GameObject target)
        {
            RxActiveListButton activeListButton = activeListButtonSetter(target.transform);
            if (activeListButton == null) return null;
            activeListSetter(target.transform, activeListButton);
            return activeListButton;
        }

        RxActiveListButton activeListButtonSetter(Transform targetTransform)
        {
            int start = 0;
            int count = targetTransform.childCount;
            string buttonName = targetTransform.name.Split("_"[0])[1].ToUpper();
            for (int index = start; index < count; index++)
            {
                GameObject childObject = targetTransform.GetChild(index).gameObject;
                string[] nameArray = childObject.name.Split("_"[0]);

                if (nameArray[0].Equals("button"))
                {
                    childObject.AddComponent(Type.GetType(sceneName + "DefaultButton"));
                    childObject.SendMessage("setButtonType", buttonName);
                    childObject.GetComponent<Image>().raycastTarget = true;
                    return childObject.AddComponent<RxActiveListButton>();

                }
            }

            for (int index = start; index < count; index++)
            {
                GameObject childObject = targetTransform.GetChild(index).gameObject;
                string[] nameArray = childObject.name.Split("_"[0]);

                if (nameArray.Length > 2 && nameArray[2].Equals("onoff"))
                {

                    childObject.AddComponent(Type.GetType(sceneName + "DefaultButton"));
                    childObject.SendMessage("setButtonType", buttonName);
                    childObject.GetComponent<Image>().raycastTarget = true;
                    return childObject.AddComponent<RxActiveListButton>();

                }
            }

            return null;
        }

        void activeListSetter(Transform targetTransform, RxActiveListButton activeListButton)
        {
            int start = 0;
            int count = targetTransform.childCount;

            for (int index = start; index < count; index++)
            {
                GameObject childObject = targetTransform.GetChild(index).gameObject;
                string[] nameArray = childObject.name.Split("_"[0]);
                string optionString = nameArray[nameArray.Length - 1];

                if (optionString.Equals("click") || optionString.Equals("on") || optionString.Equals("onclick"))
                {

                    activeListButton.addPressedObject(childObject);

                }

                if (!optionString.Equals("click"))
                {

                    activeListButton.addDefaultObject(childObject);

                }


            }
        }

    }
}