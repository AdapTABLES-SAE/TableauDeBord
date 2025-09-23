using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningPathClass
{
    public string id;
    public List<ObjectifsClass> objectifs= new List<ObjectifsClass>();

    public static LearningPathClass Copy(EleveClass student, LearningPathClass toCopy)
    {
        LearningPathClass copy = new LearningPathClass();

        // FETCH current student's LP ID
        
        copy.id = "PATH_MATH"+student.idStudent;
        int nbObjectif = 1;
        foreach(ObjectifsClass objectif in toCopy.objectifs)
        {
            ObjectifsClass objectifCurrent = new ObjectifsClass("O" + nbObjectif + copy.id, objectif.name);
            objectifCurrent.SetTables(new List<string>(objectif.tableUse));
            int nbLevels = 1;
            foreach (LevelClass level in objectif.listeLevel)
            {
                LevelClass levelCurrent = new LevelClass("L" + nbLevels + copy.id, level.name);
                levelCurrent.tasks = new List<TaskParameter>(level.tasks);
                levelCurrent.SuccessWanted = level.SuccessWanted;
                levelCurrent.SeenWanted = level.SeenWanted;
                levelCurrent.intervalMax = level.intervalMax;
                levelCurrent.intervalMin = level.intervalMin;
                levelCurrent.construTable = level.construTable;
                levelCurrent.posEgal = level.posEgal;
                objectifCurrent.listeLevel.Add(levelCurrent);
            }
            copy.objectifs.Add(objectifCurrent);
        }
        int indexObjectifGlobal = 0;
        foreach (ObjectifsClass otherObjectif in toCopy.objectifs)
        {
            foreach (string key in otherObjectif.prerequis.Keys)
            {
                int indexObjectif = 0;
                while (toCopy.objectifs[indexObjectif].id != key)
                    indexObjectif++;
                int indexLevel = 0;
                while (toCopy.objectifs[indexObjectif].listeLevel[indexLevel].id
                    != otherObjectif.prerequis[key].levelId)
                    indexLevel++;
                copy.objectifs[indexObjectifGlobal].Add(copy.objectifs[indexObjectif].id, copy.objectifs[indexObjectif].listeLevel[indexLevel].id, otherObjectif.prerequis[key].seenFloor, otherObjectif.prerequis[key].successfloor);
            }
            indexObjectifGlobal++;
        }
        return copy;
    }

    public void CheckAndAdjustTasksRepartition()
    {
        foreach (ObjectifsClass obj in objectifs) 
        {
            obj.CheckAndAdjustTasksRepartition();
        }
    }
}
