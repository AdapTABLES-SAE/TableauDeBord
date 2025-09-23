using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class initProgressEleve : MonoBehaviour
{
    public static initProgressEleve initProgressEleveStatic;
    public FunctionTab functionTab;
    public GameObject TabGameData;
    public GameObject TabOwnedObject;
    public Button TabDataLP;
    public GameObject TitreModif;
    public GameObject TitreView;
    public TMP_Text textEleve;
    public Button buttonNext;
    public Button buttonPrevious;
    // Start is called before the first frame update
    void Start()
    {
        functionInitialize();
        initProgressEleveStatic = this;
        if (initListeStudent.isModModify)
        {
            TabGameData.SetActive(false);
            TabOwnedObject.SetActive(false);
            TabDataLP.interactable = false;
            TitreModif.SetActive(true);
        }
        else
        {
            TitreView.SetActive(true);
        }
    }

    public void functionInitialize(){
        textEleve.text = EleveClass.studentChosen.nomEleve + " " + EleveClass.studentChosen.prenomEleve;
        int indexOfEleve = EleveClass.studentsOfClassroom.FindIndex(r => r==EleveClass.studentChosen);
        if(indexOfEleve+1==EleveClass.studentsOfClassroom.Count){
            buttonNext.interactable = false;
        }
        else buttonNext.interactable = true;
        indexOfEleve = EleveClass.studentsOfClassroom.FindIndex(r => r==EleveClass.studentChosen);
        if(indexOfEleve==0){
            buttonPrevious.interactable = false;
        }
        else buttonPrevious.interactable = true;
        functionTab.ReloadCurrentTab();
    }
}
