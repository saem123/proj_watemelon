using Saem;
using UniRx;
namespace Saem
{
    public class InputFieldChanged : RxInputField
    {

        protected override void initViewStream()
        {
            viewStream = applyComponent.OnValueChangedAsObservable();
        }

        protected override void publishedValue(string value)
        {
            changedValue(value);
        }

        protected virtual void changedValue(string value)
        {

        }
    }
}
