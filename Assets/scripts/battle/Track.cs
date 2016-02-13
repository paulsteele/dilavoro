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
    private List<Segment> segmentList; //list of segements to loop through, only for temp storage
    private Segment[][] segmentArray;
    private RangeMap[] rangeMaps; //the range maps for each track

    public Track() {
        timeNum = 4; //default is 4/4
        timeDen = 4; //default is 4/4
        numLanes = 0; //start with no lanes
        tempenemylist = new List<Enemy>();
        populated = false;
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
        segmentArray = new Segment[numLanes][];
        rangeMaps = new RangeMap[numLanes];
        for (int i = 0; i < numLanes; i++) {
            segmentList = new List<Segment>();
            segmentList.AddRange(enemies[i].getSegmentPool(Segment.Classification.offensive));
            segmentArray[i] = segmentList.ToArray();
            //now create the rangemap
            int numIndexes = segmentArray[i].Length;
            int maxvalue = 0;
            foreach (Segment s in segmentArray[i]) {
                maxvalue += s.getLength();
            }
            rangeMaps[i] = new RangeMap(numIndexes, maxvalue);
            maxvalue = 0;
            foreach (Segment s in segmentArray[i]) {
                maxvalue += s.getLength();
                rangeMaps[i].addKey(maxvalue);
            }
        }
        populated = true;
        return true;
    }

    public Segment.Type getAction(int lane, int beat) {
        if (lane < 0 || lane >= numLanes) {
            return Segment.Type.error;
        }
        return Segment.Type.error;
    } 

}
