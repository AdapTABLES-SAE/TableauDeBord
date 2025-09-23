using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using XCharts.Runtime;
using UnityEngine.UI;

public class ViewTasksDisplay : MonoBehaviour
{
    TaskParameter task;
    LearningPathDisplay parent;
    public TMP_Text textTitre;
    public RingChart success;
    public RingChart seen;
    public List<Button> buttons;

    void Start()
    {
        if(!initListeStudent.isModModify)
        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }    
    }
    public void SetParameters(LearningPathDisplay parent,TaskParameter task){
        this.task=task;
        this.parent=parent;
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
                textTitre.text="Identification de résultats";
                break;
        }

        seen.GetSerie(0).data[0].data[0] = task.seen;
        success.GetSerie(0).data[0].data[0] = task.success;
    }

    public void ModifyTask(){
        parent.ModifyTask(task);
    }

    public void DeleteTask(){
        parent.DeleteTask(task);
    }
}
