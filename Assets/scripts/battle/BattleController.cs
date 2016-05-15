using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
Controller for Battle
    Contains Queue for entities in the battle system
    **/
public class BattleController {
    //the list containing all the enemys
    List<Enemy> enemylist;
    //pool of enemies to pull from
    List<Enemy> turnPool;
    //list of measures to work through
    List<Measure> measures;
    //holder list to remove measures
    List<Measure> removeList;
    //flag that is true when battle is taking place
    bool battleStatus = false;
    //flag for internal starting up
    bool battleStartup = false;
    //The random generator class
    Random rng;
    //how many beats are in a measure
    float beatsPerBar;
    //how many measures are visible on
    float numVisibleMeasures;
    //width of every beat
    float widthPerBeat;
    //width a beat needs to change per frame
    float widthPerFrame;
    //the bpm that the master controller is following
    float bpm;
    //the beat the battle is in, doesn't loop like in the MasterConroller, just counts all the beats
    int beat;
    //how many beats until the next measure needs to be added to be displayed
    int measureCooldown;
    //how long the battle needs to wait to start
    int startCooldown;

    //Textures - set by master controller
    public Texture laneTexture;
    public Texture emptyTexture;
    public Texture pierceTexture;
    public Texture defendTexture;
    public Texture acceptTexture;

    public BattleController() {
        //init the enemy and measure data structures
        enemylist = new List<Enemy>();
        turnPool = new List<Enemy>();
        measures = new List<Measure>();
        removeList = new List<Measure>();
        //init the rng manipulator
        rng = new Random();
        //set the number of "16th notes" in a "measure"
        beatsPerBar = 16.0f;
        //set the number of measures that will be shown on the screen. A larger number will make the speed of the scrolling seem slower
        numVisibleMeasures = 2.0f;
        //how much screen space should seperate beats
        widthPerBeat = (Screen.width / numVisibleMeasures) / beatsPerBar;
        //how much screen space a measure should move each fixedupdate
        //widthPerFrame = (widthPerBeat / bpm) / Time.fixedDeltaTime;
        //set the initial beat counter to 0.
        beat = 0;
        //set the measure cooldown
        measureCooldown = 0;
        
    }

    public void addEnemy(Enemy enemy) {
        enemylist.Add(enemy);
    }

    public void setBPM(float bpm) {
        //set the bpm of the encounter
        this.bpm = bpm;
        // the number of beats per second
        float bps = bpm / 60.0f;
        //how far a beat should go each fixedupdate frame
        widthPerFrame = bps * Time.fixedDeltaTime * widthPerBeat;
    }

    public void startBattle(int cooldown) {
        //set the cooldown
        this.startCooldown = cooldown;
        //start the battle
        battleStartup = true;
        //reset the beat
        beat = 0;
    }
    //returns true if the battle is currently going on, and false if the battle is not taking place
    public bool inBattle() {
        return battleStatus;
    }

    //puts enemys in the list back into the pool
    void refillPool() {
        //clear anything still in there
        turnPool.Clear();
        //add everything from list
        turnPool.AddRange(enemylist);
    }

    //gets a random pool entry
    public Measure getNext() {
        //refill the pool if it is empty
        if (turnPool.Count == 0) {
            refillPool();
        }
        //find max in pool
        int max = turnPool.Count;
        //get the choice
        int index = Random.Range(0, max);
        //get a random choice from the pool
        Enemy enemy = turnPool[index];
        //remove the enemy from the pool
        turnPool.RemoveAt(index);
        //get the segment
        Segment segment = enemy.getSegmentPool()[0]; //TODO CHANGE TO RANDOM CHOICE
        //create the measure
        Measure measure = new Measure(segment, enemy, beat);
        return measure;
    }

    public void draw() {
        if (battleStatus) {
            //the bar
            GUI.DrawTexture(new Rect(0f, Screen.height - 111, Screen.width, 50), laneTexture, ScaleMode.StretchToFill);
            //the accept measure
            GUI.DrawTexture(new Rect(Screen.width - (24 * widthPerBeat), Screen.height - 150, 128, 128), acceptTexture, ScaleMode.StretchToFill);
            //draw every measure
            foreach(Measure measure in measures) {
                //get the x position of the measure
                float measureX = measure.getX();
                //get the segment for the measure
                Segment segment = measure.getSegment();
                //draw a line where the measure is
                //loop through the beats of the segment
                for (int i = 0; i < segment.getLength(); i++) {
                    //obtain the next beat
                    Beat beat = segment.getBeat(i);
                    //draw the beat
                    if (beat.getType() == Beat.Type.bash) {
                        GUI.DrawTexture(new Rect(measureX + (i * widthPerBeat), Screen.height - 110, 48, 48), defendTexture, ScaleMode.StretchToFill);
                    }
                    if (beat.getType() == Beat.Type.nothing) {
                        GUI.DrawTexture(new Rect(measureX + (i * widthPerBeat), Screen.height - 110, 48, 48), emptyTexture, ScaleMode.StretchToFill);
                    }
                    
                }
            }


            //draw measure thing

            //left accept
            //GUI.DrawTexture(new Rect(Screen.width - (numVisibleMeasures * widthPerBeat * 16) - 7 + 45, Screen.height - 90, 60, 30), leftAccept, ScaleMode.StretchToFill);
            //right accept
            //GUI.DrawTexture(new Rect(Screen.width - (numVisibleMeasures * widthPerBeat * 16) + 7 + 45, Screen.height - 90, 60, 30), rightAccept, ScaleMode.StretchToFill);
        }
    }

    /**
    Will be called in Master Controller's fixed update
        handles updating the position of the measures
    */
    public void fixedUpdate() {
        //only do updating if need to
        if (battleStatus) {
            //update the x position of 
            foreach (Measure measure in measures) {
                //Debug.Log(measure.getX());
                measure.setX(measure.getX() - widthPerFrame);
            }
        }
    }

    /**
    Will be called everytime the beat is actually changed in the mastercontroller
    */
    public void updateBeatChange() {
        //only run if battle is startingup
        if (battleStartup) {
            startCooldown--;
            if (startCooldown < 0) {
                battleStartup = false;
                battleStatus = true;
            }
        }
        //only run if battle actually started up
        if (battleStatus) {
            //check to see if need to spawn another measure
            if (measureCooldown == 0) { //means another measure is ready
                //get the next eligible measure
                Measure measure = getNext();
                //set cooldown to the length of this measure, get rid of one to make the length line up
                measureCooldown = measure.getSegment().getLength() - 1;

                //add the measure to the measures list
                measures.Add(measure);
            }
            else { //means another measure is not ready
                measureCooldown--;
            }

            //cleanup old measures that are off the screen
            //first clear the remove list
            removeList.Clear();
            foreach (Measure measure in measures) {
                //calculate the difference in beats of when the measure was first drawn to now
                int difference = beat - measure.getStartBeat();
                //the threshold of when a measure is offscreen is when the number of visible beats on screen + the length of that measure is exceeded
                int threshold = ((int)(numVisibleMeasures * beatsPerBar)) + measure.getSegment().getLength();
                //if the differece of the start time is greater than what it can be seen on screen as
                if (difference > threshold) {
                    removeList.Add(measure);
                }
            }
            //remove all old measures (really should only ever be one)
            foreach (Measure measure in removeList) {
                measures.Remove(measure);
            }
            //update the beat
            beat++;
        }
    }
    
}
