using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectifsClass
{
    public string id;
    public string name;
    public List<LevelClass> listeLevel = new List<LevelClass>();
    public List<String> tableUse = new List<String>();
    public SortedDictionary<string,PrerequisObject> prerequis= new SortedDictionary<string,PrerequisObject>();

    public ObjectifsClass(string id, string name)
    {
        this.id = id;
        this.name = name;
        tableUse.Add("1");
    }

    public void Add(string objectif, string level, float Seen, float Success){
        if (prerequis.ContainsKey(objectif)){
            prerequis[objectif].levelId=level;
            prerequis[objectif].successfloor = Success;
            prerequis[objectif].seenFloor = Seen;
        }
        else
            prerequis.Add(objectif,new PrerequisObject(level,Seen,Success));
    }

    public void SetTables(List<string> tables)
    {
        tableUse.Clear();
        tableUse = tables;
    }

    public void CheckAndAdjustTasksRepartition()
    {
        foreach (LevelClass level in listeLevel) 
        {
            level.CheckAndAdjustTasksRepartition();
        }
    }
}
