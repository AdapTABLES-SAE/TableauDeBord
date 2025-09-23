using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevelJSON
{
    public float globalEncounters;
    public float globalSuccess;
    public List<DataProgress> progresses;
}

public class DataProgress
{
    public string idTask;
    public float currentSuccess;
    public float currentEncounters;
    public string typeTask;
}

