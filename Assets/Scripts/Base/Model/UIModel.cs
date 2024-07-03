using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Saem;
using UniRx;
namespace Saem
{
    [Serializable]
    public class JsonData
    {
        public Attributes[] attributes;
    }

    [Serializable]
    public class Attributes
    {
        public string trait_type;
        public string value;
    }

    [Serializable]
    public class OriginCoords
    {
        public int top;
        public int bottom;
        public int left;
        public int right;
    }
    [Serializable]
    public class PartJsonData
    {
        public string path;
        public OriginCoords originCoords;
        public string imagePath;
        public int top;
        public int left;
        public int width;
        public int height;
    }
}