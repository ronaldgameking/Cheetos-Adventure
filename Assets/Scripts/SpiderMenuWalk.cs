using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMenuWalk : MonoBehaviour
{
    public List<Waypoint> Points;
    public float Speed = 5f;
    public float StepTime = .1f;
    public float StoppingDistance = 1f;

    private int currentPoint = 0;
    private float m_timer;
    private float distance;


    private void Update()
    {
        if (m_timer > StepTime)
        {
            Debug.Log(Points[currentPoint].point.position + " " + Points[currentPoint].JumpsTo + "[" + currentPoint + "/" + Points.Count + "]");
            transform.position = Vector2.MoveTowards(transform.position, Points[currentPoint].point.position, Speed);
            transform.transform.right = Points[currentPoint].point.position - transform.position;
            distance = Vector2.Distance(transform.position, Points[currentPoint].point.position);
            m_timer = 0;
            if (distance <= StoppingDistance)
            {
                Debug.Log("MOV");
                currentPoint++;
                if (currentPoint >= Points.Count)
                    currentPoint = 0;

                if (Points[currentPoint].JumpsTo)
                {
                    Debug.Log("JMP");
                    transform.position = Points[currentPoint].point.position;
                    currentPoint++;
                    if (currentPoint >= Points.Count)
                        currentPoint = 0;
                }
            }
        }
        m_timer += Time.deltaTime;
    }
}
[System.Serializable]
public class Waypoint
{
    public Transform point;
    public bool JumpsTo;
}