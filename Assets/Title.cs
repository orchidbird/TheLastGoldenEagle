using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour {
    bool started;
    void Update() {
        if (!started && (Input.anyKey || Input.GetMouseButtonDown(0))) {
            started = true;
            Destroy(FindObjectOfType<Text>().gameObject);
            StartCoroutine(SetBlackWalls());
        }
    }

    public CustomSpriteAnim anim;
    public float wallSpeed = 30;
    IEnumerator SetBlackWalls() {
        var left = GameObject.Find("LeftBlack");
        var right = GameObject.Find("RightBlack");
        var movedDist = 240f;
        while (movedDist > 10) {
            movedDist -= Time.deltaTime * wallSpeed;
            left.GetComponent<RectTransform>().anchoredPosition = Vector3.left * movedDist;
            right.GetComponent<RectTransform>().anchoredPosition = Vector2.right * movedDist;
            yield return null;
        }

        Destroy(GameObject.Find("Logo"));
        anim.enabled = true;
    }
}