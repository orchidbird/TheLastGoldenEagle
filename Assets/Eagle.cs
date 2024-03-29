﻿using UnityEngine;
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

    int hp = 3;
    int enemyHp = 5;
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Prey") {
            Destroy(col.gameObject);
            FindObjectOfType<StageManager>().preys--;
        }else if(col.gameObject.tag == "Trap")
            FindObjectOfType<StageManager>().GameOver.SetActive(true);
        else if (col.gameObject.name == "Wolf") {
            if (transform.position.y > col.gameObject.transform.position.y + 500) {
                enemyHp--;
                Debug.Log("적 체력: " + enemyHp);
                if(enemyHp == 0)
                    SceneManager.LoadScene("Ending");
            }else {
                hp--;
                Debug.Log("내 체력: " + hp);
                if(hp == 0)
                    FindObjectOfType<StageManager>().GameOver.SetActive(true);
            }
        }else if (col.gameObject.GetComponent<VerticalMovement>() != null)
            transform.parent = col.gameObject.transform;
        else if (col.gameObject.name == "Nest" && FindObjectOfType<StageManager>().preys == 0)
            LoadNextStage();
    }

    void LoadNextStage() {
        var sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Stage1")
            SceneManager.LoadScene("Stage2");
        else if(sceneName == "Stage2")
            SceneManager.LoadScene("Stage3");
        else if(sceneName == "Stage3")
            SceneManager.LoadScene("Stage4");
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.GetComponent<VerticalMovement>() != null)
            transform.parent = null;
    }
}