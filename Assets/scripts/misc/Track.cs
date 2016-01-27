using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track  {
    private int bpm; //the beats per minute
    public int timeNum; //time signature numerator
    public int timeDen; //time signature denominator
    public int numLanes; //the number of lanes (number of enemies)
    //public LinkedList<Enemy> enemylist; //to implement

    LinkedList<Segment> segmentList; //list of segements to loop through

    Track() {
        bpm = 0; //default to no time
        timeNum = 4; //default is 4/4
        timeDen = 4; //default is 4/4
        numLanes = 0; //start with no lanes
    }
}
