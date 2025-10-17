using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Transforms and Objects")]
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public GameObject powerUpPrefab;

    [Space(10)]
    [Header("Asteroid List")]
    public List<Transform> asteroidTransforms;
    [Space(10)]
    [Header("Bomb Values")]
    public Vector2 bombOffset;
    public float bombTrailSpacing;
    public int numberOfTrailBombs;
    [Space(10)]
    [Header("Commands")]
    public float distanceAway;
    public float warpDistance;
    public float range;
    [Space(10)]
    [Header("Movement")]
    public float maxSpeed;
    public float accelerationTime;
    public float deccelerationTime;
    private Vector3 velocity;
    [Space(10)]
    [Header("Radius & Points")]
    public float radarRadius;
    public int numberOfPoints;
    public float powerUpRadius;
    public int numberOfPowerups;

    [Space(10)]
    [Header("Missiles")]
    public GameObject missilePrefab;

    [Space(10)]
    [Header("Lock on Mechanic")]
    public bool lockOn = false;
    public bool stayLockedOn = false;
    public float angularSpeed;
    public Asteroid[] asteroids;
    // Update is called once per frame
    void Update()
    {
        asteroids = FindObjectsByType<Asteroid>(FindObjectsSortMode.None);
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

       // if (Input.GetKeyDown(KeyCode.W))
      //  {
       //     WarpPlayer(enemyTransform, warpDistance);
     //   }

       // if (Input.GetKeyDown(KeyCode.S))
       // {
          //  DetectAstroids(range, asteroidTransforms); 
       // }

        PlayerMovement();
        PlayerRadar(radarRadius, numberOfPoints);

        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnPowerUps(powerUpRadius, numberOfPowerups);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MissileHoming();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if(lockOn == false)
            {
                lockOn = true; 
            }

            else
            {
                lockOn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.N) && lockOn)
        {
            if (stayLockedOn == false)
            {
                stayLockedOn = true;
            }

            else
            {
                stayLockedOn = false;
            }
        }
        if (lockOn)
        {
            RotateToAsteroid();
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

    public void DetectAstroids(float inMaxRange, List<Transform> inAstroids)
    {
        for (int i = 0; i < inAstroids.Count; i++)
        {
            Vector3 astroidPos = inAstroids[i].position;
            Vector2 playerPos = transform.position;

            float playerAstroidDistance = Vector3.Distance(astroidPos, transform.position);
            if (playerAstroidDistance <= inMaxRange && playerAstroidDistance >= 0)
            {
                Vector2 vecToBeNormalized = astroidPos - transform.position;

                Debug.DrawLine(playerPos, (vecToBeNormalized.normalized * 2.5f) + playerPos, Color.green, 50f);
            }
        }
    }

    public void PlayerMovement()
    {
        float acceleration = maxSpeed / accelerationTime;
        float decceleration = maxSpeed / deccelerationTime; 
        //If the player is pressing any key runs this code to check which direction (if any) then moves the player ship accordingly 
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                velocity += acceleration * Time.deltaTime * Vector3.up;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                velocity += acceleration * Time.deltaTime * Vector3.down;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                velocity += acceleration * Time.deltaTime * Vector3.left;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                velocity += acceleration * Time.deltaTime * Vector3.right;
            }
        }
        //For when the player isn't clicking something, checks to see if they're moving then begins to progressively slow them down 
        else
        {
            if(velocity.x > 0)
            {
                velocity -= decceleration * Time.deltaTime * Vector3.right;
                if (velocity.x < 0)
                {
                    velocity.x = 0;
                }
            }

            if(velocity.x < 0)
            {
                velocity -= decceleration * Time.deltaTime * Vector3.left;
                if (velocity.x > 0)
                {
                    velocity.x = 0;
                }
            }

            if(velocity.y > 0)
            {
                velocity -= decceleration * Time.deltaTime * Vector3.up; 
                if(velocity.y < 0)
                {
                    velocity.y = 0; 
                }
            }
            
            if(velocity.y < 0)
            {
                velocity -= decceleration * Time.deltaTime * Vector3.down;
                if (velocity.y > 0)
                {
                    velocity.y = 0;
                }
            }
        }


            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime; 
    }

    public void PlayerRadar(float radius, int numberOfPoints)
    {

        float angleInterval = 360f / numberOfPoints;
        float radians = angleInterval * Mathf.Deg2Rad;

        List<Vector3> points = new List<Vector3>();
    
        for (int i = 0; i < numberOfPoints + 1; i++)
        {
            float adjustments = radians * i;
            Vector3 point = new Vector3(Mathf.Cos(adjustments), Mathf.Sin(adjustments)) * radius;

            points.Add(point);
        }
       
        Vector3 center = transform.position;
        float isInRadius = Vector3.Distance(enemyTransform.position, transform.position);
       
        for (int i = 0; i < points.Count - 1; i++)
        {
             if (isInRadius >= radius) 
            {
                Debug.DrawLine(center + points[i], center + points[i + 1], Color.green);
            }

            else if (isInRadius < radius)
            {
                Debug.DrawLine(center + points[i], center + points[i + 1], Color.red);
            }
        }

       }

    public void SpawnPowerUps(float radius, float numberOfPowerUps)
    {
        float angleInterval = 360f / numberOfPowerUps;
        float radians = angleInterval * Mathf.Deg2Rad;
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < numberOfPowerUps; i++)
        {
            float adjustments = radians * i;
            Vector3 point = new Vector3(Mathf.Cos(adjustments), Mathf.Sin(adjustments)) * radius;

            points.Add(point);
        }

        for(int i = 0; i < points.Count; i++)
        {
            Instantiate(powerUpPrefab, points[i] + transform.position, Quaternion.identity);
        }
    }


    public void RotateToAsteroid()
    {
        float shortestDist = Vector3.Distance(asteroids[0].transform.position, transform.position); 
        Transform storeShortestDist = asteroids[0].transform;

        if (stayLockedOn == false)
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                float distFromAstroid = Vector3.Distance(asteroids[i].transform.position, transform.position);

                if (distFromAstroid < shortestDist)
                {
                    shortestDist = distFromAstroid;
                    storeShortestDist = asteroids[i].transform;
                }
            }
        }

      
        Vector2 directionToTarget = (storeShortestDist.transform.position - transform.position).normalized;

        float asteroidAngle = CalculateDegAngleFromVector(directionToTarget);
        float playerAngle = CalculateDegAngleFromVector(transform.up); 

        float deltaAngle = Mathf.DeltaAngle(playerAngle, asteroidAngle);

        float sign = Mathf.Sign(deltaAngle);

        float angleStep = angularSpeed * sign * Time.deltaTime;

        if (Mathf.Abs(angleStep) < Mathf.Abs(deltaAngle))
        {
            transform.Rotate(0, 0, angleStep);
        }

        else
        {
            transform.Rotate(0, 0, deltaAngle);
        }
    }


    private float CalculateDegAngleFromVector(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    }

    public void MissileHoming()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
    }
}


