using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track  {
    private int bpm; //the beats per minute
    public int timeNum; //time signature numerator
    public int timeDen; //time signature denominator
    public int curBeat; //the current beat
    public int numLanes; //the number of lanes (number of enemies)
    //public LinkedList<Enemy> enemylist; //to implement

    LinkedList<Segment> segmentList; //list of segements to loop through
    
}
