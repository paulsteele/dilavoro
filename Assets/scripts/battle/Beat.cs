using UnityEngine;
using System.Collections;

/**
Storage class for beats which contain a type and and a note
    
    each beat has a type relating to what the PLAYER needs to do
    nothing - no action is to be taken place on this turn
    bash - the bash action is needed for PLAYER success
    pierce - the pierce action is needed for PLAYER success
    dodge - the dodge action is needed for PLAYER success
    error - internal type to return if there is a malformed type

    each beat also has a note which coresponds to the theoretical sound that should play
**/
public class Beat{
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

    //the type of the beat
    private Type type;
    //the note of the beat
    private Note note;

    //assign values of beat
    public Beat(Type type, Note note) {
        this.type = type;
        this.note = note;
    }

    public Type getType() {
        return type;
    }

    public  Note getNote() {
        return note;
    }
}
