/// The script for the gyroscope, tilt and shake
// <copyright file=GyroScript.cs company=team 4>
// team 4 
// </copyright>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Gyro script
/// </summary>
public class GyroScript : MonoBehaviour
{
    /// <summary>
    /// Variables declaration 
    /// </summary>
    ////X direction of the tilt
    public float Xdir;
    ////Z direction of the tilt
    public float Zdir;
    ////Gravity multiplier
    public float GravityForce;
    ////Threshold after which the tilt starts to account
    public float tiltThreshold;

    /// <summary>
    /// variables for the shaking
    /// </summary>
    float accelerometerUpdateInterval = 0.016f/*1.0 / 60.0*/;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! ;)
    public float shakeDetectionThreshold = 2.0f;
    private float lowPassFilterFactor;
    private Vector3 lowPassValue = Vector3.zero;
    private Vector3 acceleration;
    private Vector3 deltaAcceleration;
    bool isShaked;

    float xCalib;
    float zCalib;
    float timer;

    bool isCalibrated;

    GameObject ball;
    Text text;

    /// <summary>
    /// Initialization function
    /// </summary>
    void Start () {
        Input.gyro.enabled = true;
        text = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        ball = GameObject.Find("Sphere").gameObject;
        xCalib = 0;
        zCalib = 0;
        tiltThreshold = 1.5f;
        Xdir = 0;
        Zdir = 0;
        GravityForce = 100f;

        timer = 0;

        isCalibrated = false;
        isShaked = false;

        ///stuff for shaking
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
    }

    /// <summary>
    /// Update is called once per frame, fixed for physics
    /// </summary>
    void FixedUpdate () {

        timer += Time.deltaTime;
        if(timer > 0.5 && !isCalibrated)
        {
            xCalib = Input.gyro.gravity.x;
            zCalib = Input.gyro.gravity.y;
            isCalibrated = true;
        }

        // gravity stuff
        float moveHorizontal = Input.gyro.gravity.x;
        float moveVertical = Input.gyro.gravity.y;
   
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Quaternion direction = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        movement = direction * movement;

        
        Xdir = (movement.x - xCalib) * GravityForce;
        Zdir = (movement.z - zCalib) * GravityForce;

        //text.text = "x: " + Xdir + "z: " + Zdir;


        if (Mathf.Abs(Xdir) < tiltThreshold)
            Xdir = 0;
        if (Mathf.Abs(Zdir) < tiltThreshold)
            Zdir = 0;

        if(isCalibrated)
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(Xdir,0,Zdir) * Time.deltaTime);

        /// stuff for shaking
        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            // Perform your "shaking actions" here, with suitable guards in the if check above, if necessary to not, to not fire again if they're already being performed.
            isShaked = true;
            text.text = "shaked!";
            //Debug.Log("Shake event detected at time " + Time.time);
        }
    }
}
