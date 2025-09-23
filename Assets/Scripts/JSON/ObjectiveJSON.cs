using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveJSON
{
    public string objective;
    public string name;

    public List<Prerequisite> prerequisites = new List<Prerequisite>();

    public List<LevelJSON> levels = new List<LevelJSON>();

    public static List<ObjectifsClass> GetObjectives(List<ObjectiveJSON> objectifs){
        List<ObjectifsClass> objectives = new List<ObjectifsClass>();
        foreach(ObjectiveJSON objectif in objectifs){
            ObjectifsClass objective = new ObjectifsClass(objectif.objective,objectif.name);

            foreach(Prerequisite prerequisite in objectif.prerequisites){
                objective.Add(prerequisite.requiredObjective,prerequisite.requiredLevel,prerequisite.encountersPercent,prerequisite.successPercent);
            }

            if (objectif.levels.Count == 0)
            {
                objective.tableUse.Add("2");
            }
            else
                objective.SetTables(objectif.levels[0].setupParameters.buildingParameters.tables);
            objective.listeLevel = LevelJSON.GetLevels(objectif.levels);
            objectives.Add(objective);
        }
        return objectives;
    }
}

public class Prerequisite
{
    public string requiredLevel;
    public string requiredObjective;
    public float successPercent;
    public float encountersPercent;
}
