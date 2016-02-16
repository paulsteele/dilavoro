using UnityEngine;
using System.Collections;
/**
CameraController is in charge of everything related to the camera, its primary concern is calculating a bounding box that the player is allowed to move within
    if the player steps outside the box, then the camera needs to move its view to reinsert the player in the box
    
    horizontalPadding - left right allowance for focus movement
    verticalPadding - up down allowance for focus movement
**/
public class CameraController : MonoBehaviour {
    //the string name of the object to focus on. Done so that the camera doesn't have to be linked statically each time, it will automatically try to find player
    public string focustext;
    //the actual GameObject to follow
    private GameObject focus;
    //padding values for the bounding box of the camera
    public float horizontalPadding;
    public float verticalPadding;

	//Sets up the focus object
	void Start () {
        //sets the follow object to typically the player
        focus = GameObject.Find(focustext); 
    }
	
	// Update is called once per frame
	void Update () {
        //find the difference of the camera position and the focus position
        Vector2 difference = focus.transform.position - transform.position;
        //by default camera won't want to update its position 
        bool change = false;
        //values for the new position of camera
        float xchange = transform.position.x;
        float ychange = transform.position.y;

        //chechk if x difference is bigger than allowed padding
        if (Mathf.Abs(difference.x).CompareTo(horizontalPadding) > 0){
            //find exact direction
            int comp = difference.x.CompareTo(0);
            change = true;
            //move camera left
            if (comp > 0) {
                xchange = focus.transform.position.x - horizontalPadding;
            }
            //move camera right
            else if (comp < 0) {
                xchange = focus.transform.position.x + horizontalPadding;
            }
        }
        //check if y difference is bigger than allowed padding
        if (Mathf.Abs(difference.y).CompareTo(verticalPadding) > 0) {
            //find exact direction
            int comp = difference.y.CompareTo(0);
            change = true;
            //move camera down
            if (comp > 0) {
                ychange = focus.transform.position.y - verticalPadding;
            }
            //move camera up
            else if (comp < 0) {
                ychange = focus.transform.position.y + verticalPadding;
            }
        }
        //only if actual camera movement occured, update camera position
        if (change) {
            transform.position = new Vector3(xchange, ychange, transform.position.z);
        }
    }
}
