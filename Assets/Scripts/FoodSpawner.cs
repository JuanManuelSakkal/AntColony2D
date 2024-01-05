using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private int count = 0;
    public int spawnAmount = 2;
    public float spawnTimer = 0f;
    public GameObject foodObject;
    public Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 8f;
            Vector3 objectPos = mainCamera.ScreenToWorldPoint(mousePos);
            for (int i = 0; i < spawnAmount; i++)
            {
                objectPos.x += (i % 2 == 0 ? 1 : -1) * 0.2f;
                objectPos.y += (i % 2 == 0 ? 1 : -1) * 0.2f;
                Instantiate(foodObject, objectPos, Quaternion.identity);
            }
        }
    }
}