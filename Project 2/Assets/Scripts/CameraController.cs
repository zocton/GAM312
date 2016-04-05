using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float maxPanSpeed = 5f;
    public float zoomInterval = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        transform.position += new Vector3(vertical, 0f, -vertical) * maxPanSpeed * Time.deltaTime;
        transform.position += new Vector3(-horizontal, 0f, -horizontal) * maxPanSpeed * Time.deltaTime;

        
        if (Camera.main.orthographicSize < 1)
        {
            Camera.main.orthographicSize = 1;
        }
<<<<<<< HEAD
        else if (Camera.main.orthographicSize > 1)
        {
            if((Camera.main.orthographicSize -= (scroll * zoomInterval)) > 1)
=======
        else if (Camera.main.orthographicSize >= 1)
        {
            if((Camera.main.orthographicSize -= (scroll * zoomInterval)) >= 1)
>>>>>>> aca5f63b6fa1c4745742d266a3d771142849b5c4
            {
                Camera.main.orthographicSize -= scroll * zoomInterval * Time.deltaTime;
            }
        }
<<<<<<< HEAD
=======
        
>>>>>>> aca5f63b6fa1c4745742d266a3d771142849b5c4

        if (Camera.main.orthographicSize > 5)
        {
            Camera.main.orthographicSize = 5;
        }
        else
        {
            Camera.main.orthographicSize -= scroll * zoomInterval;
        }
    }
}
