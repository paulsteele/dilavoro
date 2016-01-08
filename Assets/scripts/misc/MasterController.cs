using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MasterController : MonoBehaviour {

    public bool resetPrefs;
    public int bpm;
    private int currentBeat;
    private int partialBeat;
    private int partialMax;
    private Text bpmText;
    //faster startup
	void Start() { 
        //clear prefs
        if (resetPrefs) {
            PlayerPrefs.DeleteAll();
            Debug.Log("Prefs reset");
            Application.Quit();
        }
        //setup bpm counters
        currentBeat = 0;
        partialBeat = 0;
        partialMax = ( 60) * (int) (1 / Time.fixedDeltaTime);
        bpmText = GameObject.Find("bpmcounter").GetComponent<Text>();
    }

    public void onHit() {
        Debug.Log("player hit");
    }

    void FixedUpdate() {
        partialBeat += bpm;
        //bpmText.text = "" + partialBeat + " / " + partialMax + " BPM";
        if (partialBeat == partialMax) {
            partialBeat = 0;
            currentBeat++;
            if (currentBeat > bpm) {
                currentBeat = 0;
            }
            bpmText.text = "" + currentBeat + " / " + bpm + " BPM";
        }
    }
}
