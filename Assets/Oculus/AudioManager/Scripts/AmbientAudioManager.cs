using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioManager : MonoBehaviour {

    public AudioSource aboveSrc;
    public AudioSource belowSrc;

    public float oceanHeight = 34.462f;
    public Transform player;
    public AudioClip[] clips;

    private int nextClip;
    private bool prevState;

    // Use this for initialization
    void Awake() {
        nextClip = 1;
        prevState = IsAboveWater();
        aboveSrc.Pause();
        aboveSrc.clip = clips[0];
    }
	
	// Update is called once per frame
	void Update () {
        bool state = IsAboveWater();
        if (state)
        {
            if(state != prevState) {
                belowSrc.Stop();
                aboveSrc.Play();
                prevState = state;
            }
            if (aboveSrc.isPlaying)
            {
                return;
            }
            aboveSrc.clip = clips[nextClip];
            aboveSrc.Play();

            // Cycle the queue
            nextClip++;
            if (nextClip >= clips.Length)
                nextClip = 0;
        }
        else
        {
            if (state != prevState)
            {
                ResetAbove();
                belowSrc.Play();
                prevState = state;
            }
        }
    }

    void ResetAbove()
    {
        aboveSrc.Stop();
        aboveSrc.clip = clips[0];
        nextClip = 1;
    }

    bool IsAboveWater()
    {
        return player.position.y > oceanHeight;
    }
}
