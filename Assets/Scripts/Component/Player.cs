using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class Player : BaseUnit
{
    ReactiveProperty<Vector2> moveVec = new ReactiveProperty<Vector2>();

    void Start()
    { 
        // 플레이어 이동
        this.FixedUpdateAsObservable()
            .Select(_ => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")))
            //.Where(input => input.magnitude > 0.3f)
            .Subscribe(vec =>
            {
                moveVec.Value = vec;
                move(vec);
            });

        //방향전환
        moveVec
            .Where(_ => _.x != 0)
            .Subscribe(_ => spriteRenderer.flipX = _.x < 0)
            .AddTo(this);

        //애니메이션 전환
        moveVec
            .Subscribe(_ => animator.SetFloat("speed", _.magnitude))
            .AddTo(this);

    }


}
