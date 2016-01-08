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
    public AudioClip audio;
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
        partialMax = ( 60) * (int) (1 / Time.fixedDeltaTime); //typically will be 3000, but this is the "bar" the bpm has to fill inorder to be on the next beat
        Debug.Log(partialMax);
        bpmText = GameObject.Find("bpmcounter").GetComponent<Text>();
    }

    public void onHit() {
        Debug.Log("player hit");
    }

    void FixedUpdate() {
        partialBeat += bpm; //add the bpm to the bar to fill
        if (partialBeat >= partialMax) { //if filled or overfilled 
            
            partialBeat = partialBeat - partialMax; //set the partial to the extra left over so eventually beat get back in sync
            Debug.Log(partialBeat);
            currentBeat++;
            AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
            if (currentBeat > bpm) {
                currentBeat = 0;
            }
            bpmText.text = "" + currentBeat + " / " + bpm + " BPM";
        }
    }
}
