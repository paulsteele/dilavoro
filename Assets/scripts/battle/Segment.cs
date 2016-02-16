using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
Segements are the basic 'blocks' of actions taken place in the battle system,
    
    each segement has a classification
    offensive - when the PLAYER is on the offensive 
    defensive - when the PLAYER is on the defensive
    special - type for when something unordinary needs to happen
    
    each segment also has a given length which specifies how many beats are held inside
    
    each beat has a type relating to what the PLAYER needs to do
    nothing - no action is to be taken place on this turn
    bash - the bash action is needed for PLAYER success
    pierce - the pierce action is needed for PLAYER success
    dodge - the dodge action is needed for PLAYER success
    error - internal type to return if there is a malformed type

    each beat also has a note which coresponds to the theoretical sound that should play

    NOTE on length: best be a multiple of 4 or will have a bad time
**/
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
        dodge,
        error
    }

    //What note should play
    public enum Note {
        empty,
        A,
        B
    }

    private int length; // length in number of beats
    private Classification classification; //the class of this segment 
    private Type[] typelist; //the type of attacks 
    private Note[] notelist; //the notes to play

    //create segment with specified beat length
    public Segment(int length, Classification classification) {
        //set up arrays of types and notes
        this.length = length;
        typelist = new Type[length];
        notelist = new Note[length];
        //init everything to empty
        for (int i = 0; i < length; i++) {
            typelist[i] = Type.nothing;
            notelist[i] = Note.empty;
        }
        //set the segments classification
        this.classification = classification;
    }
    
    //adds a beat to the segment pool at the given index
    public bool addBeat(int index, Type type, Note note) {
        if (index < 0 || index > length) //not valid beat
            return false;
        typelist[index] = type;
        notelist[index] = note;
        return true;
    }

    //gets the type of a given beat; error given on invalid beat
    public Type getType(int index) {
        if (index < 0 || index >= length) //not valid beat
            return Type.error;
        return typelist[index];
    }

    //gets the note of a given beat; empty if invalid
    public Note getNote(int index) {
        if (index < 0 || index >= length) //not valid beat
            return Note.empty;
        return notelist[index];
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
