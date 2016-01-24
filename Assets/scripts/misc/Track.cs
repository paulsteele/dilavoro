using UnityEngine;
using System.Collections;

public class Track  {
    //types of attacks, nothing for don't play a note, bash for bash attacks, pierce for piercing attacks, dodge for need to block
    public enum type {
        nothing,
        bash,
        pierce,
        dodge
    }
    //types of note that should play.
    public enum notes {
        A,
        B
    }

    private int bpm;
}
