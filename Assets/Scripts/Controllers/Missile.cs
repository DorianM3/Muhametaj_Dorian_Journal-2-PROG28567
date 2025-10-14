using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Asteroid[] asteroids;
    public float speedOfMissile;
    private float radiusOfDetection = 3f;
    public bool homeIn = false;
    public bool dontLook = false; 
    private Transform storeAsteroid;
    [Range(0,10)]
    public float t;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        asteroids = FindObjectsByType<Asteroid>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        MissileLaunch(speedOfMissile, radiusOfDetection);
    }

    public void MissileLaunch(float speed, float radius)
    {
        Vector2 missilePos = transform.position;
        missilePos.y += Time.deltaTime * speed;
        transform.position = missilePos;

        float currentClosest = radius;

        for (int i = 0; i < asteroids.Length && dontLook == false; ++i)
        {
            float dist = Vector3.Distance(asteroids[i].transform.position, transform.position);
            if (dist <= currentClosest && transform.position.y < asteroids[i].transform.position.y)
            {
                currentClosest = dist;
                homeIn = true;
                storeAsteroid = asteroids[i].transform;
            }
        }

        if (homeIn)
        {
            dontLook = true;
            t += Time.deltaTime; 
            transform.position = Vector3.Lerp(transform.position, storeAsteroid.position, t / 2);

            if (Vector3.Distance(transform.position, storeAsteroid.position) < 0.5f)
            {
                Destroy(gameObject);
                Destroy(storeAsteroid.gameObject); 
            }

            if (missilePos.y > 10)
            {
                Destroy(gameObject);
            }
        }


        if (missilePos.y > 10)
        {
            Destroy(gameObject);
        }
    }
}
