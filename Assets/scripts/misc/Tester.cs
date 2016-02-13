using UnityEngine;
using System.Collections;

public class Tester {
    static int passing = 0;
    static int failing = 0;

    //Testing Master Caller
    public static void RunAllTests() {
        passing = 0;
        failing = 0;

        if (TestRangeMap())
            passing++;
        else
            failing++;
        
    }

    //test function to check if working
    private static bool TestRangeMap() {
        int[] vals = { 37, 14, 56, 22 };
        int max = 0;
        for (int i = 0; i < vals.Length; i++) {
            max += vals[i];
        }
        RangeMap rm = new RangeMap(vals.Length, max);
        for (int i = 0; i < vals.Length; i++) {
            rm.addKey(vals[i]);
        }

        Assert(rm.getIndex(33), 0);
        Assert(rm.getIndex(37), 1);
        Assert(rm.getIndex(50), 1);
        Assert(rm.getIndex(51), 2);
        Assert(rm.getIndex(100), 2);
        Assert(rm.getIndex(107), 3);
        Assert(rm.getIndex(129), 0);
        return true;
    }

    private static bool Assert(int actual, int expected) {
        if (actual == expected) {
            return true;
        }
        else {
            Debug.LogError("expected: " + expected + " actual: " + actual + " Stack Trace: " + StackTraceUtility.ExtractStackTrace());
            return false;
        }
    }
}
