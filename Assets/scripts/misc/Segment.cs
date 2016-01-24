using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Segment {
    
    //what type of segment this is, offensive where player can attack, defensive where player needs to dodge, special for special attacks
    public enum Classification {
        offensive,
        defensive,
        special
    }

    //types of attacks, nothing for don't play a note, bash for bash attacks, pierce for piercing attacks, dodge for need to block
    public enum Type {
        nothing,
        bash,
        pierce,
        dodge
    }

    //What note should play
    public enum Note {
        A,
        B
    }

    private int length; // length in number of beats
    private Classification classification; //the class of this segment 
    private Type[] typelist; //the type of attacks 
    private Note[] notelist; //the notes to play

    //create segment with specified beat length
    Segment(int length, Classification classification) {
        this.length = length;
        typelist = new Type[length];
        notelist = new Note[length];
        this.classification = classification;
    }
}
