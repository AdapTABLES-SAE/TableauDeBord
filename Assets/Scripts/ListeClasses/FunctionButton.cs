using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionButton : MonoBehaviour
{
    private ClassesClass classAdded;
    public InitSceneClassRooms initScene;
    public GameObject panelCreation;

    private void Start()
    {
        panelCreation.SetActive(false);
    }

    public void ClickAddClasseButton(){
        panelCreation.SetActive(true);
        //classAdded = new ClassesClass();
        //string currentTime = DateTime.Now.ToString("MMddyyyyHHmmss");
        //classAdded.id = ProfClass.loggedTeacher.idProf + currentTime;
        //classAdded.name = "nouvelle classe à renommer";
        //classAdded.nbStudents = 0;
        //StartCoroutine(APIManager.AddClasse(ProfClass.loggedTeacher.idProf, classAdded, CheckSuccess));
    }

    public void ClickCancelAddClasseButton()
    {
        panelCreation.SetActive(false);
    }

        public void CheckSuccess(bool Succeded, ClassesClass classAdded)
    {
        if (Succeded)
        {
            ProfClass.loggedTeacher.classes.Add(classAdded);
            SceneManager.LoadScene("ListeClassesScene");

            //    ProfClass.loggedTeacher.classes.Add(classAdded);
        }
        //SceneManager.LoadScene("ListeClassesScene");
        
    }

    //public void ClickModifClasseButton(){
    //    SceneManager.LoadScene("ListeClassesSceneModif");
    //}    

    //public void ClickDeleteClasseButton(){
    //    SceneManager.LoadScene("ListeClassesSceneDelete");
    //}    

    public void ClickDisconnectButton(){
        SceneManager.LoadScene("ConnexionScene");
    }
}
