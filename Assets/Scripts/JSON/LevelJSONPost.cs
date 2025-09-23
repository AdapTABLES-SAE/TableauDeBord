using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LevelJSONPost
{
    public string name;
    public string level;
    public SetupParametersPost setupParameters = new SetupParametersPost();
    
    public static List<LevelJSONPost> SetLevels(List<LevelClass> levels,List<string> tables){
        List<LevelJSONPost> levelsJSON = new List<LevelJSONPost>();
        foreach(LevelClass levelFromList in levels){
            LevelJSONPost levelJSON = new LevelJSONPost();
            levelJSON.name=levelFromList.name;
            levelJSON.level=levelFromList.id;
            levelJSON.setupParameters.achievementParameters.successCompletionCriteria = levelFromList.SuccessWanted;
            levelJSON.setupParameters.achievementParameters.encounterCompletionCriteria = levelFromList.SeenWanted;
            levelJSON.setupParameters.buildingParameters.tables=tables;
            if(levelFromList.posEgal==LeftEqualEnum.RIGHT_EQUAL)
                levelJSON.setupParameters.buildingParameters.resultLocation = "RIGHT";
            if(levelFromList.posEgal==LeftEqualEnum.LEFT_EQUAL)
                levelJSON.setupParameters.buildingParameters.resultLocation = "LEFT";
            if(levelFromList.posEgal==LeftEqualEnum.MIX)
                levelJSON.setupParameters.buildingParameters.resultLocation = "MIX";
            if(levelFromList.construTable==LeftFacteurEnum.FACTEUR_TABLE)
                levelJSON.setupParameters.buildingParameters.leftOperand = "OPERAND_TABLE";
            if(levelFromList.construTable==LeftFacteurEnum.TABLE_FACTEUR)
                levelJSON.setupParameters.buildingParameters.leftOperand = "TABLE_OPERAND";
            if(levelFromList.construTable==LeftFacteurEnum.MIX)
                levelJSON.setupParameters.buildingParameters.leftOperand = "MIX";
            levelJSON.setupParameters.buildingParameters.intervalMin = levelFromList.intervalMin;
            levelJSON.setupParameters.buildingParameters.intervalMax = levelFromList.intervalMax;
            levelJSON.setupParameters.tasksParameters = new List<TaskParameter>(levelFromList.tasks);
            levelsJSON.Add(levelJSON);
        }
        return levelsJSON;
    }
}

public class SetupParametersPost
{
    public AchievementParameters achievementParameters = new AchievementParameters();
    public BuildingParameters buildingParameters = new BuildingParameters();
    public List<TaskParameter> tasksParameters = new List<TaskParameter>();
}