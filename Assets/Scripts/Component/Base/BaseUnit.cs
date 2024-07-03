using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BaseUnit : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected void Awake()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
    }

    protected void move(Vector2 vector)
    {
        Vector2 moveVec = vector.normalized * speed * Time.fixedDeltaTime;
        //위치이동
        rigid.MovePosition(rigid.position + moveVec);

    }

}
