using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    public KeyCode keyLeft;
    public KeyCode keyRight;
    public int horizontalMoveSpeed;
    private Rigidbody2D body;
    private Vector2 vector;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () {
	    if (Input.GetKey(keyLeft)) {
            vector = new Vector2(-1.0f, 0.0f) * horizontalMoveSpeed;
            body.AddForce(vector);
        }

        if (Input.GetKey(keyRight))
        {
            vector = new Vector2(1.0f, 0.0f) * horizontalMoveSpeed;
            body.AddForce(vector);
        }
	}
}
