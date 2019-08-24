using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {
    public int preys;
    public float timer;

    public int fallingLine;
    public GameObject GameOver;
    GameObject Eagle;

    void Start() {
        preys = GameObject.FindGameObjectsWithTag("Prey").Length;
        Eagle = FindObjectOfType<Eagle>().gameObject;
    }
    void Update() {
        timer -= Time.deltaTime;
        GetComponentInChildren<Text>().text = "남은 먹이: " + preys + "\n남은 시간: " + (int) timer;
        
        if (Eagle.transform.localPosition.y < fallingLine || timer < 0)
            GameOver.SetActive(true);
    }
}
