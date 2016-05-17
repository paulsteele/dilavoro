using UnityEngine;
using System.Collections;

/**
Responsible for handling actions by the player
**/
public class PlayerController : MonoBehaviour {
    //Fields that determine what keybinding is what
    public KeyCode keyLeft; 
    public KeyCode keyRight;
    public KeyCode keyUp;
    public KeyCode keyAttack;
    public KeyCode keyAttackAlt;

    //horizontal movespeed is how fast left and right player moves
    public int horizontalMoveSpeed;
    //jumpspeed is how much velocity is added when jumping
    public int jumpSpeed;
    //body is the rigid body for the player, where physics are applied
    private Rigidbody2D body;
    //reference to master object to handle process commands that need more oversight than the player
    private MasterController master;
    //width and height are used for jumping / left right collision detection
    private float width;
    private float height;
    //input flags are used to queue inputs read from update() to carry over to fixedUpdate()
    private bool inputLeft;
    private bool inputRight;
    private bool inputJump;
    private bool inputAttack;
    //the sound to play on attacks
    private AudioSource attacksound;

	// Use this for initialization
	void Start () {
        //find the players body object
        body = GetComponent<Rigidbody2D>();
        //get height and width for purposes of collision detection
        BoxCollider2D sp = GetComponent<BoxCollider2D>();
        width = (sp.bounds.max.x - sp.bounds.min.x) / 2;
        height = (sp.bounds.max.y - sp.bounds.min.y) / 2;
        //false out all key input flags
        inputLeft = false;
        inputRight = false;
        inputJump = false;
        inputAttack = false;
        //attack sound source setup
        attacksound = GetComponent<AudioSource>();
        //find the master object
        master = GameObject.Find("Master").GetComponent<MasterController>();
}
	
	// Update is called once per frame, gather input
	void Update () {
        //this section checks to see if key is hit on frame and then sets a flag so physics calculations can be done if fixed time along with sound cues
        //left movement
        inputLeft = Input.GetKey(keyLeft);
        //right movement
        inputRight = Input.GetKey(keyRight);
        //jump
        inputJump = Input.GetKey(keyUp);
        //attack
        if (Input.GetKeyDown(keyAttack) || Input.GetKeyDown(keyAttackAlt)) {
            inputAttack = true;
        }
    }

    //called every timestep to have correct physics
    void FixedUpdate() {
        //these variables determine what the speed should be in each direction if no keys are pressed.
        //by default player shouldn't move left or right without keys, but vertically should keep doing what is was doing before
        float deltaSpeedX = 0.0f;
        float deltaSpeedY = body.velocity.y;
        //left movement check
        if (inputLeft) {
            //check to see if wall to left of player
            bool hitting = checkCollision(-width - .15f, height, -width - .16f, -height + .15f);
            if (!hitting) //only move left if not hitting wall
                //apply instantaneous force to go left
                deltaSpeedX -= horizontalMoveSpeed;
        }
        //right movement check
        if (inputRight) {
            //check to see if wall to right of player
            bool hitting = checkCollision(width, height, width + .01f, -height + .15f);
            if (!hitting) //only move right if not hitting wall
                //apply instanteous force to go right
                deltaSpeedX += horizontalMoveSpeed;
        }
        //jump check
        if (inputJump) {
            //check to see if ground is below the player
            bool grounded = checkCollision(-width + .1f, -height -.1f, width - .1f, -height -.3f);
            if (grounded ) { //only jump if grounded
                deltaSpeedY = jumpSpeed;
            }
        }

        //process attack input
        if (inputAttack) {
            //see if master thinks its a hit
            if (master.onHit()) {
                //attacksound.Play();
            }
            //forcibly remove the inputAttack since a click HAS to be registered
            inputAttack = false;
        }

        
        //fix to make climbing easier by raising the player
        Vector2 pos = body.position;
        pos.y -= .8f;
        body.position = pos;
        //apply movement
        Vector2 vector = new Vector2(deltaSpeedX, deltaSpeedY);
        body.velocity = vector;
        //move player back down to account for previous raise
        pos.y += .8f;
        body.position = pos;
    }

    //Checks to see if collision with ground occurs is specified region around player's collision box
    private bool checkCollision(float topleftxoffset, float topleftyoffset, float bottomrightxoffset, float bottomrightyoffset) {
        Vector2 topLeft = new Vector2(body.position.x + topleftxoffset, body.position.y + topleftyoffset);
        Vector2 bottomRight = new Vector2(body.position.x + bottomrightxoffset, body.position.y + bottomrightyoffset);
        return Physics2D.OverlapArea(topLeft, bottomRight, ~(LayerMask.NameToLayer("Static-Terrain")));
    }
}
