using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSpriteAnim : MonoBehaviour {
    Sprite[] sprites;
    public int frameToFlip;

    int spriteNumber;
    int currentCount;

    void Awake() {
        sprites = Resources.LoadAll<Sprite>(SceneManager.GetActiveScene().name);
    }
    void Update() {
        currentCount++;
        if (currentCount < frameToFlip) return;
        currentCount = 0;
        spriteNumber++;
        if (spriteNumber < sprites.Length)
            GetComponent<SpriteRenderer>().sprite = sprites[spriteNumber];
        else
            SceneManager.LoadScene("Stage1");
    }
}
