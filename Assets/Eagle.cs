using UnityEngine;

public class Eagle : MonoBehaviour {
    public float speed;
    public float jumpPower;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + Vector2.up * jumpPower;
        }
    }
}