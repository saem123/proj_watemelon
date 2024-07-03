using UnityEngine.UI;
using UnityEngine;
namespace Saem
{
    [RequireComponent(typeof(Text))]
    public class RxText<T> : RxUI<Text, T>
    {
        protected override void publishedValue(T value)
        {
            applyComponent.text = streamFormatter(value);
        }
        protected virtual string streamFormatter(T value)
        {
            return value.ToString();
        }

        public void setText(T value)
        {
            publishedValue(value);
        }
    }
}