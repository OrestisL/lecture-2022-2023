using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public static int count = 0;
    protected string s;
    public virtual void Initialize()
    {
        count++;
        BaseBehaviourHandler.AddToList(this);
        //Debug.Log(name);
    }

    public virtual void OnUpdate() { }

    public static void Something() { }

    public virtual void BehaviourDestroyed()
    {
        count--;
        BaseBehaviourHandler.RemoveFromList(this);
    }
}
