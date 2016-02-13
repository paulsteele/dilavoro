using UnityEngine;
using System.Collections;

public class RangeMap {
    int[] keys; //the keys which return an index which point to segment which will contain values in that range
    int size; //size of the array
    int max; //max number of range numbers, used for modulus
    int current; //current index inserting to
    //this map returns the index where the given range is caught inbetween two range values
    public RangeMap(int size, int max) {
        this.size = size;
        this.max = max;
        keys = new int[size];
        current = 0;
    }

    public void addKey(int key) {
        if (current < size) {
            keys[current] = key;
            current++;
        }
    }

    public int getIndex(int value) {
        int fixedval = value % max; //find the safe index value
        for (int i = 0; i < size; i++) {
            if (fixedval < keys[i]) {
                return i;
            }
        }

        return -1;
    }  
}