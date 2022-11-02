using UnityEngine;
using UnityEngine.AI;

public class EnemyFOV : BaseBehaviour
{
    [SerializeField] private LayerMask visibleLayers;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float viewRadius = 10;
    [SerializeField] private NavMeshAgent agent;

    private void Start()
    {
        Initialize();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnUpdate()
    {
        Collider[] seenObjects = Physics.OverlapSphere(transform.position, viewRadius, visibleLayers);
        if (seenObjects.Length > 0)
        {
            if (!Physics.Linecast(transform.position, seenObjects[0].transform.position, obstacleLayers))
            {
                //no obstacles in between
                agent.SetDestination(seenObjects[0].transform.position);
            }
        }
    }

    private void OnDestroy()
    {
        BehaviourDestroyed();
    }
}
