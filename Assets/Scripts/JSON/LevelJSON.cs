using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class LevelJSON
{
    public string name;
    public string level;
    public SetupParameters setupParameters = new SetupParameters();

    public static List<LevelClass> GetLevels(List<LevelJSON> levels){
        List<LevelClass> levelsReturn= new List<LevelClass>();
        foreach(LevelJSON level in levels)
        {
            LevelClass levelReturn = new LevelClass(level.level,level.name);
            levelReturn.intervalMin = level.setupParameters.buildingParameters.intervalMin;
            //Debug.Log(level.setupParameters.buildingParameters.intervalMax);
            levelReturn.intervalMax = level.setupParameters.buildingParameters.intervalMax;
            if(level.setupParameters.buildingParameters.resultLocation=="RIGHT")
                levelReturn.posEgal= LeftEqualEnum.RIGHT_EQUAL;
            if(level.setupParameters.buildingParameters.resultLocation=="LEFT")
                levelReturn.posEgal= LeftEqualEnum.LEFT_EQUAL;
            if(level.setupParameters.buildingParameters.resultLocation=="Mix")
                levelReturn.posEgal= LeftEqualEnum.MIX;
            if(level.setupParameters.buildingParameters.leftOperand=="OPERAND_TABLE")
                levelReturn.construTable = LeftFacteurEnum.FACTEUR_TABLE;
            if(level.setupParameters.buildingParameters.leftOperand=="TABLE_OPERAND")
                levelReturn.construTable = LeftFacteurEnum.TABLE_FACTEUR;
            if(level.setupParameters.buildingParameters.leftOperand=="MIX")
                levelReturn.construTable = LeftFacteurEnum.MIX;
            
            levelReturn.SuccessWanted = (int) level.setupParameters.achievementParameters.successCompletionCriteria;
            levelReturn.SeenWanted = (int) level.setupParameters.achievementParameters.encounterCompletionCriteria;
            foreach(TaskParameterJSON taskParam in level.setupParameters.tasksParameters){
                if(taskParam.taskType==TypeTaskEnum.C1)
                {
                    levelReturn.AddT1(taskParam);
                }
                if(taskParam.taskType==TypeTaskEnum.C2)
                {
                    levelReturn.AddT2(taskParam);
                }
                if(taskParam.taskType==TypeTaskEnum.REC)
                {
                    levelReturn.AddT3(taskParam);
                }
                if(taskParam.taskType==TypeTaskEnum.ID)
                {
                    levelReturn.AddT4(taskParam);
                }
                if(taskParam.taskType==TypeTaskEnum.MEMB)
                {
                    levelReturn.AddT5(taskParam);
                }
            }
            levelsReturn.Add(levelReturn);
        }
        return levelsReturn;
    }
}

public class SetupParameters
{
    public AchievementParameters achievementParameters = new AchievementParameters();
    public BuildingParameters buildingParameters = new BuildingParameters();
    public List<TaskParameterJSON> tasksParameters = new List<TaskParameterJSON>();
}

public class AchievementParameters
{
    public float successCompletionCriteria;
    public float encounterCompletionCriteria;
}

public class BuildingParameters
{
    public List<string> tables = new List<string>();
    public string resultLocation;
    public string leftOperand;
    public int intervalMin;
    public int intervalMax;
}

public class TaskParameterJSON
{
    [JsonConverter(typeof(StringEnumConverter))]
    public TypeTaskEnum taskType;
    public int maxTime = 20;
    public int successiveSuccessesToReach = 1;
    public int repartitionPercent = 0;
    public string[] targets;
    [JsonConverter(typeof(StringEnumConverter))]
    public AnswerModalityEnum answerModality = AnswerModalityEnum.CHOICE;
    public int nbIncorrectChoices = 2;
    public int nbCorrectChoices = 1;
    public int nbFacts = 1;
    [JsonConverter(typeof(StringEnumConverter))]
    public SourceVariationEnum sourceVariation = SourceVariationEnum.RESULT;
    [JsonConverter(typeof(StringEnumConverter))]
    public TargetT3Enum target = TargetT3Enum.CORRECT;
}