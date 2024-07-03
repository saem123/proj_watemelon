namespace Saem
{
    public class ErrorMessageText : RxText<string>
    {

        protected override void initViewStream()
        {

            viewStream = ErrorService.instance.getErrorMessageStream();
        }
    }
}
