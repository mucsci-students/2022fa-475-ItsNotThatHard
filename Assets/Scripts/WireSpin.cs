using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSpin : MonoBehaviour
{
    public float startRotate = 0f;
    void Start(){
        gameObject.transform.Rotate(0.0f, startRotate, 0.0f);
    }
    public float timer = 2f;
    private float elapsedTime = 0f;

    void OnTriggerEnter(Collider collider)
    {
        GameObject collidedWith = collider.gameObject;
		if (collidedWith.tag == "Player") {
		    gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
            elapsedTime = 0f;
            while(elapsedTime < timer) {
                elapsedTime += Time.deltaTime;
            }
        }
    }
}
