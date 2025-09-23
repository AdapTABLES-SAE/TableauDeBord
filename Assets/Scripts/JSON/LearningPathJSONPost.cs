using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningPathJSONPost
{
    public string learningPathID;
    public string learnerID;

    public LearningPathJSONPost(string learnerID)
    {
        this.learnerID = learnerID;
    }

    public List<ObjectiveJSONPost> objectives = new List<ObjectiveJSONPost>();

    public void TranfertToJSON(LearningPathClass lp){
        learningPathID = lp.id;
        objectives=ObjectiveJSONPost.SetObjectives(lp.objectifs);
    }
}
