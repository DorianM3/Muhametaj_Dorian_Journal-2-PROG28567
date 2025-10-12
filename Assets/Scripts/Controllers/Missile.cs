using System.Reflection;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speedOfMissile;
    public float radiusOfDetection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

        if (missilePos.y > 10)
        {
            Destroy(gameObject);
        }
    }
}
