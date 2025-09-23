using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPrerequisDisplay : MonoBehaviour
{
    public GameObject checkmark;
    public TMP_Text textnom;
    private LearningPathDisplay parent;
    private LevelClass level;
    private ObjectifsClass objectif;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetParameters(LearningPathDisplay parent,LevelClass level,ObjectifsClass objectif){
        this.parent=parent;
        this.objectif=objectif;
        this.level=level;
    }
    public void Display(bool allreadyUsed){
        if(allreadyUsed){
            checkmark.SetActive(true);
        }
        textnom.text=level.name;
    }
    public void Selected(){
        parent.SetLevelPrerequisSelected(objectif,level);
    }
    public void Cancel(){
        parent.SetLevelPrerequisCancel();
    }
}
