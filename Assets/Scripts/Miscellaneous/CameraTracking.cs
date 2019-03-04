using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    public Transform TrackingObject;
    public Vector3 Offset;
	// Update is called once per frame
	void Update () {
        transform.position = TrackingObject.transform.position + Offset;
	}
}
