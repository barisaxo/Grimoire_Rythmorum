using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using System;

public class FoundItem_Dialogue : Dialogue
{
    readonly State SubsequentState;
    readonly Sea.IInventoriable Rewards;

    public FoundItem_Dialogue(Sea.IInventoriable rewards, State subsequentState)
    {
        SubsequentState = subsequentState;
        Rewards = rewards;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= GetStartLine();

    private Line GetStartLine()
    {
        if (Rewards.Rewards is null) return new Line("You still need to implement this reward, Pino", SubsequentState);

        var lines = new Line[Rewards.Rewards.Length];

        if (Rewards.Rewards[^1].DataItem is Data.PatternsFound)
        {
            lines[^1] = new Line("You gained " + Rewards.Rewards[^1].Amount + " Pattern"
                     + (Rewards.Rewards[^1].Amount > 1 ? "s." : "."), SubsequentState);
        }
        else
        {
            lines[^1] = new Line((Rewards.Rewards[^1].Amount == 1 ? "A " : Rewards.Rewards[^1].Amount + " ")
                 + Rewards.Rewards[^1].DataItem.Name + (Rewards.Rewards[^1].Amount > 1 ? "s have" : " has") +
                 " been added to your inventory.", SubsequentState);
        }

        for (var i = lines.Length - 2; i > -1; i--)
        {
            if (Rewards.Rewards[i].DataItem is Data.PatternsFound)
            {
                lines[^1] = new Line("You gained " + Rewards.Rewards[i].Amount + " Pattern"
                         + (Rewards.Rewards[i].Amount > 1 ? "s." : "."), lines[i + 1]);
            }
            else
            {
                lines[i] = new Line((Rewards.Rewards[i].Amount == 1 ? "A " : Rewards.Rewards[i].Amount + " ")
                 + Rewards.Rewards[i].DataItem.Name + (Rewards.Rewards[i].Amount > 1 ? "s have" : " has") +
                 " been added to your inventory.", lines[i + 1]);
            }
        }

        return lines[0];
    }
    // new Line(
    //     "A " + SceneObject.Description.Name + " has been added to your inventory!", SubsequentState);




}
