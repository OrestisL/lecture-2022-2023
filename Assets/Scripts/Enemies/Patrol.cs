using UnityEngine;
using UnityEngine.AI;

public class Patrol : BaseBehaviour
{
    public Transform patrolPath;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    void Start()
    {
        Initialize();

        agent = GetComponent<NavMeshAgent>();

        if (patrolPath)
        {
            points = new Transform[patrolPath.childCount];

            for (int i = 0; i < patrolPath.childCount; i++)
            {
                points[i] = patrolPath.GetChild(i);
            }
        }
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    public override void OnUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
            GotoNextPoint();
    }


    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Transform current = points[i];
            if (current)
                Gizmos.DrawSphere(current.position + Vector3.up, 1f);
        }

        for (int i = 1; i < points.Length ; i++)
        {
            Gizmos.DrawLine(points[i-1].position + Vector3.up, points[i].position + Vector3.up);
        }

        //draw line between last and first
        Gizmos.DrawLine(points[0].position + Vector3.up, points[points.Length - 1].position + Vector3.up);
    }
}