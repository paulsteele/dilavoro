using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy {

    private HashSet<Segment> offensivePool;
    private HashSet<Segment> defensivePool;

	// Use this for initialization
	public Enemy () {
        offensivePool = new HashSet<Segment>();
        defensivePool = new HashSet<Segment>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public HashSet<Segment> getSegmentPool(Segment.Classification classification) {
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
            offensivePool.Add(segment);
        }
        if (segment.getClassification() == Segment.Classification.defensive) {
            defensivePool.Add(segment);
        }
    }

    public void testSegment() {
        Segment s = new Segment(16, Segment.Classification.offensive);
        s.addBeat(3, Segment.Type.bash, Segment.Note.A);
        s.addBeat(7, Segment.Type.bash, Segment.Note.A);
        s.addBeat(11, Segment.Type.bash, Segment.Note.A);
        s.addBeat(15, Segment.Type.bash, Segment.Note.A);
    }
}
