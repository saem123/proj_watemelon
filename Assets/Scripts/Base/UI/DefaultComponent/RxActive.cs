namespace Saem
{
    public class RxActive : RxView<bool>
    {
        protected override void publishedValue(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}