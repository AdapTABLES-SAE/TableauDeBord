using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerDataClasses : MonoBehaviour
{
    public TMP_InputField inputField;

    public InitSceneClassRooms init;

    public void AddClasses(){
        ClassesClass classAdded = new ClassesClass();
        string currentTime = DateTime.Now.ToString("MMddyyyyHHmmss");
        classAdded.id = ProfClass.loggedTeacher.idProf + currentTime;
        classAdded.name = inputField.text;
        classAdded.nbStudents = 0;
        StartCoroutine(APIManager.AddClasse(ProfClass.loggedTeacher.idProf,classAdded,CheckSuccess));
    }

    //public void CancelAdd(){
    //    SceneManager.LoadScene("ListeClassesScene");
    //}

    public void CheckSuccess(bool Succeded,ClassesClass classAdded)
    {
        if(Succeded){
            ProfClass.loggedTeacher.classes.Add(classAdded);
            SceneManager.LoadScene("ListeClassesScene");
            //init.Display();
        }
        //SceneManager.LoadScene("ListeClassesScene");
    }
}
