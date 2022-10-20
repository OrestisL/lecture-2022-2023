using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CameraClick : MonoBehaviour
{
    Camera mainCam;
    public LayerMask hitLayers;
    public static UnityEvent OnClickedOnItem = new UnityEvent();
    public static Action OnActionComplete;
    public static Func<float, float> OnFunc;
    public AudioSource audioSource;
    public AudioClip clickClip;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        Click();
    }

    void Click() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left click");
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.Log(hitInfo.transform.name);
                OnClickedOnItem?.Invoke();

                audioSource.PlayOneShot(clickClip);

                Ray secondRay = new Ray(hitInfo.transform.position, Vector3.down);
                if (Physics.Raycast(secondRay, out RaycastHit hitInfo2))
                {
                    Debug.Log($"second ray start {hitInfo.transform.name}, hit {hitInfo2.transform.name}");
                }

                Collider[] hitColls = Physics.OverlapSphere(hitInfo.transform.position, 5, hitLayers);

                OnFunc?.Invoke(-5);

                if (hitColls.Length > 0)
                {
                    for (int i = 0; i < hitColls.Length; i++)
                    {
                        if (hitColls[i].name.Equals(hitInfo.transform.name))
                            continue;

                        Debug.Log($"overlap sphere hit :{hitColls[i].name}");
                    }
                }
            }
        }
    }
}

