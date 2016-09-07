// <copyright file="Bezier.cs" company="Team4">
//     Team4 copyright tag.
// </copyright>

using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles the calculation of bezier curves
/// </summary>
public static class Bezier
{
    /// <summary>
    /// Get a point between four control points in a bezier spline
    /// </summary>
    /// <param name="p0">Point one</param>
    /// <param name="p1">Point two</param>
    /// <param name="p2">Point three</param>
    /// <param name="p3">Point four</param>
    /// <param name="t">Time between 0 and 1</param>
    /// <returns>A point</returns>
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float deltaT = 1f - t;
        return deltaT * deltaT * deltaT * p0 + 3f * deltaT * deltaT * t * p1 +
            3f * deltaT * t * t * p2 + t * t * t * p3;
    }

    /// <summary>
    /// Get the first derivative of the bezier points
    /// </summary>
    /// <param name="p0">Point one</param>
    /// <param name="p1">Point two</param>
    /// <param name="p2">Point three</param>
    /// <param name="p3">Point four</param>
    /// <param name="t">Time between 0 and 1</param>
    /// <returns>A derivative</returns>
    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float deltaT = 1f - t;
        return 3f * deltaT * deltaT * (p1 - p0) + 6f * deltaT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }
}
