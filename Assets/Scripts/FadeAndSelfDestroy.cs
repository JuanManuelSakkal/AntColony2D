using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndSelfDestroy : MonoBehaviour
{
    public float selfDestructDelay = 20f;
    void Start() {
        StartCoroutine(SelfDestruct(selfDestructDelay));
    }

    IEnumerator SelfDestruct(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
    }
}
