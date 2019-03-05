using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Transform holdertransform;

	// Use this for initialization
	void Start () {
        GameObject holder = GameObject.Find("OVR_Rig_Holder");
        holdertransform = holder.transform;
    }
	
	// Update is called once per frame
	void Update () {
        //Vector2 xz = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        Vector2 xz = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        Debug.Log(xz);

        float thumbStickY = xz.y != 0 ? Mathf.Sign(xz.y) : 0;
        float thumbStickX = xz.x != 0 ? Mathf.Sign(xz.x) : 0;

        float translation = thumbStickY * 2.0f;
        float translationX = thumbStickX * 2.0f;
        //float rotation = thumbStickX * 20.0f;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        translationX *= Time.deltaTime;
        //rotation *= Time.deltaTime;

        // Move translation along the object's z-axis


        // Rotate around our y-axis
        //transform.Rotate(0, rotation, 0);

        float y = 0.0f;

        if (OVRInput.Get(OVRInput.RawButton.B)) // || Input.GetKeyDown(KeyCode.Space))
        {
            y = 4.0f * Time.deltaTime;
        }
        else if (OVRInput.Get(OVRInput.RawButton.A))
        {
            y = -4.0f * Time.deltaTime;
        }



        //Debug.Log("Before: " + transform.position);
        //transform.Translate(translationX, y, translation);
        holdertransform.Translate(translationX, y, translation);
        Debug.Log(transform.rotation.eulerAngles.x);
        //Debug.Log(transform.rotation.eulerAngles.y);
        //transform.position += new Vector3(translationX, y, translation);
        //Debug.Log("After: " + transform.position);
        //Vector2 zy = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        //this.transform.position += new Vector3(0, zy.y, zy.x);
    }
}
