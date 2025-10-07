using UnityEngine;

public class TwoAngles : MonoBehaviour
{
    public float redAngle;
    public float blueAngle; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AngleVector(redAngle, blueAngle);
    }

    public void AngleVector(float angle1, float angle2)
    {
        float adjustments1 = angle1 * Mathf.Deg2Rad;
        float adjustments2 = angle2 * Mathf.Deg2Rad;

        Vector3 point1 = new Vector3(Mathf.Cos(adjustments1), Mathf.Sin(adjustments1));
        Vector3 point2 = new Vector3(Mathf.Cos(adjustments2), Mathf.Sin(adjustments2));

        Debug.DrawLine(Vector2.zero, point1, Color.red);
        Debug.DrawLine(Vector2.zero, point2, Color.blue);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float dotProduct = (point1.x * point2.x) + (point1.y * point2.y);
            Debug.Log(dotProduct); 
        }
    }
}
