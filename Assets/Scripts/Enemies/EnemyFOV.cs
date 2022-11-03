using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyFOV : BaseBehaviour
{
    [SerializeField] private LayerMask visibleLayers;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float viewRadius = 10;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Collider> seenObjects;
    [SerializeField] private float checkInterval = 1f;
    private float _checkInterval;

    private void Start()
    {
        Initialize();
        agent = GetComponent<NavMeshAgent>();
        _checkInterval = checkInterval;
    }

    public override void OnUpdate()
    {
        _checkInterval -= Time.deltaTime;
        if (_checkInterval <= 0)
        {
            _checkInterval = checkInterval;
            seenObjects = new List<Collider>();
            CheckForObjects();
        }
    }

    private void CheckForObjects() 
    {
        Collider[] seen = Physics.OverlapSphere(transform.position, viewRadius, visibleLayers);
        if (seen.Length > 0)
        {
            for (int i = 0; i < seen.Length; i++)
            {
                //bool obstaclesExist = Physics.Linecast(transform.position + Vector3.up, seen[i].transform.position, obstacleLayers);
                //float angle = Vector3.Angle(transform.forward, seen[i].transform.position - transform.position);
                if (!Physics.Linecast(transform.position + Vector3.up, seen[i].transform.position, obstacleLayers))
                {
                    seenObjects.Add(seen[i]);
                    //player spotted, do something
                }
            }
        }
    }


    private void OnDestroy()
    {
        BehaviourDestroyed();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        if (seenObjects != null)
        {
            for (int i = 0; i < seenObjects.Count; i++)
            {
                Gizmos.DrawLine(transform.position + Vector3.up, seenObjects[i].transform.position);
            }
        }
    }
}
