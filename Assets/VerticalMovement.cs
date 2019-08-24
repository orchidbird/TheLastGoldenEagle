using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour {
    public int upperEnd;
    public int lowerEnd;
    public int speed = 50;

    public bool goingUp;
    // Update is called once per frame
    void Update() {
        if (goingUp) {
            if (transform.position.y < upperEnd)
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            else
                goingUp = false;
        }else {
            if (transform.position.y > lowerEnd)
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            else
                goingUp = true;
        }
    }
}
