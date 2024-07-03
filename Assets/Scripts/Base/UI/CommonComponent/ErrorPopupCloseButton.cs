// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// namespace Saem
// {
//     public class ErrorPopupCloseButton : RxButton
//     {
//         protected override void onClick()
//         {
//             ErrorService.instance.offErrorPopup();

//             Time.timeScale = SettingService.instance.getTimeScale();
//         }
//     }
// }