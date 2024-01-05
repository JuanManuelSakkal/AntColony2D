using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePheromone : MonoBehaviour
{
    public GameObject homePheromone;
    public float pheromoneRate = 1f;

    void Start() {
        StartCoroutine(CreatePheromoneWithDelay(pheromoneRate));
    }

    void SpawnPheromone(){
        Instantiate(homePheromone, transform.position, transform.rotation);
    }

    IEnumerator CreatePheromoneWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            SpawnPheromone();
        }
    }
}
