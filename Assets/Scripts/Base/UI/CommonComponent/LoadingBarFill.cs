namespace Saem
{

    public class LoadingBarFill : RxImageFillAmount
    {
        protected override void initViewStream()
        {
            viewStream = LoadingStream.instance.loadingValue();
        }
    }
}