using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = target.transform.position + offset;
        //transform.position = Vector3.back * offset.magnitude;
        transform.position = target.transform.position + target.transform.rotation * (Vector3.back * offset.magnitude) + new Vector3(0f, offset.y-3, 0f);
        transform.rotation = target.transform.rotation;
    }
}
