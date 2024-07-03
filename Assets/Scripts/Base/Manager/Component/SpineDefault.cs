// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Saem;
// using Spine.Unity;
// namespace Saem
// {
//     public class SpineDefault : MonoBehaviour
//     {

//         protected SkeletonGraphic skeletonGraphic;

//         private void Awake()
//         {
//             skeletonGraphic = GetComponent<SkeletonGraphic>();
//         }

//         protected void changeSkin(int skinNumber)
//         {
//             skeletonGraphic.Skeleton.SetSkin(skeletonGraphic.SkeletonData.Skins.Items[skinNumber]);
//         }

//         protected void playAnimation(int animationNumber, bool isLoop = false)
//         {
//             skeletonGraphic.AnimationState.SetAnimation(0, skeletonGraphic.SkeletonData.Animations.Items[animationNumber], isLoop);
//         }

//         protected void playAnimation(string animationName, bool isLoop = false)
//         {
//             skeletonGraphic.AnimationState.SetAnimation(0, animationName, isLoop);
//         }

//     }
// }