using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhalePath : MonoBehaviour {

    public Animator anim;

    [SerializeField]
    public Transform player, whale;
    public float moveSpeed = 3.0f;
    public float triggerDistance, playerTrigger, celebrationTime;
    public List<Transform> waypoints;


    private int current, target;
    private bool reached, prev, celebrating;
    private float celebrationStart = 0.0f;

    // Start is called before the first frame update
    void Start() {
        current = 0;
        target = 1;
        reached = true;
        prev = true;
    }

    // Update is called once per frame
    void Update() {

        if (atTarget()) {
            reached = true;
            if (reached != prev) {
                anim.SetBool("Follow", true);
                prev = reached;
            }

            if ((player.position - whale.position).magnitude < playerTrigger) {
                celebrating = true;
                anim.SetBool("Celebrating", true);
                celebrationStart = Time.time;
            }
            if(Time.time - celebrationStart < celebrationTime != celebrating) {
                celebrating = false;
                anim.SetBool("Celebrating", false);
                current = target;
                if (target < waypoints.Count - 1) {
                    target++;
                }
            }

            

        }

        if (celebrating) {
            Debug.Log("Celebrting: " + celebrating);
        }

        Vector3 move = waypoints[target].position - whale.position;
        whale.position += move.normalized * moveSpeed * Time.deltaTime;
    }

    private bool atTarget() {
        return ((whale.position - waypoints[target].position).magnitude < triggerDistance);
    }

}
