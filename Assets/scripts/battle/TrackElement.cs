using UnityEngine;
using System.Collections;
/**
TrackElements are the indiviual elements to be drawn by the battle bar
**/
public class TrackElement {
    //the lane they should be drawn on
    int lane;
    //width of the object
    float width;
    //the texture to display
    Texture texture;
    //the x location of the element
    float x;

    public TrackElement(int lane, float x, float width, Texture texture) {
        this.lane = lane;
        this.x = x;
        this.width = width;
        this.texture = texture;
    }

    public void addX(float delta) {
        x += delta;
    }

    public float getX() {
        return x;
    }

    //must be called from inside OnGui()
    public void draw() {
        GUI.DrawTexture(new Rect(x, Screen.height - 90, width, 30), texture, ScaleMode.StretchToFill);
    }
}
