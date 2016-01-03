using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    public KeyCode keyLeft; 
    public KeyCode keyRight;
    public KeyCode keyUp;
    public int horizontalMoveSpeed;
    public int jumpSpeed;


    private Rigidbody2D body;
    private Vector2 vector;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// FixedUpdate is called once per timestep
	void FixedUpdate () {
        float deltaSpeedX = 0.0f;
        float deltaSpeedY = body.velocity.y;
        //left movement
	    if (Input.GetKey(keyLeft)) {
            //apply instantaneous force to go left
            deltaSpeedX -= horizontalMoveSpeed;
            
        }
        //right movement
        if (Input.GetKey(keyRight)) {
            //apply instanteous force to go right
            deltaSpeedX += horizontalMoveSpeed;
        }
        //jump
        if (Input.GetKeyDown(keyUp)) {
            deltaSpeedY += jumpSpeed;
        }

        //apply movement
        vector = new Vector2(deltaSpeedX, deltaSpeedY);
        body.velocity = vector;
    }
}
