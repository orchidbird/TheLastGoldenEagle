using System;
using UnityEngine;

public class Eagle : MonoBehaviour {
    public float speed;
    public float jumpPower;
    SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    enum JumpState{None, Up, Down}
    JumpState jumping;
    void Update() {
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left*Time.deltaTime*speed);
            sprite.flipX = false;
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right*Time.deltaTime*speed);
            sprite.flipX = true;
        }

        var animator = GetComponent<Animator>();
        var rigid = GetComponent<Rigidbody2D>(); 
        if (jumping == JumpState.None && Input.GetKeyDown(KeyCode.Space)) {
            rigid.velocity = rigid.velocity + Vector2.up * jumpPower;
            animator.SetTrigger("Jump");
            jumping = JumpState.Up;
        }

        if (jumping == JumpState.Up && rigid.velocity.y < 0)
            jumping = JumpState.Down;
        if (jumping != JumpState.None && rigid.velocity.y == 0) {
            jumping = JumpState.None;
            animator.SetTrigger("Jump");
        }
    }
}