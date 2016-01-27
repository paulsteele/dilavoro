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

    Track() {
        timeNum = 4; //default is 4/4
        timeDen = 4; //default is 4/4
        numLanes = 0; //start with no lanes
        enemylist = null;
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
        for (int i = 0; i < numLanes; i++) { //initialize 
            segmentList[i] = new LinkedList<Segment>();
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
