// This script will spawn the asteroids outside the screen
// it will also contain a list to add different prefabs to also spawn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour {
    // Made into a list to add more prefabs to spawn in with different abilities
    public GameObject[] asteroidPrefab;
    public float respawnTime = 1.0f;
    public Vector2 screenBounds;

    // Use this for initialization
    void Start () {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(AsteroidWave()); 
    } // Start

    // This method will change for top spawn
    public virtual void SpawnEnemy()
    {
        // This adds asteroid to the scene
        // It will choose in the last a random prefab to spawn in 
        GameObject ard = Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)]) as GameObject;
        // This will position asteroid outside screen on random y axis to create the challange
        // Changed -2 to positive 2 because screenBounds.x will be negative already. 
        // This will allow it to work for an orthographic camera
        ard.transform.position = new Vector2(screenBounds.x * 2, Random.Range(-screenBounds.y, screenBounds.y));
    } // spawnEnemy
	
    // This is a coroutine that will spawn asteroids at time respawnTime.
    IEnumerator AsteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        } // while
    } // Coroutine asteroidWave
}
