using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saem;
namespace Saem
{
    public class ErrorPopupActive : RxActive
    {
        protected override void initViewStream()
        {

            viewStream = ErrorService.instance.getErrorPopupActiveStream();

        }
    }
}