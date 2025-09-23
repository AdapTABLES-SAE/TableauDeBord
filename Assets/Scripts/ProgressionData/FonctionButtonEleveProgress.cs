using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.SceneManagement;
using TMPro;

public class FonctionButtonEleveProgress : MonoBehaviour
{
    public void ClickReturnButton(){
        SceneManager.LoadScene("ListeElevesScene");
    }

    public void ClickNextButton(Button button){
        int indexOfEleve = EleveClass.studentsOfClassroom.FindIndex(r => r==EleveClass.studentChosen);
        EleveClass.studentChosen = EleveClass.studentsOfClassroom[indexOfEleve+1];
        initProgressEleve.initProgressEleveStatic.functionInitialize();
    }

    public void ClickPreviousButton(Button button){
        int indexOfEleve = EleveClass.studentsOfClassroom.FindIndex(r => r==EleveClass.studentChosen);
        EleveClass.studentChosen = EleveClass.studentsOfClassroom[indexOfEleve-1];
        initProgressEleve.initProgressEleveStatic.functionInitialize();
    }
}
