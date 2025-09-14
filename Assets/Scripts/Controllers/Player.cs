using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    public Vector2 bombOffset;
    public float bombTrailSpacing;
    public int numberOfTrailBombs; 

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBombAtOffset(bombOffset);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs); 
        }
    }

    public void SpawnBombAtOffset(Vector3 inOffset)
    {
        Vector3 spawnPosition = transform.position + inOffset;
        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        //We add one to the start and the end so the code never multiplies a location by 0
        for(int i = 1; i < inNumberOfBombs + 1; i++)
        {
            //Make a vector2 of the players transform so we can adjust the y each iteration of the loop and lower the bomb placement more and more 
            Vector2 playerLocation = transform.position;
            playerLocation.y -= (inBombSpacing * i);
            Instantiate(bombPrefab, playerLocation, Quaternion.identity); 
        }
    }
}
