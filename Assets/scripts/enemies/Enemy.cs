using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
Enemy is the basic object where the system looks to find a collection of segments. Each vertical lane on the battle UI is specific to an unique enemy instance
    Enemy has two distinct pools, an offensive pool and a defensive pool each containing segements of the respective classes
**/
public class Enemy {
    //the pools for each type of segment
    private HashSet<Segment> offensivePool;
    private HashSet<Segment> defensivePool;

	//Set up pools of segments for adding into
	public Enemy () {
        offensivePool = new HashSet<Segment>();
        defensivePool = new HashSet<Segment>();
	}
	
    //returns the segment pool for the given classification, returns null if classification is not valid
    public HashSet<Segment> getSegmentPool(Segment.Classification classification) {
        if (classification == Segment.Classification.offensive) {
            return offensivePool;
        }
        if (classification == Segment.Classification.defensive) {
            return defensivePool;
        }
        return null;
    }

    //add a segement to the enemy, will automatically be sorted by offensive / defensive
    public void addSegment(Segment segment) {
        if (segment.getClassification() == Segment.Classification.offensive) {
            offensivePool.Add(segment);
        }
        if (segment.getClassification() == Segment.Classification.defensive) {
            defensivePool.Add(segment);
        }
    }
}
