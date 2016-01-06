using UnityEngine;
using System.Collections;

public class startup : MonoBehaviour {

    public bool resetPrefs;
	//faster startup
	void Start() {
        
        //clear prefs
        if (resetPrefs) {
            PlayerPrefs.DeleteAll();
            Debug.Log("Prefs reset");
            Application.Quit();
        }
    }
}
