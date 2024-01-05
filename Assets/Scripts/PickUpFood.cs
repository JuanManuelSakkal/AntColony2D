using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpFood : MonoBehaviour
{
    GameObject storedFood;
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Food" && storedFood == null) {
            collisionInfo.gameObject.transform.parent = gameObject.transform;
            collisionInfo.gameObject.transform.localPosition = new Vector3(0, 1.2f, 0);
            collisionInfo.gameObject.layer = 0;
            storedFood = collisionInfo.gameObject;
            gameObject.GetComponent<MoveAnt>().target = null;
            gameObject.GetComponent<FieldOfView>().targetMask = gameObject.GetComponent<FieldOfView>().homePheromoneMask;
            gameObject.GetComponent<FieldOfView>().preference = Preference.Intensity;

        }

        if (collisionInfo.gameObject.tag == "Home" && storedFood != null) {
            Destroy(storedFood);
            gameObject.GetComponent<MoveAnt>().target = null;
            gameObject.GetComponent<FieldOfView>().targetMask = gameObject.GetComponent<FieldOfView>().foodMask;
            gameObject.GetComponent<FieldOfView>().preference = Preference.Proximity;

        }

    }
    void Update()
    {
        
    }
}
