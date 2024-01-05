using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePheromone : MonoBehaviour
{
    public GameObject homePheromone;
    public float pheromoneRate = 1f;
    public float initialIntensity = 0.9f;

    public float currentIntensity;

    void Start() {
        currentIntensity = initialIntensity;
        StartCoroutine(CreatePheromoneWithDelay(pheromoneRate));
    }

    void SpawnPheromone(){
        GameObject newPheromone = Instantiate(homePheromone, transform.position, transform.rotation);
        newPheromone.GetComponent<Intensity>().intensity = currentIntensity;
        currentIntensity *= 0.8f;
    }

    IEnumerator CreatePheromoneWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            SpawnPheromone();
        }
    }
}
