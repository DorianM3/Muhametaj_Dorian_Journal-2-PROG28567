using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float orbitSpeed;
    public float radiusOfPlanet;
    private float count = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(radiusOfPlanet, orbitSpeed, planetTransform); 
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        float adjustedSpeed = speed * Time.deltaTime;
        count -= adjustedSpeed; 
        float adjustments = count  * Mathf.Deg2Rad;
        
        
        Vector3 planetStore = planetTransform.position;
        Vector3 point = new Vector3(Mathf.Cos(adjustments), Mathf.Sin(adjustments)) * radius;

        point.x += planetStore.x;
        point.y += planetStore.y;
        transform.position = point;

    }
}
