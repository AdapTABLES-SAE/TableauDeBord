using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerDataClassesModif : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text title;

    void Start(){
        title.text += ClassesClass.classeChosen.name;
        inputField.text = ClassesClass.classeChosen.name;
    }
    public void ModifyClasses(){
        StartCoroutine(APIManager.ModifClasse(ClassesClass.classeChosen.id,inputField.text,Success));
    }

    public void Success(bool Succeded){
        if(Succeded)
            ClassesClass.classeChosen.name = inputField.text;
        SceneManager.LoadScene("ListeClassesSceneModif");
    }

    public void CancelModify(){
        SceneManager.LoadScene("ListeClassesSceneModif");
    }
}
