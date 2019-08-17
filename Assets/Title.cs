using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    void Update() {
        if (Input.anyKey)
            SceneManager.LoadScene("Opening");
    }
}