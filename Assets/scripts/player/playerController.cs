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
	
	// FixedUpdate is called once per timestep
	void FixedUpdate () {
        float deltaspeed = 0.0f;
        //left movement
	    if (Input.GetKey(keyLeft)) {
            //apply instantaneous force to go left
            deltaspeed -= horizontalMoveSpeed;
            
        }
        //right movement
        if (Input.GetKey(keyRight)) {
            //apply instanteous force to go right
            deltaspeed += horizontalMoveSpeed;
        }

        //apply movement
        vector = new Vector2(deltaspeed, body.velocity.y);
        body.velocity = vector;
    }
}
