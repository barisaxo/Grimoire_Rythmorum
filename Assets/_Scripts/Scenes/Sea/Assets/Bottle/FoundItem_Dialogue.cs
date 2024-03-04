using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class FoundItem_Dialogue : Dialogue
{
    readonly State SubsequentState;
    readonly Sea.ISceneObject SceneObject;

    public FoundItem_Dialogue(Sea.ISceneObject sceneObject, State subsequentState)
    {
        SubsequentState = subsequentState;
        SceneObject = sceneObject;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(
        "A " + SceneObject.Description.Name + " has been added to your inventory!", SubsequentState);

}
