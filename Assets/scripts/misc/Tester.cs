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
        s.setBeat(3, Beat.Type.bash, Beat.Note.A);
        s.setBeat(7, Beat.Type.bash, Beat.Note.A);
        s.setBeat(11, Beat.Type.bash, Beat.Note.A);
        s.setBeat(15, Beat.Type.bash, Beat.Note.A);
        return s;
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
