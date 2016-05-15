using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
    Container for audio clips for a song
    */
public class Song {
    //That actual audio clips
    public List<AudioClip> audio;

    public Song () {
        //init the list
        audio = new List<AudioClip>();
    }

    public void addClip(AudioClip clip) {
        //add the clip to the container
        audio.Add(clip);
    }

    public List<AudioClip> getClips() {
        return audio;
    }

}
