using System;
namespace Saem
{
    public class CommonFlag
    {

        public static int onState<T>(int origin, T state) where T : System.Enum
        {
            int typeCode = Convert.ToInt32(state);
            return onBit(origin, typeCode);
        }

        public static int offState<T>(int origin, T state) where T : System.Enum
        {
            int typeCode = Convert.ToInt32(state);
            return offBit(origin, typeCode);
        }

        public static bool isState<T>(int origin, T state) where T : System.Enum
        {
            int typeCode = Convert.ToInt32(state);
            return getBit(origin, typeCode);
        }

        public static bool getBit(int number, int loc)
        {
            int bit = 0x1 << loc;
            return (number & bit) == bit;
        }

        public static int onBit(int number, int loc)
        {
            int bit = 0x1 << loc;
            return number | bit;
        }

        public static int offBit(int number, int loc)
        {
            int bit = 0x01 << loc;
            return number & ~bit;
        }

        public static int setAreaBit(int number, int loc, int area)
        {
            int result = number;
            for (int index = loc; index < loc + area; index++)
            {
                result = onBit(result, index);
            }
            return result;
        }

        public static int countBit(int number)
        {
            int count = 0;
            while (number > 0)
            {
                number &= number - 1;
                count++;
            }
            return count;
        }

        public static bool checkFullBit(int number, int count)
        {
            return countBit(number) == count;
        }

    }

}

