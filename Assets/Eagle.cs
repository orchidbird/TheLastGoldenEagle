using UnityEngine;
using UnityEngine.SceneManagement;

public class Eagle : MonoBehaviour {
    public float speed;
    public float jumpPower;
    float originGravity;
    SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        originGravity = GetComponent<Rigidbody2D>().gravityScale;
    }

    public bool flyable;
    public float flyFallingSpeed = 1;
    enum AnimState{Stop, Jump, Walk, Fly}
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
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(animState == AnimState.Stop || animState == AnimState.Walk)
                newVel = rigid.velocity + Vector2.up * jumpPower;
            else if(flyable){
                rigid.gravityScale = 0;
                SetAnim(AnimState.Fly);
                newVel = rigid.velocity;
                newVel.y = -flyFallingSpeed;
            }
        }else if (animState == AnimState.Fly && (Input.GetKeyUp(KeyCode.Space) || rigid.velocity.y == 0)){
            rigid.gravityScale = originGravity;
            SetAnim(AnimState.Jump);
        }else if (animState != AnimState.Fly) {
            if(rigid.velocity.y != 0)
                SetAnim(AnimState.Jump);
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
                SetAnim(AnimState.Walk);
            else
                SetAnim(AnimState.Stop);
        }

        rigid.velocity = newVel;
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

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Prey") {
            Destroy(col.gameObject);
            FindObjectOfType<StageManager>().preys--;
        }else if (col.gameObject.GetComponent<VerticalMovement>() != null)
            transform.parent = col.gameObject.transform;
        else if (col.gameObject.name == "Nest" && FindObjectOfType<StageManager>().preys == 0) {
            SceneManager.LoadScene("Stage2");
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.GetComponent<VerticalMovement>() != null)
            transform.parent = null;
    }
}