// <copyright file="BezierSpline.cs" company="Team4">
//     Team4 copyright tag.
// </copyright>

using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles bezierspline points manipulation and strage
/// </summary>
public class BezierSpline : MonoBehaviour
{
    /// <summary>
    /// This is the spline points, these points determine the spline layout
    /// </summary>
    [SerializeField]
    public Vector3[] Points;

    /// <summary>
    /// Gets the current count of curves in the spline
    /// </summary>
    public int CurveCount
    {
        get
        {
            return (Points.Length - 1) / 3;
        }
    }

    /// <summary>
    ///  Gets control point count of the current spline NOT the same thing as CurveCount
    /// </summary>
    public int ControlPointCount
    {
        get
        {
            return Points.Length;
        }
    }

    /// <summary>
    /// Get the length of the spline
    /// </summary>
    /// <returns>Returns the length of the spline</returns>
    public float GetSplineLength()
    {
        int lengthCalcDensity = 20;
        int checkPointCount = lengthCalcDensity * CurveCount;
        float length = 0;
        for (int i = 0; i < checkPointCount - 1; i++)
        {
            length += Vector3.Distance(GetPoint(i), GetPoint(i + 1));
        }

        return length;
    }

    /// <summary>
    /// Get the control point of index i
    /// </summary>
    /// <param name="i">Index of control point</param>
    /// <returns>a vector 3 control point</returns>
    public Vector3 GetControlPoint(int i)
    {
        return Points[i];
    }

    /// <summary>
    /// Sets the control point of index i
    /// </summary>
    /// <param name="i">Index of control point</param>
    /// <param name="p">V3 Point</param>
    public void SetControlPoint(int i, Vector3 p)
    {
        Points[i] = p;
    }

    /// <summary>
    /// Get point returns a position on the spline to a time between 0 and 1, 0 being the start of the spline and 1 the end of the spline.
    /// </summary>
    /// <param name="t">Time between 0 and 1</param>
    /// <returns>Point on the spline to time t</returns>
    public Vector3 GetPoint(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = Points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }

        return transform.TransformPoint(Bezier.GetPoint(Points[i], Points[i + 1], Points[i + 2], Points[i + 3], t));
    }

    /// <summary>
    /// Get the velocity of a point on the spline, this velocity is according to the direction of the point
    /// </summary>
    /// <param name="t">Time between 0 and 1</param>
    /// <returns>Velocity of point on spline</returns>
    public Vector3 GetVelocity(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = Points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }

        return transform.TransformPoint(Bezier.GetFirstDerivative(Points[i], Points[i + 1], Points[i + 2], Points[i + 3], t)) -
            transform.position;
    }

    /// <summary>
    /// Get direction of a point on the spline to the time t
    /// </summary>
    /// <param name="t">Time between 0 and 1</param>
    /// <returns>Direction of time</returns>
    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }

    /// <summary>
    /// Adds a curve to the current spline, this is for editor view use only
    /// </summary>
    public void AddCurve()
    {
        Vector3 point = Points[Points.Length - 1];
        Array.Resize(ref Points, Points.Length + 3);
        point.x += 1f;
        Points[Points.Length - 3] = point;
        point.x += 1f;
        Points[Points.Length - 2] = point;
        point.x += 1f;
        Points[Points.Length - 1] = point;
    }

    /// <summary>
    /// Resets the current spline to a standard one curve spline, this is for editor view use only
    /// </summary>
    public void Reset()
    {
        Points = new Vector3[] 
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0),
            new Vector3(3, 0, 0),
            new Vector3(4, 0, 0)
        };
    }
}
