using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviourHandler : MonoBehaviour
{
    public static List<BaseBehaviour> baseBehaviourList = new List<BaseBehaviour>();

    public static void AddToList(BaseBehaviour baseBeh)
    {
        baseBehaviourList.Add(baseBeh);
    }

    public static void RemoveFromList(BaseBehaviour baseBeh)
    {
        if (baseBehaviourList.Contains(baseBeh))
            baseBehaviourList.Remove(baseBeh);
    }

    private void Update()
    {
        for (int i = 0; i < baseBehaviourList.Count; i++)
        {
            baseBehaviourList[i].OnUpdate();
        }
    }
}
