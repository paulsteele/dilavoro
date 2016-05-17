using UnityEngine;
using System.Collections;

/**
Measures are the blocks that are used for battle calculation and display
**/
public class Measure  {
    //the segment for the measure
    private Segment segment;
    //the enemy instance for the measure
    private Enemy enemy;
    //the xlocation of this measure
    private float x;
    //the beat this measure started on
    private int startBeat;
    //the counter that tells what index of beat should be returned
    int beatCounter;
    //flag to tell the battle interpreter if the last beat has already passed or not
    bool expired;

   public Measure(Segment segment, Enemy enemy, int startBeat, int beatCounter) {
        //set the segment used as the base
        this.segment = segment;
        //set the enemy that is doing this beat
        this.enemy = enemy;
        //set the startBeat
        this.startBeat = startBeat;
        //starts at the edge of the screen
        x = Screen.width;
        //set beat counter
        this.beatCounter = beatCounter;
        //set expired to false
        expired = false;
    }

    public void advanceCounter() {
        //advance the counter
        beatCounter++;
        //if the last beat has passed
        if (beatCounter >= segment.getLength()) {
            expired = true;
        }
    }

    public Beat getBeat() {
        //if the measure hasn't made it yet or if too far
        if (beatCounter < 0 || beatCounter >= segment.getLength()) {
            return new Beat(Beat.Type.error, Beat.Note.empty);
        }
        //otherwise return the beat of the segment
        return segment.getBeat(beatCounter);
    }

    public Segment getSegment() {
        return segment;
    }

    public Enemy getEnemy() {
        return enemy;
    }

    public float getX() {
        return x;
    }

    public void setX(float x) {
        this.x = x;
    }

    public int getStartBeat() {
        return startBeat;
    }

    public bool isExpired() {
        return expired;
    }
}
