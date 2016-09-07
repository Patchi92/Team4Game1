// <copyright file="AudioScript.cs" company="Team4">
// Company copyright tag.
// </copyright>

using System.Collections;
using UnityEngine;

/// <summary>
/// This script controls the pause and playing of the music
/// </summary>
public class AudioScript : MonoBehaviour
{
    /// <summary>
    /// Minimum music clip length in seconds
    /// </summary>
    public float MinMusicTime = 10f;

    /// <summary>
    /// Maximum music clip length in seconds
    /// </summary>
    public float MaxMusicTime = 15f;

    /// <summary>
    /// Minimum pause time in seconds
    /// </summary>
    public float MinPauseTime = 5f;

    /// <summary>
    /// Maximum pause time in seconds
    /// </summary>
    public float MaxPauseTime = 10f;

    /// <summary>
    /// Audio source component
    /// </summary>
    public AudioSource audio;

    /// <summary>
    /// Current time
    /// </summary>
    private float time = 0f;

    /// <summary>
    /// Length of the music clip in seconds
    /// </summary>
    private float musicTime;

    /// <summary>
    /// Length of the pause in seconds
    /// </summary>
    private float pauseTime;

    /// <summary>
    /// Sets the random values for music and pausing
    /// </summary>
    private void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        SetRandomMusicTime();
        SetRandomPauseTime();
    }
    
    /// <summary>
    /// If and else if statements control the pausing and resuming of music
    /// while resetting and putting new random values
    /// </summary>
    private void FixedUpdate()
    {
        time += Time.deltaTime;

        Debug.Log("time " + time);
        Debug.Log("music " + musicTime);
        Debug.Log("pause " + pauseTime);

        if (audio.isPlaying && time >= musicTime)
        {
            audio.Pause();
            time = 0f;
            SetRandomMusicTime();
        }
        else if (!audio.isPlaying && time >= pauseTime)
        {
            audio.Play();
            time = 0f;
            SetRandomPauseTime();
        }
    }

    /// <summary>
    /// Sets the random time between minTime and maxTime for playing music
    /// </summary>
    private void SetRandomMusicTime()
    {
        musicTime = Random.Range(MinMusicTime, MaxMusicTime);
    }

    /// <summary>
    /// Sets the random time between minTime and maxTime for pausing music
    /// </summary>
    private void SetRandomPauseTime()
    {
        pauseTime = Random.Range(MinPauseTime, MaxPauseTime);
    }
}