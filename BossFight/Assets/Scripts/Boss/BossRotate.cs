using UnityEngine;

public class BossRotate : MonoBehaviour {

    public GameObject playerRef;

    void Start() {
        playerRef = GameObject.Find("Player").transform.GetChild(0).gameObject;
    }

    void FixedUpdate() {
        transform.LookAt(playerRef.transform);
        Debug.Log("Looked");
    }
}
