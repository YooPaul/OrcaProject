﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioManager : MonoBehaviour {

    public AudioSource aboveSrc;
    public AudioSource belowSrc;

    
    public Transform player;
    public Transform OceanPlane;
    public AudioClip[] clips;
    public Animator anim;
    public GameObject pod;

    private int nextClip;
    private bool prevState;
    private float oceanHeight;
    private bool goneAbove;

    // Use this for initialization
    void Awake() {
        nextClip = 1;
        prevState = IsAboveWater();
        aboveSrc.Pause();
        aboveSrc.clip = clips[0];
        oceanHeight = OceanPlane.position.y;
        goneAbove = false;
    }
	
	// Update is called once per frame
	void Update () {
        bool state = IsAboveWater();

        if (IsAboveWater())
        {
            if(!pod.active)
            {
                pod.SetActive(true);
    
            }
            
        }

        if (state)
        {
            
            if(state != prevState) {
                belowSrc.Stop();
                aboveSrc.Play();
                prevState = state;
                anim.SetBool("User_breaching", true);
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
                anim.SetBool("User_breaching", false);
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
