using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {
    public int jump;
    Rigidbody2D rigid;

    void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    void Update() {
        if (rigid.velocity.y == 0)
            rigid.velocity = rigid.velocity + Vector2.up * jump;
    }
}
