using System;
using System.Linq;
using System.Collections.Generic;
namespace Saem
{
    public class CommonList
    {
        public static IOrderedEnumerable<GenericList> orderBy<GenericGetType, GenericList>(List<GenericList> sortList, bool isOrder, Func<GenericList, GenericGetType> getter)
        {
            if (isOrder)
            {
                return sortList.OrderBy(getter);
            }
            else
            {
                return sortList.OrderByDescending(getter);
            }

        }

        public static IOrderedEnumerable<GenericList> thenBy<GenericGetType, GenericList>(IOrderedEnumerable<GenericList> sortList, bool isOrder, Func<GenericList, GenericGetType> getter)
        {
            if (isOrder)
            {
                return sortList.ThenBy(getter);
            }
            else
            {
                return sortList.ThenByDescending(getter);
            }

        }
    }
}