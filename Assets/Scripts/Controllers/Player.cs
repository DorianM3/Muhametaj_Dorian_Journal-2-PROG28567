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
    public float distanceAway;
    public float warpDistance;
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnBombOnRandomCorner(distanceAway);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            WarpPlayer(enemyTransform, warpDistance);
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

    public void SpawnBombOnRandomCorner(float inDistance)
    {
        int whichCorner = Random.Range(0, 4);
        Vector2 playerPos = transform.position; 
        if(whichCorner == 0)
        {
            playerPos += Vector2.up;
            playerPos += Vector2.left;
            playerPos.x -= inDistance;
            playerPos.y += inDistance; 
        }

        else if(whichCorner == 1)
        {
            playerPos += Vector2.up;
            playerPos += Vector2.right;
            playerPos.x += inDistance;
            playerPos.y += inDistance; 
        }

        else if(whichCorner == 2)
        {
            playerPos += Vector2.down;
            playerPos += Vector2.right;
            playerPos.x += inDistance;
            playerPos.y -= inDistance; 
        }

        else
        {
            playerPos += Vector2.down;
            playerPos += Vector2.left;
            playerPos.x -= inDistance;
            playerPos.y -= inDistance;
        }
       
        Instantiate(bombPrefab, playerPos, Quaternion.identity); 
    }

    public void WarpPlayer(Transform target, float ratio)
    {
        if(ratio <= 1 && ratio >= 0)
        {
            transform.position = Vector2.Lerp(transform.position, enemyTransform.position, ratio); 
        }
    }
}
