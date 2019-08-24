using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour {
    float velocity;
    public float endPoint;
    public float waitAfterFinish;
    void Start() {
        velocity = (endPoint - transform.position.y) / GetComponent<AudioSource>().clip.length;
        Debug.Log("올라가는 속도: " + velocity);
    }

    // Update is called once per frame
    bool finished;
    void Update() {
        transform.Translate(Vector3.up*velocity*Time.deltaTime);
        if (transform.position.y <= endPoint || finished) return;
        velocity = 0;
        finished = true;
        StartCoroutine(WaitAfterFinish());
    }

    IEnumerator WaitAfterFinish(){
        yield return new WaitForSeconds(waitAfterFinish);
        SceneManager.LoadScene("Title");
    }
}
