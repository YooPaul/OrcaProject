using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    public float playerSpeed = 3.0f;
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
        
        if (atTarget()) {
            current = target;
            if (target < waypoints.Count-1) {
                target++;
            }
        }

        Vector2 xy = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || (Mathf.Sign(xy.y) > 0 && xy.y != 0.0f)) {
            move = waypoints[target].position - player.position;
        } else if (Input.GetKey(KeyCode.DownArrow) || (Mathf.Sign(xy.y) < 0 && xy.y != 0.0f)) {
            move = waypoints[current].position - player.position;
        }
        player.position += move.normalized * playerSpeed * Time.deltaTime;
    }

    private bool atTarget() {
        return ((player.position - waypoints[target].position).magnitude < triggerDistance);
    }

    
}
