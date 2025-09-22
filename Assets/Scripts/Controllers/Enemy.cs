using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    public float enemyRange; 
    public Vector2 enemyPos;
    public Transform player; 
    [Range(0, 1)]
    public float t; 
    private void Update()
    {
        t += Time.deltaTime;
        if (t >= 1)
        {
            Movement();
            t = 0; 
        }
    }


    public void Movement()
    {
        enemyPos = transform.position;
        float rangeToPlayer = Vector3.Distance(player.position, transform.position);
        if (rangeToPlayer > enemyRange)
        {
            int whatDir = Random.Range(0, 8);
            if (whatDir == 0 && enemyPos.y < 10 - enemySpeed)
            {
                enemyPos += Vector2.up * enemySpeed;
            }

            else if (whatDir == 1 && enemyPos.y < 10 - enemySpeed && enemyPos.x < 22 - enemySpeed)
            {
                enemyPos += Vector2.up;
                enemyPos += Vector2.right;
            }

            else if (whatDir == 2 && enemyPos.x < 22 - enemySpeed)
            {
                enemyPos += Vector2.right;
            }

            else if (whatDir == 3 && enemyPos.x < 22 - enemySpeed && enemyPos.y > -10 + enemySpeed)
            {
                enemyPos += Vector2.right;
                enemyPos += Vector2.down;
            }

            else if (whatDir == 4 && enemyPos.y > -10 + enemySpeed)
            {
                enemyPos += Vector2.down;
            }

            else if (whatDir == 5 && enemyPos.y > -10 + enemySpeed && enemyPos.x > -22 + enemySpeed)
            {
                enemyPos += Vector2.down;
                enemyPos += Vector2.left;
            }

            else if (whatDir == 6 && enemyPos.x > -22 + enemySpeed)
            {
                enemyPos += Vector2.left;
            }

            else if (whatDir == 7 && enemyPos.x > -22 + enemySpeed && enemyPos.y < 10 - enemySpeed)
            {
                enemyPos += Vector2.left;
                enemyPos += Vector2.up;
            }

            transform.position = enemyPos;

        }

        else
        {
            Vector2 playerPos = player.position; 
            Vector2 normalizedVec = playerPos - enemyPos;
            
            transform.position = Vector2.Lerp(enemyPos, normalizedVec.normalized + enemyPos, 1); 
        }
    }
}
