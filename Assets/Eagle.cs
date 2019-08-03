using UnityEngine;

public class Eagle : MonoBehaviour {
    public float speed = 3;
    SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void Update() {
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left*Time.deltaTime*speed);
            sprite.flipX = false;
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right*Time.deltaTime*speed);
            sprite.flipX = true;
        }
    }
}