using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
Enemy is the basic object where the system looks to find a collection of segments. Each vertical lane on the battle UI is specific to an unique enemy instance
    Enemy has two distinct pools, an offensive pool and a defensive pool each containing segements of the respective classes
**/
public class Enemy {
    //the pools for each type of segment
    private List<Segment> movePool;

	//Set up pools of segments for adding into
	public Enemy () {
        movePool = new List<Segment>();
	}
	
    //returns the segment pool for the given classification, returns null if classification is not valid
    public List<Segment> getSegmentPool() {
        return movePool;
    }

    //add a segement to the enemy, will automatically be sorted by offensive / defensive
    public void addSegment(Segment segment) {
        movePool.Add(segment);
    }
}
