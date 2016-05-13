using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
MasterController is in charge of coordinating everything that goes on the backend of the system.
    Primarily responsible for
    Keeping track of the beat, and bpm 
    Keeping track of UI info(at least sending info)
    Determining whether a hit matches what the track specified

    NOTE on beats:
    beats are not the traditional beats per minute that musicians are accustomed to. A beat in this engine if 1/4 of a beat since 16th
    notes are the designed smallest unit of measure (16th notes are 1/4 of a beat). BPMS will seem inflated from traditional bpms of songs, but 
    they are simply multiplied by 4. This should be taken into consideration when designing segments

    NOTE on timekeeping:
    the system can only accurately handle bpms that are factors of 3600 * 4(for the 16th notes again)
    the system will work with any bpm, but due frames only being check every 60th of a second, beats not factors of 3600 won't be checked until after they are 
    over which will make them seem off, currently the partial extra is added to these non-factors so that every 10 or so beats will be back in line.
**/
public class MasterController : MonoBehaviour {
    //flag to see if unity needs to reset it preferences (used mainly if adjusting window size)
    public bool resetPrefs;
    //flag to see if the test suite should be run
    public bool runTests;
    //the number of "beats" per minute
    public int bpm;
    //the current beat that the game is on
    private int currentBeat;
    //the partial status of a beat, since beats are many frames it is needed to keep track of where along the beat progress the game is on
    private int partialBeat;
    //the threshold partialBeat needs to be in order to advance the currentBeat
    private int beatThreshold;
    //the amount of leeway the player has to hit the note they want
    public int hitLeeway;
    //UI STUFF VERY LIKELY TO CHANGE
    //the text object for displaying BPM
    private Text bpmText;
    //Handles anything dealing with the batte engine
    private BattleController battleManager;
    //List of AudioSources
    private List<AudioSource> audioSources;


    public Texture laneTexture;
    public Texture measureLine;
    public Texture emptyTexture;
    public Texture pierceTexture;
    public Texture defendTexture;
    public Texture acceptTexture;


    float measureWidth;
    float widthPerBeat;
    float beatsPerFrame;
    float widthPerFrame;
    
    //Startup functions, does checking for test running and reset prefs
    //the initializing everything that needs intializing
	void Start() { 
        //if resetPrefs selected reset all unity preferences
        if (resetPrefs) {
            PlayerPrefs.DeleteAll();
            Debug.Log("Prefs reset");
            Application.Quit();
        }
        //setup beat counters to inital values
        currentBeat = 0;
        partialBeat = 0;
        //typically will be 60 * 4 * 60 (delta time should be 60Hz)
        //chosen since 60 units (multipled by 4 for 16th notes) in 60 seconds should be equivilent to musician's 60bpm
        beatThreshold = ( 60) * 4 *(int) (1 / Time.fixedDeltaTime); 
        //log the threshold number
        Debug.Log(beatThreshold);
        //find UI components and assign to fields
        bpmText = GameObject.Find("bpmcounter").GetComponent<Text>();
        //measureWidth = Screen.width / numVisibleMeasures;
        //widthPerBeat = measureWidth / 16.0f;
        beatsPerFrame = (bpm / 60.0f) / 60.0f;
        //widthPerFrame = beatsPerFrame * widthPerBeat;
        //set up battle manager
        battleManager = new BattleController();
        //set default bpm for battlemanager
        battleManager.setBPM(bpm);
        //push textures
        battleManager.laneTexture = this.laneTexture;
        battleManager.emptyTexture = this.emptyTexture;
        battleManager.defendTexture = this.defendTexture;
        battleManager.pierceTexture = this.pierceTexture;
        battleManager.acceptTexture = this.acceptTexture;
        //Init the audio sources
        audioSources = new List<AudioSource>();

        //run tests at end so no errors when doing integration testing
        if (runTests) {
            Tester.RunAllTests(this);
        }
    }

    //------------------------SECTION NEEDS TO BE REDONE-----------------------------------

    //the player object calls this when they process an attack,
    //will check to see if the player hit what they should have, on time, or not
    public bool onHit() {
        //checks to see if beat fell in leeway or not
        int beat = checkBeat();
        //if beat did successfully hit
        if (beat != -1) {
            Debug.Log("hit on beat " + beat);
            return true;
        }
        //otherwise return a miss
        Debug.Log("miss on beat " + currentBeat + " | p: " + partialBeat + "/" + beatThreshold);
        return false;
    }

    //checks to see if a beat falls withing the leeway chosen to be most effective
    public int checkBeat() {
        //if the partial beat is more than lower limit (threshold - number of frames)
        if (partialBeat > (beatThreshold - (bpm * hitLeeway))) {
            //do some calc to make sure the beat isn't actually beat zero
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

    //--------------------------------------------------------------------------------------

    //beat calculations are very dependent on time and not framerate
    void FixedUpdate() {
        //do calculataions for adavancing the beat and see if a beat changed
        bool beatChanged = advanceBeat();
        
        
        //do actions when beats change
        if (beatChanged) {
            //update battleManager that needs to be done per beat
            battleManager.updateBeatChange();
        }
        //update the battle in fixed timescale
        battleManager.fixedUpdate();
    }

    void spawnTrackElements() {
        
    } 

    //Handles any drawing calls that need to be done for UI
    void OnGUI() {
        battleManager.draw();

    }
    

    //Perform operations for advancing the beat; the beat must be moved up by 4 * bpm to correctly account for time
    //will return true if the beat managed to get higher than the threshold
    //this implementation currently adds the overflow of uneven beats to the next partial so that eventially it gets back on track
    //this should hopefully allow the leeway detection to compensate for lack of perfect accuracy
    private bool advanceBeat() {
        partialBeat += 4 * bpm; //add the bpm * 4 (for proper speed) to the progress of the beat
        if (partialBeat >= beatThreshold) { //if a full beat has passed, or overpassed
            //set the partial to the extra left over so eventually beat get back in sync
            partialBeat = partialBeat - beatThreshold; 
            //advance the currentBeat and reset to 0 if needed
            currentBeat++;
            if (currentBeat >= bpm) {
                currentBeat = 0;
            }
            //change the text of the UI element
            bpmText.text = "" + currentBeat + " / " + bpm + " BPM";
            return true;
        }
        return false;
    }

    //returns the battle controller
    public BattleController getBattleController() {
        return battleManager;
    }

    //helper to create a background track, derived from http://answers.unity3d.com/questions/240468/how-to-play-multiple-audioclips-from-the-same-obje.html
    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip; 
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol; 
        return newAudio; 
 }
    //puts a song in to play
    public void loadSong(Song song) {
        AudioSource full = AddAudio(song.full, true, false, 1.0f);
        audioSources.Add(full);
    }

    //plays tracks in the song list 
    public void playSong() {
        float bps = bpm / 60.0f;
        int thresh = 16;
        if (partialBeat != 0) {
            thresh--;
        }
        // 3 * beatLenght + partial beat = number of partial beats 
        // number of partial beats / 
        float time = (((thresh * beatThreshold) + partialBeat) / (float)beatThreshold) / bps;
        Debug.Log(time);

        foreach (AudioSource audio in audioSources) {
            audio.PlayDelayed(time);
        }
    }
}
