using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
Segements are the basic 'blocks' of actions taken place in the battle system,
    
    each segement has a classification
    offensive - when the PLAYER is on the offensive 
    defensive - when the PLAYER is on the defensive
    special - type for when something unordinary needs to happen

    NOTE on length: best be a multiple of 4 or will have a bad time
**/
public class Segment {
    
    //what type of segment this is, offensive where player can attack, defensive where player needs to dodge, special for special attacks
    public enum Classification {
        offensive,
        defensive,
        special
    }


    private int length; // length in number of beats
    private Classification classification; //the class of this segment 
    private Beat[] beats; // the beats to play

    //create segment with specified beat length
    public Segment(int length, Classification classification) {
        //set up arrays of types and notes
        this.length = length;
        beats = new Beat[length];
        //init everything to empty
        for (int i = 0; i < length; i++) {
            beats[i] = new Beat(Beat.Type.nothing, Beat.Note.empty);
        }
        //set the segments classification
        this.classification = classification;
    }
    
    //adds a beat to the segment pool at the given index
    public bool setBeat(int index, Beat.Type type, Beat.Note note) {
        if (index < 0 || index > length) //not valid beat
            return false;

        beats[index] = new Beat(type, note);
        return true;
    }

    public Beat getBeat(int index) {
        if (index < 0 || index >= length) //not valid beat
            return null;
        return beats[index];
    }

    //returns the length of the segment
    public int getLength() {
        return length;
    }

    //returns the classification of the segment
    public Classification getClassification() {
        return classification;
    }
}
