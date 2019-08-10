using UnityEngine;

public class Squrrel : MonoBehaviour {
    SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sprite.flipX) {
            if (transform.position.x > 1)
                transform.Translate(Vector3.left * Time.deltaTime*3);
            else
                sprite.flipX = true;
        }
        else
        {
            if (transform.position.x < 7)
                transform.Translate(Vector3.right * Time.deltaTime*3);
            else
                sprite.flipX = false;
        }
    }
}