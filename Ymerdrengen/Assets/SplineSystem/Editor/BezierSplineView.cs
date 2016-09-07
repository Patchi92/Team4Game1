// <copyright file="BezierSplineView.cs" company="Team4">
//     Team4 copyright tag.
// </copyright>

using System.Collections;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This class handles the editor view of the bezierspline class
/// </summary>
[CustomEditor(typeof(BezierSpline))]
public class BezierSplineView : Editor
{
    /// <summary>
    /// The scale of the direction lines
    /// </summary>
    private const float DirectionScale = 0.5f;

    /// <summary>
    /// Handle variables
    /// </summary>
    private const float HandleSize = 0.08f;

    /// <summary>
    /// The size of the point buttons
    /// </summary>
    private const float PickSize = 0.12f;

    /// <summary>
    /// Selcted poitn index
    /// </summary>
    private int selectedIndex = -1;

    /// <summary>
    /// The spline currenty shown
    /// </summary>
    private BezierSpline spline;

    /// <summary>
    /// The handle transform of the spline
    /// </summary>
    private Transform handleTransform;

    /// <summary>
    /// The handle rotation of the spline
    /// </summary>
    private Quaternion handleRotation;

    /// <summary>
    /// How many steps per curve is shown
    /// </summary>
    private int stepsPerCurve = 10;
   
    /// <summary>
    /// This overrides the onInspectorGUI which modifies the inspector of the class bezierSpline
    /// </summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        spline = target as BezierSpline;
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(spline, "Add Curve");
            spline.AddCurve();
            EditorUtility.SetDirty(spline);
        }

        if (GUILayout.Button("Reset Curve"))
        {
            spline.Reset();
            EditorUtility.SetDirty(spline);
        }
    }

    /// <summary>
    /// This is a built in UNITY class that handles on scene GUI visuals
    /// </summary>
    private void OnSceneGUI()
    {
        spline = target as BezierSpline;
        handleTransform = spline.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        for (int i = 1; i < spline.ControlPointCount; i += 3)
        {
            Vector3 p1 = ShowPoint(i);
            Vector3 p2 = ShowPoint(i + 1);
            Vector3 p3 = ShowPoint(i + 2);
        
            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);
        
            Handles.DrawBezier(p0, p3, p1, p2, Color.green, null, 2f);
            p0 = p3;
        }

        ShowDirections();
    }

    /// <summary>
    /// Show point shows a button point in the editor
    /// </summary>
    /// <param name="index">Index of the point</param>
    /// <returns>Returns the vec3 of the point</returns>
    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(spline.GetControlPoint(index));
        float size = HandleUtility.GetHandleSize(point);
        Handles.color = Color.white;
        if (Handles.Button(point, handleRotation, size * HandleSize, size * PickSize, Handles.DotCap))
        {
            selectedIndex = index;
            Repaint();
        }

        if (selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Move Point");
                EditorUtility.SetDirty(spline);
                spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
            }
        }

        return point;
    }

    /// <summary>
    /// Show direction of a point on the spline
    /// </summary>
    private void ShowDirections()
    {
        Handles.color = Color.red;
        Vector3 point = spline.GetPoint(0f);
        Handles.DrawLine(point, point + (spline.GetDirection(0f) * DirectionScale));
        int steps = stepsPerCurve * spline.CurveCount;
        for (int i = 1; i <= steps; i++)
        {
            point = spline.GetPoint(i / (float)steps);
            Handles.DrawLine(point, point + (spline.GetDirection(i / (float)stepsPerCurve) * DirectionScale));
        }
    }
}
