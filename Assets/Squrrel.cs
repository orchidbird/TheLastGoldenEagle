using UnityEngine;

public class Squrrel : MonoBehaviour {
    SpriteRenderer sprite;
    public int leftEnd;
    public int rightEnd;
    public int speed = 50;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sprite.flipX) {
            if (transform.position.x > leftEnd)
                transform.Translate(Vector3.left * Time.deltaTime*speed);
            else
                sprite.flipX = true;
        }
        else
        {
            if (transform.position.x < rightEnd)
                transform.Translate(Vector3.right * Time.deltaTime*speed);
            else
                sprite.flipX = false;
        }
    }
}