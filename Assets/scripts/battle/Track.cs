using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
Track is the system used collection for keeping track of which segments go where
    It consists of an array of enemies that will be drawn from to pick segments from
    it has a number of 'Lanes' which are each individual enemeis
    the player will only be able to be on one lane at any given time each lane is essentially the traditional DDR lane
    a selection algorithm will be used to properly set up the lanes
    OFFENSIVE segments from each lane MAY overlap, so the player gets to choose whom they attack
    DEFENSIVE segments MUST NOT overlap, so that the player is never forced to take damage
    the algorithm will do all the hard work in setting things up so they work
    an example (O = offensive, D = defensive)
    |D-D-|----|O-O-|----|----|----|
    |----|-D-D|-O--|----|----|----|
    |----|----|--O-|----|----|-DD-|
    |----|----|--OO|-D--|----|----|
    |----|----|OOOO|----|-DD-|----|
    segments will loop and be modified as enemies are eleminated
    on the enemies that was eliminated, a replacement segment colum will be found

    when adding enemies they will be stored in a list, once populate() is called a faster access method will be switched to
**/
public class Track  {
    //the number of lanes (number of enemies)
    private int numLanes;
    //enemy storage for adding before populating
    private List<Enemy> enemyList;
    //two dimensional jagged array for storing the segments after they've been processed, the first index is
    //the lane, the second is the segment index(NOT BEAT INDEX, USE RANGEMAP TO DETERMINE)
    private List<Segment>[] segmentList;
    //the array of beats, contains what will be accessed during battle
    private Beat[][] beatArray;


    //simply for UI for display time signature
    //time signature numerator
    public int timeNum;
    //time signature denominator
    public int timeDen;


    //whether or not track is full and populated
    private bool populated; 
    
    //set up inital values
    public Track() {
        //default time signature is 4/4
        timeNum = 4;
        timeDen = 4;
        //start with no lanes
        numLanes = 0; 
        //start with empty enemy list
        enemyList = new List<Enemy>();
        //start out not populated
        populated = false;
    }

    //Check to see if the track has been populated
    public bool isReady() { 
        return populated;
    }

    //Adds an enemy reference to the tracks selection pool
    public void addEnemy(Enemy enemy) {
        numLanes++;
        enemyList.Add(enemy);
    }

    //handle populating the needed data structures for efficient running
    //must move enemies to an array
    public bool populate() {
        //if nothing to populate not finished
        if (numLanes == 0) 
            return false;
        //set up the segment array to get ready to hold segments
        segmentList = new List<Segment>[numLanes];
        //set up beat array to get ready to hold beats
        beatArray = new Beat[numLanes][];
        //counter variable set up
        int i = 0;
        //do for each lane(enemy)
        foreach (Enemy e in enemyList) {
            //create list to hold the segments in order as they'd like to be stored
            segmentList[i] = new List<Segment>();
            //SELECTION ALGO GOES HERE
            segmentList[i].AddRange(e.getSegmentPool(Segment.Classification.offensive));
            //find max number of beats in the list
            int numberOfBeats = 0;
            foreach (Segment s in segmentList[i]) {
                numberOfBeats += s.getLength();
            }
            //set up beatarray
            beatArray[i] = new Beat[numberOfBeats];
            //counter variable
            int j = 0;
            //go through each segment
            foreach (Segment s in segmentList[i]) {
                //go through each beat in the segment
                for (int k = 0; k < s.getLength(); k++) {
                    //assign the beat array the correct beat
                    beatArray[i][j] = s.getBeat(k);
                    j++;
                }
            }
            //advance counter
            i++;
        }
        //set populated, and return
        populated = true;
        return true;
    }

    //returns the number of lanes currently on the track
    public int getNumLanes() {
        return numLanes;
    }

    //returns the action found on the specified lane, on the specified beat number
    //accounts for segments smaller than bpm
    //OVERSIGHT DOES NOT ACCOUNT FOR SEGMENTS LONGER THAN BPM
    public Beat.Type getAction(int lane, int beat) {
        //make sure a valid lane size
        if (lane < 0 || lane >= numLanes || beat < 0) {
            return Beat.Type.error;
        }
        //get the beat looped throught the size
        beat = beat % beatArray[lane].Length; 

        return beatArray[lane][beat].getType();
    } 

}
