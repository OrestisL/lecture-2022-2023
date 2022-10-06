using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    class ExampleClass
    {
        public class Coords
        {
            public float x;
            public float y;
            public Coords() { }
            public Coords(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
        }
        private float x = 0, y = 0;
        private Coords coords;
        public ExampleClass()
        {
            Debug.Log("x is : " + x + " y is : " + y);
        }
        public ExampleClass(float x)
        {
            this.x = x;
            y = x * x;

            Debug.Log("x is : " + x + " y is : " + y);
        }
        public ExampleClass(float x, float y)
        {
            this.x = x;
            this.y = y;
            Debug.Log("x is : " + x + " y is : " + y);
        }

        public Coords GetCoords()
        {
            coords = new Coords(x, y);
            return coords;
        }
    }

    public int x;
    [SerializeField] private int y;
    public bool b;
    public string s;
    public char c;
    public float z;
    /// <summary>
    /// Adds toAdd to x.
    /// </summary>
    /// <param name="toAdd">The amount to be added.</param>
    /// <returns>The sum of x + toAdd.</returns>
    public int AddToX(int toAdd)
    {
        return x + toAdd;
    }


    public void TestFunc(int toAdd)
    {
        x = x + toAdd;
    }

    private void Start()
    {
        x = 3;
        y = AddToX(x);
        b = x < (y + 1);

        //int v = AddToX(5);
        //int xx = AddToX(c);
        //TestFunc(6);
        Debug.Log(x);

        //for (float i = 0; i <= 1.0f; i += 0.1f) //i++ => i = i + 1;, i-- => i = i - 1; i +-*/= x => i = i +-*/ x
        //{
        //    ExampleClass pointOnLine = new ExampleClass(i, i);
        //    GameObject tempOnLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    tempOnLine.transform.position = new Vector3(pointOnLine.GetCoords().x, pointOnLine.GetCoords().y, 0);
        //    tempOnLine.transform.localScale = Vector3.one * 0.05f;

        //    ExampleClass pointOnCurve = new ExampleClass(i);
        //    GameObject tempOnCurve = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    tempOnCurve.transform.position = new Vector3(pointOnCurve.GetCoords().x, pointOnCurve.GetCoords().y, 0);
        //    tempOnCurve.transform.localScale = Vector3.one * 0.05f;
        // }

        //Debug.Log(EvaluateAtPoint(x, Function));
        //Debug.Log(NumericalIntegration(1, 5, 1000, Function));
    }


    //private double EvaluateAtPoint(double x, Func<double, double> function)
    //{
    //    return function(x);
    //}
    //private float Function(float x)
    //{
    //    return Mathf.Sin(x) * Mathf.Cos(x);
    //}

    ///// <summary>
    ///// Integrates function over [start, end] using Simpson's 3/8 rule.
    ///// </summary>
    ///// <param name="start">Start of integration</param>
    ///// <param name="end">End of integration</param>
    ///// <param name="function">The function to be integrated</param>
    ///// <returns></returns>
    //private double NumericalIntegration(float start, float end, int steps, Func<float, float> function)
    //{
    //    float sum = 0;
    //    int simpsonCoeff;

    //    for (int i = 0; i <= steps; i++)
    //    {
    //        if (i == 0 || i == steps)
    //            simpsonCoeff = 1;
    //        else
    //        {
    //            if (i % 2 == 0)
    //                simpsonCoeff = 4;
    //            else
    //                simpsonCoeff = 2;
    //        }
    //        sum += simpsonCoeff * (function(start + i * (end - start) / steps));
    //    }
    //    sum *= (end - start) / (3 * steps);

    //    return sum;

    //}
}
