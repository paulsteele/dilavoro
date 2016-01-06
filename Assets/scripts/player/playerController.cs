using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    public KeyCode keyLeft; 
    public KeyCode keyRight;
    public KeyCode keyUp;
    public KeyCode keyAttack;
    public int horizontalMoveSpeed;
    public int jumpSpeed;

    private Rigidbody2D body;
    private Vector2 vector;
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
        Sprite sp = GetComponent<SpriteRenderer>().sprite;
        width = (sp.bounds.max.x - sp.bounds.min.x) / 2;
        height = (sp.bounds.max.y - sp.bounds.min.y) / 2;
        width *= body.transform.localScale.x;
        height *= body.transform.localScale.y;
        //false out all key hit flags
        hitleft = false;
        hitright = false;
        hitjump = false;
        hitattack = false;
        //attack sound source setup
        attacksound = GetComponent<AudioSource>();
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
            //apply instantaneous force to go left
            deltaSpeedX -= horizontalMoveSpeed;
        }
        //right movement
        if (hitright) {
            //apply instanteous force to go right
            deltaSpeedX += horizontalMoveSpeed;
        }
        //jump
        if (hitjump) {

            Vector2 topLeft = new Vector2(body.position.x - width, body.position.y - height);
            Vector2 bottomRight = new Vector2(body.position.x + width, body.position.y - height - 1);
            bool grounded = Physics2D.OverlapArea(topLeft, bottomRight, LayerMask.NameToLayer("StaticTerrain"));
            if (grounded && deltaSpeedY.CompareTo(0.0f) == 0) {
                deltaSpeedY += jumpSpeed;
            }

        }

        if (hitattack) {
            attacksound.Play();
            hitattack = false;
        }

        //apply movement
        vector = new Vector2(deltaSpeedX, deltaSpeedY);
        body.velocity = vector;
    }
}
