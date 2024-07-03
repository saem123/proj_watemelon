
using Saem;
using UniRx;
namespace Saem
{

    public class InputFieldEndEdit : RxInputField
    {

        protected override void initViewStream()
        {
            viewStream = applyComponent.OnEndEditAsObservable();
        }

        protected override void publishedValue(string value)
        {
            endEdit(value);
        }

        protected virtual void endEdit(string value)
        {

        }

    }
}

