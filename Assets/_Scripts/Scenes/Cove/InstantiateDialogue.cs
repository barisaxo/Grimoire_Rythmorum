using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class InstantiateDialogue : Dialogue
{
    override public Dialogue Initiate()
    {
        Data.Manager.Io.MiscData.SetValue(new Data.FirstInstantiateDialogue(), true);
        var lines = new Line[intro.Length];
        lines[^1] = new Line(intro[^1], new NewCoveScene_State() { Fade = true });
        for (int i = lines.Length - 2; i > -1; i--) lines[i] = new Line(intro[i], lines[i + 1]);
        FirstLine = lines[0];
        return this;
    }

    readonly string[] intro = new string[]{
        "There are eight feuding regions:\nIonia, Doria, Phrygia, Lydia, MixoLydia, Aeolia, Locria, and Chromatica.",
        "In each region there is a lighthouse.\nThese have all long been abandoned.",
        "It is said that if all the lighthouses were lit,\nthe seas would finally be at peace.",
        "Your quest is simple:\nfind and activate all eight lighthouses.",
        "Your journey, however,\nwill be far more challenging."
    };

    /*
    "There are eight feuding regions: Ionia, Doria, Phrygia, Lydia, MixoLydia, Aeolia, Locria, and Chromatica.
     Each region has a lighthouse that has long since been abandoned. 
     It is rumored that if all the lighthouses are lit, the seas will finally be at peace. 
     Your quest is simple: find and activate all eight lighthouses. 
     Your journey, however, will be far more difficult."
    */

}
