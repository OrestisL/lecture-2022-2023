using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviourHandler : MonoBehaviour
{
    public static List<BaseBehaviour> baseBehaviourList = new List<BaseBehaviour>();

    public static void AddToList<T>(T baseBeh) where T : BaseBehaviour
    {
        baseBehaviourList.Add(baseBeh);
    }

    public static void RemoveFromList<T>(T baseBeh) where T : BaseBehaviour
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
