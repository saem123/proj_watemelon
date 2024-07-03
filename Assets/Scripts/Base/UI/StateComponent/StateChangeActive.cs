namespace Saem
{
    public class StateChangeActive<T> : RxView<T> where T : System.Enum
    {
        public T activeState;

        protected override void publishedValue(T value)
        {
            gameObject.SetActive(value.Equals(activeState));
        }
    }
}