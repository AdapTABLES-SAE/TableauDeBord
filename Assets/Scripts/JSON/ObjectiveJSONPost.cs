using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveJSONPost
{
    public string objective;
    public string name;
    public List<Prerequisite> prerequisites = new List<Prerequisite>();
    public List<LevelJSONPost> levels = new List<LevelJSONPost>(); 

    public static List<ObjectiveJSONPost> SetObjectives(List<ObjectifsClass> objectifs){
        List<ObjectiveJSONPost> objectives = new List<ObjectiveJSONPost>(); 
        foreach(ObjectifsClass objectif in objectifs){
            ObjectiveJSONPost objectifCurrent = new ObjectiveJSONPost();
            objectifCurrent.objective=objectif.id;
            objectifCurrent.name=objectif.name;
            foreach(KeyValuePair<string,PrerequisObject> keyValue in objectif.prerequis){
                Prerequisite prerequisite = new  Prerequisite();
                prerequisite.requiredObjective = keyValue.Key;
                prerequisite.requiredLevel = keyValue.Value.levelId;
                prerequisite.successPercent = keyValue.Value.successfloor;
                prerequisite.encountersPercent = keyValue.Value.seenFloor;
                objectifCurrent.prerequisites.Add(prerequisite);
            }
            objectifCurrent.levels = LevelJSONPost.SetLevels(objectif.listeLevel,objectif.tableUse);
            objectives.Add(objectifCurrent);
        }
        return objectives;
    }
}
