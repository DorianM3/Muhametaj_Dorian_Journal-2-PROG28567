using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float angularSpeed;
    public Transform target; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        float targetAngle = CalculateDegAngleFromVector(directionToTarget);
        float upAngle = CalculateDegAngleFromVector(transform.up); 

        float deltaAngle = Mathf.DeltaAngle(upAngle, targetAngle); 

        float sign = Mathf.Sign(deltaAngle);

        float angleStep = angularSpeed * Time.deltaTime * sign; 
        
        Debug.DrawLine(transform.position, transform.up + transform.position, Color.red);
        
       
        
        float dot = Vector3.Dot(transform.up, directionToTarget);
        
        if(Mathf.Abs(angleStep) < Mathf.Abs(deltaAngle))
        {
            transform.Rotate(0, 0, angleStep); 
        }

        else
        {
            transform.Rotate(0, 0, deltaAngle); 
        }


        if (dot < 0)
        {
            Debug.Log("Behind");
        }

        else if (dot > 0)
        {
            Debug.Log("In Front");
        }


    }

    private float CalculateDegAngleFromVector(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg; 
    }
}
