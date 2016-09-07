using UnityEngine;
using System.Collections;

public class TEST_SCRIPT : MonoBehaviour {

    public BezierSpline spline;

    float t;
    float walkTime = 8f;
	
	// Update is called once per frame
	void Update () {

        t += Time.deltaTime / walkTime;

        Debug.Log(spline.GetSplineLength());
        Debug.Log(t);

        transform.position = spline.GetPoint(t);

        if (t >= 1)
            t = 0;
	}
}
