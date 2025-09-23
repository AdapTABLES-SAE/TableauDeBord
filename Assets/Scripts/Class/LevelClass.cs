using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class LevelClass
{
    public string id;
    public string name;
    public float SeenNumber;
    public float SeenWanted;
    public float SuccessNumber;
    public float SuccessWanted;
    public LeftFacteurEnum construTable;
    public LeftEqualEnum posEgal;
    public int intervalMin;
    public int intervalMax;
    public List<TaskParameter> tasks = new List<TaskParameter>();

    public LevelClass(string id, string name)
    {
        this.id = id;
        this.name = name;
        SeenNumber = 0;
        SeenWanted = 100;
        SuccessNumber = 0;
        SuccessWanted = 100;
        construTable=LeftFacteurEnum.TABLE_FACTEUR;
        posEgal=LeftEqualEnum.RIGHT_EQUAL;
        intervalMin=1;
        intervalMax=10;
    }
    public void AddT1(TaskParameterJSON taskParameter){
        tasks.Add(new T1Parameters(taskParameter));
    }
    public void AddT1(){
        tasks.Add(new T1Parameters());
        Equilibrize();
    }
    public void AddT2(TaskParameterJSON taskParameter){
        tasks.Add(new T2Parameters(taskParameter));
    }
    public void AddT2(){
        tasks.Add(new T2Parameters());
        Equilibrize();
    }
    public void AddT3(TaskParameterJSON taskParameter){
        tasks.Add(new T3Parameters(taskParameter));
    }
    public void AddT3(){
        tasks.Add(new T3Parameters());
        Equilibrize();
    }
    public void AddT4(TaskParameterJSON taskParameter){
        tasks.Add(new T4Parameters(taskParameter));
    }
    public void AddT4(){
        tasks.Add(new T4Parameters());
        Equilibrize();
    }
    public void AddT5(TaskParameterJSON taskParameter){
        tasks.Add(new T5Parameters(taskParameter));
    }
    public void AddT5(){
        tasks.Add(new T5Parameters());
        Equilibrize();
    }

    public void CheckAndAdjustTasksRepartition()
    {
        
        int globalTaskPrct = 0;
        foreach (TaskParameter task in tasks)
        {
            globalTaskPrct = task.repartitionPercent;
        }
        if (globalTaskPrct < 100) {
            int diff = 100 - globalTaskPrct;
            tasks[0].repartitionPercent += diff; 
        } else if (globalTaskPrct > 100) {
            int diff = globalTaskPrct - 100;
            tasks[0].repartitionPercent -= diff; 
        }
    }

    private void Equilibrize()
    {
        foreach(TaskParameter taskParameter in tasks)
        {
            taskParameter.repartitionPercent = 100 / tasks.Count;
        }
    }

}
public enum LeftFacteurEnum
{
    FACTEUR_TABLE, TABLE_FACTEUR, MIX
}
public enum LeftEqualEnum
{
    RIGHT_EQUAL, LEFT_EQUAL, MIX
}
public class TaskParameter
{
    [JsonProperty(PropertyName = "taskType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public TypeTaskEnum typeTask;
    public int timeMaxSecond = 20;
    public int successiveSuccessesToReach = 1;
    public int repartitionPercent=100;
    public float success;
    public float seen;
}

public enum TypeTaskEnum
{
    C1, C2, REC, ID, MEMB
}

public class T1Parameters: TaskParameter
{
    public TargetT1Enum[] targets;
    [JsonConverter(typeof(StringEnumConverter))]
    public AnswerModalityEnum answerModality = AnswerModalityEnum.CHOICE;
    public int nbIncorrectChoices = 2;
    public int nbCorrectChoices = 1;

    public T1Parameters():base()
    {
        typeTask = TypeTaskEnum.C1;
        targets = new TargetT1Enum[1];
        targets[0] = TargetT1Enum.RESULT;
    }

    public T1Parameters(TaskParameterJSON param)
    {
        typeTask = TypeTaskEnum.C1;
        timeMaxSecond = param.maxTime;
        successiveSuccessesToReach = param.successiveSuccessesToReach;
        repartitionPercent = param.repartitionPercent;
        Debug.Log(repartitionPercent);
        answerModality = param.answerModality;
        nbIncorrectChoices = param.nbIncorrectChoices;
        targets = new TargetT1Enum[param.targets.Length];
        int index = 0;
        foreach (string value in param.targets)
        {
            //Debug.Log("value=" + value);
            targets[index++] = (TargetT1Enum) Enum.Parse(typeof(TargetT1Enum), value); 
        }
    }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum TargetT1Enum
{
    RESULT, TABLE, OPERAND
}

public enum AnswerModalityEnum
{
    CHOICE, INPUT
}

public class T2Parameters : TaskParameter
{
    public TargetT2Enum[] targets;
    public int nbIncorrectChoices = 3;
    public int nbCorrectChoices = 2;

    public T2Parameters() : base()
    {
        typeTask = TypeTaskEnum.C2;
        targets = new TargetT2Enum[1];
        targets[0] = TargetT2Enum.OPERAND_RESULT;
    }

    public T2Parameters(TaskParameterJSON param)
    {
        typeTask = TypeTaskEnum.C2;
        timeMaxSecond = param.maxTime;
        successiveSuccessesToReach = param.successiveSuccessesToReach;
        repartitionPercent = param.repartitionPercent;
        nbIncorrectChoices = param.nbIncorrectChoices;
        targets = new TargetT2Enum[param.targets.Length];
        int index = 0;
        foreach (string value in param.targets)
        {
            targets[index++] = (TargetT2Enum) Enum.Parse(typeof(TargetT2Enum), value);
        }
    }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum TargetT2Enum
{
    OPERAND_TABLE, OPERAND_RESULT, TABLE_RESULT
}

public class T3Parameters : TaskParameter
{
    public int nbIncorrectChoices = 3;
    public int nbCorrectChoices = 3;

    public T3Parameters() : base()
    {
        typeTask = TypeTaskEnum.REC;
    }

    public T3Parameters(TaskParameterJSON param)
    {
        typeTask = TypeTaskEnum.REC;
        timeMaxSecond = param.maxTime;
        successiveSuccessesToReach = param.successiveSuccessesToReach;
        repartitionPercent = param.repartitionPercent;
        nbIncorrectChoices = param.nbIncorrectChoices;
        nbCorrectChoices = param.nbCorrectChoices;
    }
}

public class T4Parameters : TaskParameter
{
    public int nbFacts = 1;
    [JsonConverter(typeof(StringEnumConverter))]
    public SourceVariationEnum sourceVariation = SourceVariationEnum.RESULT;

    public T4Parameters() : base()
    {
        typeTask = TypeTaskEnum.ID;
    }

    public T4Parameters(TaskParameterJSON param)
    {
        typeTask = TypeTaskEnum.ID;
        timeMaxSecond = param.maxTime;
        successiveSuccessesToReach = param.successiveSuccessesToReach;
        repartitionPercent = param.repartitionPercent;
        nbFacts = param.nbFacts;
        sourceVariation = param.sourceVariation;
    }
}

public enum SourceVariationEnum
{
    RESULT, OPERAND
}

public class T5Parameters : TaskParameter
{
    public int nbCorrectChoices = 2;
    public int nbIncorrectChoices = 2;
    [JsonConverter(typeof(StringEnumConverter))]
    public TargetT3Enum target = TargetT3Enum.CORRECT;

    public T5Parameters() : base()
    {
        typeTask = TypeTaskEnum.MEMB;
    }

    public T5Parameters(TaskParameterJSON param)
    {
        typeTask = TypeTaskEnum.MEMB;
        timeMaxSecond = param.maxTime;
        successiveSuccessesToReach = param.successiveSuccessesToReach;
        repartitionPercent = param.repartitionPercent;
        nbIncorrectChoices = param.nbIncorrectChoices;
        nbCorrectChoices = param.nbCorrectChoices;
        target = param.target;
    }
}

public enum TargetT3Enum
{
    CORRECT, INCORRECT
}