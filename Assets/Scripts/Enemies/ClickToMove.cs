using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float radiusAroundPoint = 1f;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                agent.SetDestination(hit.point + new Vector3(Mathf.Cos(angle), 0 , Mathf.Sin(angle)) * radiusAroundPoint);

                if (agent.remainingDistance <= radiusAroundPoint)
                {
                    transform.LookAt(hit.transform, Vector3.up);
                }
            }
        }

    }
}
