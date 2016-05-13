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

   public Measure(Segment segment, Enemy enemy) {
        this.segment = segment;
        this.enemy = enemy;
        //starts at the edge of the screen
        x = Screen.width;
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
}
