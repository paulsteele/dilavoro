using UnityEngine;
using System.Collections;

/**
RangeMap is an internal storage class to act like an inequality hash map
    essentially is given the length of a bunch of lists and returns which list to look in , and what position is desired
**/
public class RangeMap {
    //the keys which return an index which point to segment which will contain values in that range
    int[] keys;
    //size of the array
    int size;
    //max number of range numbers, used for modulus
    int max;
    //current index inserting to
    int currentIndex; 

    
    //set up the map given a size of number of array, and the max total to be held within
    public RangeMap(int size) {
        this.size = size;
        this.max = 0;
        keys = new int[size];
        currentIndex = 0;
    }

    //adds a key to the rangemap, only will do work if the map isn't full
    public void addKey(int key) {
        //make sure not full
        if (currentIndex < size) {
            keys[currentIndex] = key;
            currentIndex++;
            //add to the max
            max += key;
        }
    }

    //return the index of the segment and the wanted position
    public BeatCoordinate getBeatCoordinate(int value) {
        //find the safe index value
        int fixedval = value % max; 
        //then loop through array and subtract the lengths of each from desired position
        //until the desired one was found
        for (int i = 0; i < size; i++) {
            if (fixedval < keys[i]) {
                return new BeatCoordinate(i, fixedval);
            }
            else { //make the value less
                fixedval -= keys[i];
            }
        }

        return null;
    }  
}
