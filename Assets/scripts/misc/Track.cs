using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track  {
    public int timeNum; //time signature numerator
    public int timeDen; //time signature denominator
    private int numLanes; //the number of lanes (number of enemies)
    private List<Enemy> tempenemylist; //temp enemy storage for adding
    private Enemy[] enemies; //finalized array of enemies
    private bool populated; //whether or not track is complete
    List<Segment>[] segmentList; //list of segements to loop through

    public Track() {
        timeNum = 4; //default is 4/4
        timeDen = 4; //default is 4/4
        numLanes = 0; //start with no lanes
        tempenemylist = new List<Enemy>();
        enemies = null;
        populated = false;
        segmentList = null;
    }

    public bool isReady() { //Check to see if the track has been populated
        return populated;
    }

    public void addEnemy(Enemy enemy) {
        numLanes++;
        tempenemylist.Add(enemy);
    }

    public bool populate() {
        if (numLanes == 0) //if nothing to populate not finished
            return false;
        //populate the final array
        enemies = tempenemylist.ToArray();
        segmentList = new List<Segment>[numLanes];
        for (int i = 0; i < numLanes; i++) {
            segmentList[i] = new List<Segment>();
            segmentList[i].AddRange(enemies[i].getSegmentPool(Segment.Classification.offensive));
        }
        populated = true;
        return true;
    }

}
