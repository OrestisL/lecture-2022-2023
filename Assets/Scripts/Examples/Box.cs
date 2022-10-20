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

        CameraClick.OnClickedOnItem.AddListener(() => Function(4, 3));
        CameraClick.OnFunc += (x) => Square(x);
        CameraClick.OnFunc += (x) => Cube(x);
        CameraClick.OnFunc += (x) => SquareRoot(x);
    }

    public override void OnUpdate()
    {
        Debug.Log(count);
    }

    private void OnDestroy()
    {
        BehaviourDestroyed();
    }

    void Function(float x, float y)
    {
        Debug.Log(x + y);
    }

    float Square(float x)
    {
        float y = x * x;
        Debug.Log(y);
        return y;
    }
    float Cube(float x)
    {
        float y = x * x * x;
        Debug.Log(y);
        return y;
    }

    float SquareRoot(float x)
    {
        if (x < 0)
        {
            Debug.Log("Input for sqrt was < 0");
            return 0;
        }
        float y = Mathf.Sqrt(x);
        Debug.Log(y);
        return y;
    }

}
