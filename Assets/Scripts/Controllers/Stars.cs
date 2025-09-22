using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public List<Vector2> starPositions;
    public float drawingTime;
    public LineRenderer lineDraw; 

    // Update is called once per frame
    void Update()
    {
        DrawConstellation(); 
    }

    public void DrawConstellation()
    {

        foreach (Transform starTransforms in starTransforms)
        {
            starPositions.Add(starTransforms.position);
        }
        lineDraw.positionCount = starPositions.Count();
        for (int i = 0; i < starPositions.Count(); i++)
        {
            lineDraw.SetPosition(i, starPositions[i]);
        }
        int store = starPositions.Count(); 
        for (int i = 0; i < store - 1; i++)
        {
            starPositions.Remove(starPositions[i - i]);
        }
    }
}
