// <copyright file="MoveScript.cs" company="Team4">
// Company copyright tag.
// </copyright>

using System.Collections;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    public float speed = 5;

    public Transform startPos, endPos;
    private Vector3 actualStartPos, actualEndPos;

    private Rigidbody rb;
    private GameObject girl;
    private float trackLength;
    private float timeTravelled;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        girl = GameObject.FindGameObjectWithTag("Girl");
        PlayerTracking();
        
    }

    void FixedUpdate ()
    {
        if (girl.GetComponent<AudioSource>().isPlaying)
        {
            timeTravelled += Time.deltaTime;
            transform.position = Vector3.Lerp(actualStartPos, actualEndPos, (timeTravelled*speed)/trackLength);
        }
    }

    /// <summary>
    /// Sets the start and end position while finding the length of the track
    /// </summary>
    void PlayerTracking()
    {
        actualStartPos = startPos.position;
        actualEndPos = endPos.position;
        trackLength = ((actualEndPos - actualStartPos).magnitude);
    }
}
