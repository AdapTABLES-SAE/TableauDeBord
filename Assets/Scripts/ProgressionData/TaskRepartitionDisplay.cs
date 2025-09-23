using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using XCharts.Runtime;
using UnityEngine.UI;

public class TaskRepartiionDisplay : MonoBehaviour
{
    public TaskParameter task;
    LearningPathDisplay parent;
    public TMP_Text textTitre;
    public TMP_InputField inputRepartition;
    public TaskRepartiionDisplay SetParameters(LearningPathDisplay parent,TaskParameter task){
        this.task=task;
        this.parent=parent;
        return this;
    }
    public void Display(){
        switch(task.typeTask){
            case(TypeTaskEnum.C1):
                textTitre.text="Complétion de 1 element";
                break;
            case(TypeTaskEnum.C2):
                textTitre.text="Complétion de 2 elements";
                break;
            case(TypeTaskEnum.REC):
                textTitre.text="Reconstitution d'un fait";
                break;
            case(TypeTaskEnum.ID):
                textTitre.text="Validité d'un fait";
                break;
            case(TypeTaskEnum.MEMB):
                textTitre.text="Identification des résultats";
                break;
        }
        inputRepartition.text = task.repartitionPercent.ToString();
    }

    public void UpdateInput(string value)
    {
        SignialParent();
    }

    public void SignialParent()
    {
        parent.ValueChangerepartition();
    }
}
