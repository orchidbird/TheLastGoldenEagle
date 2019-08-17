using System;
using UnityEngine;

public class Eagle : MonoBehaviour {
    public float speed;
    public float jumpPower;
    SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    enum AnimState{Stop, Jump, Walk}
    AnimState animState;
    void Update() {
        var rigid = GetComponent<Rigidbody2D>();
        var newVel = rigid.velocity;
        if (Input.GetKey(KeyCode.A)) {
            newVel.x = -speed;
            sprite.flipX = false;
        }else if (Input.GetKey(KeyCode.D)) {
            newVel.x = speed;
            sprite.flipX = true;
        }else
            newVel.x = 0;

        rigid.velocity = newVel;

        if (animState != AnimState.Jump && Input.GetKeyDown(KeyCode.Space))
            rigid.velocity = rigid.velocity + Vector2.up * jumpPower;
        
        if (rigid.velocity.y != 0)
            SetAnim(AnimState.Jump);
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
            SetAnim(AnimState.Walk);
        else
            SetAnim(AnimState.Stop);
    }

    void HorizontalMove(bool right){
        transform.Translate((right ? Vector3.right : Vector3.left) *Time.deltaTime*speed);
        sprite.flipX = right;
    }

    void SetAnim(AnimState state) {
        if(animState == state) return;
        animState = state;
        GetComponent<Animator>().SetTrigger(state.ToString());
    }
}