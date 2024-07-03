using UnityEngine;
namespace Saem
{

    public class DefaultTypeButton<T> : RxButton
    {

        [SerializeField]
        protected T buttonType;

        public void setButtonType(T buttonType)
        {
            this.buttonType = buttonType;
        }

        public void setButtonType(string buttonType)
        {
            setButtonType(CommonParser.enumParse<T>(buttonType));
        }
    }
}
