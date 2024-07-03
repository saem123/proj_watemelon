using System;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Collections.Generic;
using UnityEngine;
using Saem;
using UnityEngine.UI;

namespace Saem
{
    [Serializable]
    class PSDObject
    {
        public int left;
        public int right;
        public int top;
        public int bottom;
    }

    [Serializable]
    class PSDNode : PSDObject
    {
        public string type;
        public bool visible;
        public float opacity;
        public string blendingMode;
        public string name;
        public int height;
        public int width;

    }
    [Serializable]
    class PSDTree : PSDNode
    {
        public PSDText text;
        public List<PSDTree> children;
    }

    [Serializable]
    class PSDText : PSDObject
    {
        public string value;
        public PSDFont font;

    }

    [Serializable]
    class PSDFont// : ISerializable
    {
        public List<string> lengthArray;
        public List<string> styles;
        public List<string> weights;
        public List<string> names;
        public List<float> sizes;
        public List<string> colors;
        public List<string> alignment;
        public List<string> textDecoration;
        public List<float> leading;


        public PSDFont() { }
        protected PSDFont(SerializationInfo info, StreamingContext context)
        {
            lengthArray = (List<string>)info.GetValue("lengthArray", typeof(List<string>));
            styles = (List<string>)info.GetValue("styles", typeof(List<string>));
            weights = (List<string>)info.GetValue("weights", typeof(List<float>));
            sizes = (List<float>)info.GetValue("sizes", typeof(List<float>));
            object colorTo = info.GetValue("colors", typeof(object));
            alignment = (List<string>)info.GetValue("alignment", typeof(List<string>));
            textDecoration = (List<string>)info.GetValue("textDecoration", typeof(List<string>));
            leading = (List<float>)info.GetValue("leading", typeof(List<float>));
            Custom.Log(colors);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("lengthArray", lengthArray);
            info.AddValue("styles", styles);
            info.AddValue("weights", weights);
            info.AddValue("sizes", sizes);
            info.AddValue("colors", colors);
            info.AddValue("alignment", alignment);
            info.AddValue("textDecoration", textDecoration);
            info.AddValue("leading", leading);
        }
    }

    public partial class FileLoadManager : MonoBehaviour
    {
        public Canvas 캔버스;
        public GameObject 생성용프리팹, 텍스트프리팹;
        List<TextAsset> json파일들;

        public string 생성할폴더이름 = "popup_result_ui_1920x1080_v2";
        public string sceneName;
        public float 원본가로 = 2560;
        public float 원본세로 = 1440;

        public float 기준가로 = 1280;
        public float 기준세로 = 720;

        public bool isMakeScript;

        void Start()
        {
            sceneName = CommonParser.getPascalName(생성할폴더이름);
            stateStringList.Add("NONE");
            UI생성();
            if (isMakeScript == true)
                makeUIClasses();
        }

        void UI생성()
        {
            TextAsset 구조 = Resources.Load<TextAsset>(생성할폴더이름 + "/" + 생성할폴더이름);

            PSDTree tree = JsonUtility.FromJson<PSDTree>(구조.text);

            RectTransform root = 작업(tree, 캔버스.transform, 생성할폴더이름).GetComponent<RectTransform>();

            while (root.childCount > 0)
            {
                root.GetChild(0).SetParent(캔버스.transform);
            }

            Destroy(root.gameObject);
        }

        GameObject 작업(PSDTree 트리, Transform 부모, string 경로)
        {

            if (트리.name != null && 트리.name.Contains("backup"))
                return null;

            return 이미지작업(트리, 부모, 경로);


        }

        void 그룹작업(PSDTree 트리, Transform 부모, string 경로)
        {

            for (int index = 트리.children.Count - 1; index >= 0; index--)
            {
                작업(트리.children[index], 부모, 경로);
            }
        }

        TextAnchor GetTextAnchor(string alignment)
        {
            switch (alignment)
            {
                case "left":
                    return TextAnchor.MiddleLeft;

                case "right":
                    return TextAnchor.MiddleRight;

                default:

                    return TextAnchor.MiddleCenter;

            }

        }

        GameObject 이미지작업(PSDTree 트리, Transform 부모, string 경로)
        {
            GameObject 생성된객체 = Instantiate(생성용프리팹, 부모);
            생성된객체.name = 트리.name;
            string 이미지경로 = 경로;

            if (생성된객체.name != null && 생성된객체.name != "")
            {
                부모.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                Destroy(부모.GetComponent<Image>());
                이미지경로 = 경로 + "/" + 생성된객체.name;
            }
            else
            {
                생성된객체.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                Destroy(부모.GetComponent<Image>());
            }

            Sprite sprite = Resources.Load<Sprite>(이미지경로);
            Image 생성된객체의이미지 = 생성된객체.GetComponent<Image>();
            생성된객체의이미지.sprite = sprite;
            Color 투명값 = Color.white;
            투명값.a = 트리.opacity;
            생성된객체의이미지.color = 투명값;

            Vector3 position = new Vector3(트리.left * (기준가로 / (float)원본가로), 기준세로 - 트리.top * (기준세로 / (float)원본세로), 0);

            Vector3 center = position + Vector3.right * 트리.width / 2.0f + Vector3.down * 트리.height / 2.0f;

            //setAnchor(center, 생성된객체의이미지.rectTransform);
            생성된객체의이미지.transform.position = position;

            생성된객체의이미지.rectTransform.sizeDelta = new Vector2(트리.width * (기준가로 / (float)원본가로), 트리.height * (기준세로 / (float)원본세로));

            if (트리.children.Count > 0)
            {
                그룹작업(트리, 생성된객체.transform, 이미지경로);

            }

            makeComponent(생성된객체, 생성된객체.name);

            return 생성된객체;
        }

        void setAnchor(Vector3 center, RectTransform rectTransform)
        {
            Vector2 anchorMin = Vector2.one * 0.5f;
            Vector2 anchorMax = Vector2.one * 0.5f;

            if (center.x < 기준가로 * 0.3f)
            {
                anchorMin.x = 0;
                anchorMax.x = 0;
            }

            if (center.x > 기준가로 * 0.7f)
            {
                anchorMin.x = 1;
                anchorMax.x = 1;
            }

            if (center.y < 기준세로 * 0.3f)
            {
                anchorMin.y = 0;
                anchorMax.y = 0;
            }

            if (center.y > 기준세로 * 0.7f)
            {
                anchorMin.y = 1;
                anchorMax.y = 1;

            }

            rectTransform.anchorMax = anchorMax;
            rectTransform.anchorMin = anchorMin;
        }

    }
}