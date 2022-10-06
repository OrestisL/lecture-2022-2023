using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : BaseBehaviour
{
    [Header("Prefabs")]
    public GameObject blueSpherePrefab;
    private GameObject blueSphereInstance;

    private void OnEnable()
    {
        Debug.Log("enabled");
        if (blueSphereInstance) //same as writing if(blueSphereInstance != null)
            blueSphereInstance.SetActive(true);
    }

    private void OnDisable()
    {
        Debug.Log("disabled");
        if (blueSphereInstance)
            blueSphereInstance.SetActive(false);
    }
    public override void Initialize()
    {
        base.Initialize();
    }

    private void Start()
    {
        Initialize();
        Debug.Log(count);
        blueSphereInstance = Instantiate(blueSpherePrefab);
        s = "ajsdfgasd";
    }

    public override void OnUpdate()
    {
        Debug.Log(count);
    }

    private void OnDestroy()
    {
        BehaviourDestroyed();
    }

}
