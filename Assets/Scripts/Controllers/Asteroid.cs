using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Apple;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    [Range(0, 1)]
    public float t; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        int randomDir = Random.Range(0, 4);
        Vector2 asteroidPos = transform.position;
        Vector2 destination = asteroidPos; 

        if (randomDir == 0)
        {
            destination += Vector2.up * (maxFloatDistance - arrivalDistance) * Time.deltaTime;
        }

        else if (randomDir == 1)
        {
            destination += Vector2.down * (maxFloatDistance - arrivalDistance) * Time.deltaTime;
        }

        else if (randomDir == 2)
        {
            destination += Vector2.left * (maxFloatDistance - arrivalDistance) * Time.deltaTime;
        }

        else
        {
            destination += Vector2.right * (maxFloatDistance - arrivalDistance) * Time.deltaTime;
        }

        for(float i = t; i <= 1; i += Time.deltaTime * moveSpeed)
        transform.position = Vector2.Lerp(asteroidPos, destination, i); 
    }
}
