using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float maxPanSpeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(vertical, 0f, -vertical) * maxPanSpeed * Time.deltaTime;
        transform.position += new Vector3(-horizontal, 0f, -horizontal) * maxPanSpeed * Time.deltaTime;
    }
}
