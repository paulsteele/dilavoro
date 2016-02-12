using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track  {
    public int timeNum; //time signature numerator
    public int timeDen; //time signature denominator
    private int numLanes; //the number of lanes (number of enemies)
    private LinkedList<Enemy> enemylist; //to implement
    private bool populated; //whether or not track is complete
    LinkedList<Segment>[] segmentList; //list of segements to loop through

    public Track() {
        timeNum = 4; //default is 4/4
        timeDen = 4; //default is 4/4
        numLanes = 0; //start with no lanes
        enemylist = new LinkedList<Enemy>();
        populated = false;
    }

    public bool isReady() { //Check to see if the track has been populated
        return populated;
    }

    public void addEnemy(Enemy enemy) {
        numLanes++;
        enemylist.AddLast(enemy);
    }

    public bool populate() {
        if (numLanes == 0) //if nothing to populate not finished
            return false;
        int i = 0;
        foreach (Enemy e in enemylist) {
            //temporary selection algo
            segmentList[i] = e.getSegmentPool(Segment.Classification.offensive);
            i++;
        }

        populated = true;
        return true;
    }

    public LinkedList<Segment> getLane(int index) {
        if (index >= 0 && index < numLanes) {
            return segmentList[index];
        }
        else
            return null;
    }
}
