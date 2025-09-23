using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrerequisObject
{
    public string levelId;
    public float seenFloor;
    public float successfloor;
    public PrerequisObject(string levelid, float seenFloor, float Successfloor){
        this.levelId=levelid;
        this.seenFloor=seenFloor;
        this.successfloor=Successfloor;
    }
}
