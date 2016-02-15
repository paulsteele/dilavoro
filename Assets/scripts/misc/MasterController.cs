﻿using UnityEngine.UI;
using UnityEngine;
using System.Collections;


public class MasterController : MonoBehaviour {

    public bool resetPrefs;
    public bool runTests;
    public int bpm;
    public int hitLeeway;
    private int currentBeat;
    private int partialBeat;
    private int partialMax;
    private Text bpmText;
    private AudioSource bass;
    private Track track;
    private bool inBattle;
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
        partialMax = ( 60) * 4 *(int) (1 / Time.fixedDeltaTime); //typically will be 60 * 60 * 4(4 to make 16th notes one "beat", but this is the "bar" the bpm has to fill inorder to be on the next beat
        Debug.Log(partialMax);
        bpmText = GameObject.Find("bpmcounter").GetComponent<Text>();
        bass = GetComponent<AudioSource>();
        track = null;
        inBattle = false;
        if (runTests) {
            Tester.RunAllTests(this);
        }

        //for testing
        /*addTrack(new Track());
        Enemy e = new Enemy();
        e.testSegment();
        track.addEnemy(e);
        track.populate();
        */
    }

    public bool onHit() {
        int beat = checkBeat();
        if (beat != -1) {
            Debug.Log("hit on beat " + beat);
            return true;
        }
        Debug.Log("miss on beat " + currentBeat + " | p: " + partialBeat + "/" + partialMax);
        return false;
    }

    public int checkBeat() {
        if (partialBeat > (partialMax - (bpm * hitLeeway))) {
            int safebeat = currentBeat + 1;
            if (safebeat > bpm) {
                return 0;
            }
            return safebeat;
        }
        if (partialBeat < bpm * hitLeeway) {
            return currentBeat;
        }
        return -1;
    }

    void FixedUpdate() {
        bool beatChanged = advanceBeat();
        if (beatChanged && inBattle) {
            for (int i = 0; i < track.getNumLanes(); i++) {
                Debug.Log("beat " + currentBeat + " : " + track.getAction(i, currentBeat));
            }
        }
    }

    //returns true if beat changed
    private bool advanceBeat() {
        partialBeat += 4 * bpm; //add the bpm to the bar to fill
        if (partialBeat >= partialMax) { //if filled or overfilled 
            bass.Play();
            partialBeat = partialBeat - partialMax; //set the partial to the extra left over so eventually beat get back in sync
            currentBeat++;
            if (currentBeat > bpm) {
                currentBeat = 0;
            }
            bpmText.text = "" + currentBeat + " / " + bpm + " BPM";
            return true;
        }
        return false;
    }

    public void addTrack(Track track) {
        this.track = track;
    }

    public void setBattle(bool battle) {
        this.inBattle = battle;
    }
}
