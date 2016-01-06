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
            float xchange;
            if (comp > 0) {
                xchange = focus.transform.position.x - horizontalPadding;

            }
            else if (comp < 0) {
                xchange = focus.transform.position.x + horizontalPadding;
            }
            else {
                return;
            }
            transform.position = new Vector3(xchange, transform.position.y, transform.position.z);
        }
    }
}
