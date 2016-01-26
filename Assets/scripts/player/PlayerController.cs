using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public KeyCode keyLeft; 
    public KeyCode keyRight;
    public KeyCode keyUp;
    public KeyCode keyAttack;
    public int horizontalMoveSpeed;
    public int jumpSpeed;
    private Rigidbody2D body;
    
    private Vector2 vector;
    private MasterController master;
    private float width;
    private float height;
    private bool hitleft;
    private bool hitright;
    private bool hitjump;
    private bool hitattack;
    private AudioSource attacksound;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        //get height and width for purposes of collision detection
        BoxCollider2D sp = GetComponent<BoxCollider2D>();
        width = (sp.bounds.max.x - sp.bounds.min.x) / 2;
        height = (sp.bounds.max.y - sp.bounds.min.y) / 2;
        //false out all key hit flags
        hitleft = false;
        hitright = false;
        hitjump = false;
        hitattack = false;
        //attack sound source setup
        attacksound = GetComponent<AudioSource>();
        //setup master
        master = GameObject.Find("Master").GetComponent<MasterController>();
}
	
	// Update is called once per frame, gather input
	void Update () {
        //this section checks to see if key is hit on frame and then sets a flag so physics calculations can be done if fixed time along with sound cues
        //left movement
        hitleft = Input.GetKey(keyLeft);
        //right movement
        hitright = Input.GetKey(keyRight);
        //jump
        hitjump = Input.GetKey(keyUp);
        //attack
        if (Input.GetKeyDown(keyAttack)) {
            hitattack = true;
        }
    }

    void FixedUpdate() { //physic calculations handled here
        float deltaSpeedX = 0.0f;
        float deltaSpeedY = body.velocity.y;
        //left movement
        if (hitleft) {
            bool hitting = checkCollision(-width - .15f, height, -width - .16f, -height + .15f);
            if (!hitting) //only move left if not hitting wall
                //apply instantaneous force to go left
                deltaSpeedX -= horizontalMoveSpeed;
        }
        //right movement
        if (hitright) {
            bool hitting = checkCollision(width, height, width + .01f, -height + .15f);
            if (!hitting) //only move right if not hitting wall
                //apply instanteous force to go right
                deltaSpeedX += horizontalMoveSpeed;
        }
        //jump
        if (hitjump) {
            bool grounded = checkCollision(-width + .1f, -height -.1f, width - .1f, -height -.3f);
            if (grounded ) {
                deltaSpeedY = jumpSpeed;
            }
        }

        if (hitattack) {
            if (master.onHit()) {
                attacksound.Play();
            }
            hitattack = false;
        }

        //apply movement
        //fix to make climbing easier
        Vector2 pos = body.position;
        pos.y -= .8f;
        body.position = pos;
        vector = new Vector2(deltaSpeedX, deltaSpeedY);
        body.velocity = vector;
        pos.y += .8f;
        body.position = pos;
    }

    //Checks to see if collision with ground occurs is specified region around player's collision box
    private bool checkCollision(float topleftxoffset, float topleftyoffset, float bottomrightxoffset, float bottomrightyoffset) {
        Vector2 topLeft = new Vector2(body.position.x + topleftxoffset, body.position.y + topleftyoffset);
        Vector2 bottomRight = new Vector2(body.position.x + bottomrightxoffset, body.position.y + bottomrightyoffset);
        Debug.DrawLine(topLeft, bottomRight);
        return Physics2D.OverlapArea(topLeft, bottomRight, ~(LayerMask.NameToLayer("Static-Terrain")));
    }
}
