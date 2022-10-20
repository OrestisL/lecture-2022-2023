using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonExample : GenericSingleton<SingletonExample>
{
    [SerializeField] private static float v = 50.123f;
    public static float V 
    {
        get 
        {
            if (Instance)
            { }
            return v;
        }

        set     
        {
            v += value;
        }
    }
    public override void Awake()
    {
        base.Awake();
    }
}
