using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

    public string focustext;
    public float horizontalPadding;
    private GameObject focus;
    private float centerx;
	// Use this for initialization
	void Start () {
        focus = GameObject.Find(focustext);
    }
	
	// Update is called once per frame
	void Update () {
        //bounds check
        float difference = focus.transform.position.x - transform.position.x;
        if (Mathf.Abs(difference).CompareTo(horizontalPadding) > 0) {
            int comp = difference.CompareTo(0);
            Vector3 move;
            if (comp > 0) {
                move = new Vector3(focus.transform.position.x - horizontalPadding, transform.position.y, transform.position.z);

            }
            else if (comp < 0) {
                move = new Vector3(focus.transform.position.x + horizontalPadding, transform.position.y, transform.position.z);
            }
            else {
                return;
            }
            transform.position = move;
        }
        

    }
}
