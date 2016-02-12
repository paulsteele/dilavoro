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
        this.length = length;
        typelist = new Type[length];
        notelist = new Note[length];
        //init everything to empty
        for (int i = 0; i < length; i++) {
            typelist[i] = Type.nothing;
            notelist[i] = Note.empty;
        }
        this.classification = classification;
    }

    public bool addBeat(int index, Type type, Note note) {
        if (index < 0 || index > length) //not valid beat
            return false;
        typelist[index] = type;
        notelist[index] = note;
        return true;
    }

    public Type getType(int index) {
        if (index > 0 && index < length) //not valid beat
            return Type.nothing;
        return typelist[index];
    }

    public Note getNote(int index) {
        if (index > 0 && index < length) //not valid beat
            return Note.empty;
        return notelist[index];
    }

    public Classification getClassification() {
        return classification;
    }
}
