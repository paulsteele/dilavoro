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
    private float width;
    private float height;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        //get height and width for purposes of collision detection
        Sprite sp = GetComponent<SpriteRenderer>().sprite;
        width = (sp.bounds.max.x - sp.bounds.min.x) / 2;
        height = (sp.bounds.max.y - sp.bounds.min.y) / 2;
        width *= body.transform.localScale.x;
        height *= body.transform.localScale.y;
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
        if (Input.GetKey(keyUp)) {
            
            Vector2 topLeft = new Vector2(body.position.x - width, body.position.y - height);
            Vector2 bottomRight = new Vector2(body.position.x + width, body.position.y - height -1 );
            bool grounded = Physics2D.OverlapArea(topLeft, bottomRight, LayerMask.NameToLayer("StaticTerrain"));
            if (grounded && deltaSpeedY.CompareTo(0.0f) == 0) {
                deltaSpeedY += jumpSpeed;
            }
            
        }

        //apply movement
        vector = new Vector2(deltaSpeedX, deltaSpeedY);
        body.velocity = vector;
    }
}
