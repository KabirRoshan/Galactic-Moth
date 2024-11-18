// This script will spawn the asteroids outside the screen
// it will also contain a list to add different prefabs to also spawn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inherits the side spawn class SpawnAsteroids
public class SpawnAsteroidsTop : SpawnAsteroids
{
    // OVERRIDE
    public override void SpawnEnemy()
    {
        // This adds asteroid to the scene
        // It will choose in the last a random prefab to spawn in 
        GameObject ard = Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)]) as GameObject;
        // This will position asteroid outside screen on random x axis to create the challange
        // Changed -2 to positive 2 because screenBounds.y will be negative already. 
        // This will allow it to work for an orthographic camera
        ard.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y * 2);
    } // spawnEnemy
} // Class SpawnAsteriodsTop
