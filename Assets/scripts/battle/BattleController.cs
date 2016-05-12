using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
Controller for Battle
    Contains Queue for entities in the battle system
    **/
public class BattleController {
    //the list containing all the enemys
    List<Enemy> enemylist;
    //pool of enemies to pull from
    List<Enemy> turnPool;
    //list of measures to work through
    List<Measure> measures;
    //flag that is true when battle is taking place
    bool battleStatus = false;
    //The random generator class
    Random rng;


    //Textures
    public Texture laneTexture;

    public BattleController() {
        enemylist = new List<Enemy>();
        turnPool = new List<Enemy>();
        rng = new Random();
    }

    public void addEnemy(Enemy enemy) {
        enemylist.Add(enemy);
    }

    public void startBattle() {
        //start the battle
        battleStatus = true;
    }
    //returns true if the battle is currently going on, and false if the battle is not taking place
    public bool inBattle() {
        return battleStatus;
    }

    //puts enemys in the list back into the pool
    void refillPool() {
        //clear anything still in there
        turnPool.Clear();
        //add everything from list
        turnPool.AddRange(enemylist);
    }

    //gets a random pool entry
    public Measure getNext() {
        //refill the pool if it is empty
        if (turnPool.Count == 0) {
            refillPool();
        }
        //find max in pool
        int max = turnPool.Count;
        //get the choice
        int index = Random.Range(0, max);
        //get a random choice from the pool
        Enemy enemy = turnPool[index];
        //remove the enemy from the pool
        turnPool.RemoveAt(index);
        //get the segment
        Segment segment = enemy.getSegmentPool()[0]; //TODO CHANGE TO RANDOM CHOICE
        //create the measure
        Measure measure = new Measure(segment, enemy);
        return measure;
    }

    public void draw() {
        if (battleStatus) {
            //the bar
            GUI.DrawTexture(new Rect(0f, Screen.height - 90, Screen.width, 30), laneTexture, ScaleMode.StretchToFill);

            //draw measure thing

            //left accept
            //GUI.DrawTexture(new Rect(Screen.width - (numVisibleMeasures * widthPerBeat * 16) - 7 + 45, Screen.height - 90, 60, 30), leftAccept, ScaleMode.StretchToFill);
            //right accept
            //GUI.DrawTexture(new Rect(Screen.width - (numVisibleMeasures * widthPerBeat * 16) + 7 + 45, Screen.height - 90, 60, 30), rightAccept, ScaleMode.StretchToFill);
        }
    }
    
}
