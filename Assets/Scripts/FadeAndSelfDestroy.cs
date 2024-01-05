using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndSelfDestroy : MonoBehaviour
{
    public float selfDestructDelay = 20f;
    void Start() {
            Destroy(gameObject, selfDestructDelay);
    }

}
