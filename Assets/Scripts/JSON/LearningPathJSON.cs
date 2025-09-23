using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningPathJSON
{
    public string learningPathID;
    public List<ObjectiveJSON> objectives = new List<ObjectiveJSON>();

    public LearningPathClass JSONToClass(){
        LearningPathClass lp = new LearningPathClass();
        lp.id=learningPathID;
        lp.objectifs=ObjectiveJSON.GetObjectives(objectives);
        return lp;
    }
}
