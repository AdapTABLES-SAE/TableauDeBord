using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectifPrerequisDisplay : MonoBehaviour
{
    public GameObject checkmark;
    public TMP_Text textnom;
    private LearningPathDisplay parent;
    private ObjectifsClass objectif;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetParameters(LearningPathDisplay parent,ObjectifsClass objectif){
        this.parent=parent;
        this.objectif=objectif;
    }
    public void Display(bool allreadyUsed){
        if(allreadyUsed){
            checkmark.SetActive(true);
        }
        textnom.text=objectif.name;
    }
    public void Selected(){
        parent.SetObjectifPrerequisSelected(objectif);
    }
    public void Cancel(){
        parent.SetObjectifPrerequisCancel();
    }
}
