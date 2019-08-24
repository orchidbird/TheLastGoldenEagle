using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public float stoppingTime;
    void Update() {
        stoppingTime -= Time.deltaTime;
        if (stoppingTime < 0 && Input.anyKeyDown)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
