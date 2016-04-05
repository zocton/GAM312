using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float speed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");

		float amount = 0.707f * speed * Time.deltaTime;

		transform.position += new Vector3 (-amount*(vertical + horizontal), 0f, amount*(horizontal - vertical));
	}
}
