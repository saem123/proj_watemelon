// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Saem;
// using Spine.Unity;
// using System;
// namespace Saem
// {
//     public class RxSpineAnimation<T> : RxComponent<SkeletonGraphic, T> where T : Enum
//     {
//         string currentState = "";
//         protected override void publishedValue(T value)
//         {
//             string state = stateToString(value);
//             if (currentState.Equals(state)) return;
//             currentState = state;
//             playAnimation(currentState);
//         }

//         protected virtual string stateToString(T value)
//         {
//             return value.ToString();
//         }


//         protected void playAnimation(string animationName, bool isLoop = true)
//         {
//             applyComponent.AnimationState.SetAnimation(0, animationName, isLoop);
//         }
//     }
// }
