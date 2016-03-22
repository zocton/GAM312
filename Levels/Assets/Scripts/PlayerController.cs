using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveForce = 10.0f,
        turnRate = 4f,
        jumpSpeed = 0.3f,
        time = 4.0f;
    private Rigidbody rigidbody;
    public float maxSpeed = 5f;
    public bool isFalling = false;
    private UnityChanController modelController;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        modelController = GetComponentInChildren<UnityChanController>();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); modelController.BeginJumpAnimation(); }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "FLOOR")
        {
            isFalling = false;
            Debug.Log("collided");
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (rigidbody.velocity.magnitude <= maxSpeed)
        {
            rigidbody.AddRelativeForce(new Vector3(/*horizontal*/0f, 0f, vertical * moveForce));
            rigidbody.angularVelocity = new Vector3(0f, horizontal * turnRate, 0f);
        }
        else
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        modelController.AnimateForwardMotion(vertical);
        modelController.AnimateTurningMotion(horizontal);
    }  

    void Jump()
    {
        if (isFalling == false)
        {
            rigidbody.velocity += Vector3.up * moveForce;
            Debug.Log(rigidbody.transform.position.y);
            isFalling = true;
        }
    }
}
