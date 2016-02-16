﻿using UnityEngine;
using System.Collections;
/**
Simpler Test Driver than Unity Test Tools, was way too complex, if looking to run, check box in MasterController, runTests
**/
public class Tester {
    static int passing = 0;
    static int failing = 0;


    //Testing Master Caller
    public static void RunAllTests(MasterController master) {
        passing = 0;
        failing = 0;

        if (TestRangeMap())
            passing++;
        else
            failing++;
        testTrack(master);
    }

    public static void testTrack(MasterController master) {
        Track track = new Track();
        Enemy e = new Enemy();
        e.addSegment(getTestSegment());
        track.addEnemy(e);
        track.populate();
        master.setTrack(track);
        master.setBattle(true);
    }

    public static Segment getTestSegment() {
        Segment s = new Segment(16, Segment.Classification.offensive);
        s.addBeat(3, Segment.Type.bash, Segment.Note.A);
        s.addBeat(7, Segment.Type.bash, Segment.Note.A);
        s.addBeat(11, Segment.Type.bash, Segment.Note.A);
        s.addBeat(15, Segment.Type.bash, Segment.Note.A);
        return s;
    }


    //test function to check if working
    private static bool TestRangeMap() {
        int[] vals = { 37, 14, 56, 22 };
        RangeMap rm = new RangeMap(vals.Length);
        for (int i = 0; i < vals.Length; i++) {
            rm.addKey(vals[i]);
        }
        /*
        Assert(rm.getIndex(33), 0);
        Assert(rm.getIndex(37), 1);
        Assert(rm.getIndex(50), 1);
        Assert(rm.getIndex(51), 2);
        Assert(rm.getIndex(100), 2);
        Assert(rm.getIndex(107), 3);
        Assert(rm.getIndex(129), 0);
        */
        BeatCoordinate bc1 = rm.getBeatCoordinate(33);
        Assert(bc1.index, 0);
        Assert(bc1.position, 33);
        BeatCoordinate bc2 = rm.getBeatCoordinate(37);
        Assert(bc2.index, 1);
        Assert(bc2.position, 0);
        BeatCoordinate bc3 = rm.getBeatCoordinate(50);
        Assert(bc3.index, 1);
        Assert(bc3.position, 13);
        BeatCoordinate bc4 = rm.getBeatCoordinate(51);
        Assert(bc4.index, 2);
        Assert(bc4.position, 0);
        BeatCoordinate bc5 = rm.getBeatCoordinate(100);
        Assert(bc5.index, 2);
        Assert(bc5.position, 49);
        BeatCoordinate bc6 = rm.getBeatCoordinate(107);
        Assert(bc6.index, 3);
        Assert(bc6.position, 0);
        BeatCoordinate bc7 = rm.getBeatCoordinate(129);
        Assert(bc7.index, 0);
        Assert(bc7.position, 0);
        BeatCoordinate bc8 = rm.getBeatCoordinate(200);
        Assert(bc8.index, 2);
        Assert(bc8.position, 20);
        return true;
    }

    private static bool Assert(int actual, int expected){
        if (actual == expected) {
            return true;
        }
        else {
            Debug.LogError("expected: " + expected + " actual: " + actual + " Stack Trace: " + StackTraceUtility.ExtractStackTrace());
            return false;
        }
    }
}
