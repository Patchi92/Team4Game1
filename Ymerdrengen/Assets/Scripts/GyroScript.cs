using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GyroScript : MonoBehaviour {
    Text text;
    // Use this for initialization


    float xCal;
    float zCal;
    GameObject ball;

    //Matrix4x4 calibrationMatrix;
    //Vector3 wantedDeadZone = Vector3.zero;
    //Vector3 _InputDir;


    //For Jiri
    public float xDir;
    public float zDir;
    public float GravityForce;

    void Start () {
        Input.gyro.enabled = true;
        text = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        ball = GameObject.Find("Sphere").gameObject;
        //xCal = Input.gyro.attitude.x;
        //zCal = Input.gyro.attitude.z;

        xDir = 0;
        zDir = 0;
        GravityForce = 100f;

        //calibrateAccelerometer();

    }

    // Update is called once per frame
    void FixedUpdate () {

        //_InputDir = getGraviter(Input.gyro.gravity);
        //then in your code you use _InputDir instead of Input.acceleration for example 
        //transform.Translate(_InputDir.x, 0, -_InputDir.z);

        text.text = "" + Input.GetAxis("Horizontal"); ;
        //
        // acceleration stuff
        //
        //ball.GetComponent<Rigidbody>().AddForce(Input.acceleration.x*10, 0, Input.acceleration.y*10);
        //ball.GetComponent<Rigidbody>().AddForce(1, 0, 0);

        // gravity stuff
        float moveHorizontal = Input.gyro.gravity.x;
        float moveVertical = Input.gyro.gravity.y;
        //float moveHorizontal = _Inputdir.x;
        //float moveVertical = _Inputdir.y;
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Quaternion direction = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        movement = direction * movement;

        xDir = movement.x * GravityForce;
        zDir = movement.z * GravityForce;
        ball.GetComponent<Rigidbody>().AddForce(movement * GravityForce * Time.deltaTime);
    }

    //Method for calibration 
    //void calibrateAccelerometer()
    //{
    //    wantedDeadZone = Input.gyro.gravity;
    //    Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), wantedDeadZone);
    //    //create identity matrix ... rotate our matrix to match up with down vec
    //    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));
    //    //get the inverse of the matrix
    //    calibrationMatrix = matrix.inverse;

    //}

    ////Method to get the calibrated input 
    //Vector3 getGraviter(Vector3 gravitor)
    //{
    //    Vector3 grav = this.calibrationMatrix.MultiplyVector(gravitor);
    //    return grav;
    //}
}
