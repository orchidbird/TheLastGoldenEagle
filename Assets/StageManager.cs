using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {
    public int preys;
    public float timer;

    public Text uiText;

    void Update() {
        timer -= Time.deltaTime;
        uiText.text = "남은 먹이: " + preys + "\n남은 시간: " + (int) timer;
    }
}
