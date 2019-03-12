using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    public Transform player, whale;
    public float playerSpeed = 3.0f;
    public float lookSpeed = 0.5f;
    public float triggerDistance;
    public List<Transform> waypoints;

    private int current, target;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        target = 1;
    }

    // Update is called once per frame
    void Update()
    {
        var targetRotation = Quaternion.LookRotation(waypoints[target].position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);

        if (!atTarget() && whale.GetComponent<WhalePath>().hasReached()) {
            Vector2 xy = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            Debug.Log(xy);
            Vector3 move = Vector3.zero;
            if (Input.GetKey(KeyCode.UpArrow) || (Mathf.Sign(xy.y) > 0 && xy.y != 0.0f))
            {
                move = waypoints[target].position - player.position;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || (Mathf.Sign(xy.y) < 0 && xy.y != 0.0f))
            {
                move = waypoints[current].position - player.position;
            }
            player.position += move.normalized * playerSpeed * Time.deltaTime;
        }
        
    }

    private bool atTarget() {
        return ((player.position - waypoints[target].position).magnitude < triggerDistance);
    }

    public void updateTarget()
    {
        current = target;
        if (target < waypoints.Count - 1)
        {
            target++;
        }
        else if (target == waypoints.Count - 1)
        {
            target--;
        }
    }
    
}
