using UnityEngine;
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
        Enemy e = new Enemy();
        e.addSegment(getTestSegment());
        master.getBattleController().addEnemy(e);
        testAudio(master);
        master.getBattleController().startBattle();
    }

    public static void testAudio(MasterController master) {
        AudioClip full = new AudioClip();
        AudioClip seventy = new AudioClip();
        AudioClip forty = new AudioClip();
        AudioClip twenty = new AudioClip();
        full = Resources.Load<AudioClip>("music/test/Dilavoro Test");
        Debug.Log(full);
        Song s = new Song(full, seventy, forty, twenty);
        master.loadSong(s);
        master.playSong();

    }

    public static Segment getTestSegment() {
        Segment s = new Segment(16);
        s.setBeat(0, Beat.Type.bash, Beat.Note.A);
        s.setBeat(4, Beat.Type.bash, Beat.Note.A);
        s.setBeat(8, Beat.Type.bash, Beat.Note.A);
        s.setBeat(12, Beat.Type.bash, Beat.Note.A);
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
