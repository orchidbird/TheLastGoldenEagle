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
        if (Input.GetKey(KeyCode.A)) 
            HorizontalMove(false);
        if (Input.GetKey(KeyCode.D))
            HorizontalMove(true);
        
        var rigid = GetComponent<Rigidbody2D>();
        
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
        Debug.Log("Set Animation State To: " + state);
        animState = state;
        GetComponent<Animator>().SetTrigger(state.ToString());
    }
}