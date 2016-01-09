using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public string focustext;
    public float horizontalPadding;
    public float verticalPadding;
    private GameObject focus;

	// Use this for initialization
	void Start () {
        focus = GameObject.Find(focustext); //sets the follow object to typically the player
    }
	
	// Update is called once per frame
	void Update () {
        //bounds check
        Vector2 difference = focus.transform.position - transform.position; //find the difference of the camera position and the focus position
        bool change = false;
        //values for the new position of camera
        float xchange = transform.position.x;
        float ychange = transform.position.y;
        //chechk if x difference is bigger than allowed padding
        if (Mathf.Abs(difference.x).CompareTo(horizontalPadding) > 0){
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
