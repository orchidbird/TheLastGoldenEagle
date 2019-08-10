using System;
using UnityEngine;

public class Eagle : MonoBehaviour {
    public float speed;
    public float jumpPower;
    SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    enum MoveState{None, Jumping, Falling, Walking}
    MoveState moveState;
    void Update() {
        if (Input.GetKey(KeyCode.A)) 
            HorizontalMove(false);
        if (Input.GetKey(KeyCode.D))
            HorizontalMove(true);

        var animator = GetComponent<Animator>();
        var rigid = GetComponent<Rigidbody2D>(); 
        if (moveState == MoveState.None && Input.GetKeyDown(KeyCode.Space)) {
            rigid.velocity = rigid.velocity + Vector2.up * jumpPower;
            animator.SetTrigger("Jump");
            moveState = MoveState.Jumping;
        }

        if (moveState == MoveState.Jumping && rigid.velocity.y < 0)
            moveState = MoveState.Falling;
        if (moveState != MoveState.None && rigid.velocity.y == 0) {
            moveState = MoveState.None;
            animator.SetTrigger("Jump");
        }
        if(moveState == MoveState.Walking && rigid.velocity.x == 0)
            animator.SetTrigger("Stop");
    }

    void HorizontalMove(bool right){
        transform.Translate(right ? Vector3.right : Vector3.left *Time.deltaTime*speed);
        sprite.flipX = right;
        if (moveState == MoveState.None) {
            moveState = MoveState.Walking;
            GetComponent<Animator>().SetTrigger("Walk");
        }
    }
}