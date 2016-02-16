using UnityEngine;
using System.Collections;

/**
A simple wrapper for two ints,
    index is the position of the desired segment
    position is the location of the desired beat in the segment
**/
public class BeatCoordinate {
    public readonly int index;
    public readonly int position;

    public BeatCoordinate(int index, int position) {
        this.index = index;
        this.position = position;
    }
}
