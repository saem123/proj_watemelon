namespace Saem
{

    public class RxNumberActive : RxView<int>
    {
        public int number, digit;

        protected override void publishedValue(int value)
        {
            bool condition = CommonUtility.getDigitNumber(value, digit) == number;
            condition = condition || value < 0 && number == 0;
            gameObject.SetActive(condition);
        }

    }
}
