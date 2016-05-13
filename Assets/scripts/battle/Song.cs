using UnityEngine;
using System.Collections;

public class Song {
    public AudioClip full;
    public AudioClip seventy;
    public AudioClip forty;
    public AudioClip twenty;

    public Song (AudioClip full, AudioClip seventy, AudioClip forty, AudioClip twenty) {
        this.full = full;
        this.seventy = seventy;
        this.forty = forty;
        this.twenty = twenty;
    }

}
