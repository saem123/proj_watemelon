
using TMPro;
namespace Saem
{
    public class RxTMP<T> : RxUI<TMP_Text, T>
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