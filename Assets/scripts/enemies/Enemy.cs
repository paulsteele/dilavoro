using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    private LinkedList<Segment> offensivePool;
    private LinkedList<Segment> defensivePool;

	// Use this for initialization
	void Start () {
        offensivePool = new LinkedList<Segment>();
        defensivePool = new LinkedList<Segment>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public LinkedList<Segment> getSegmentPool(Segment.Classification classification) {
        if (classification == Segment.Classification.offensive) {
            return offensivePool;
        }
        if (classification == Segment.Classification.defensive) {
            return defensivePool;
        }
        return null;
    }

    public void addSegment(Segment segment) {
        if (segment.getClassification() == Segment.Classification.offensive) {
            offensivePool.AddLast(segment);
        }
        if (segment.getClassification() == Segment.Classification.defensive) {
            defensivePool.AddLast(segment);
        }
    }
}
