using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FunctionButtonListeStudent : MonoBehaviour
{
    public GameObject panelCreationStudent;

    void Start()
    {
        panelCreationStudent.SetActive(false);
    }

    public void Cancel()
    {
        panelCreationStudent.SetActive(false);
    }

    public void AddStudent()
    {
        panelCreationStudent.SetActive(true);
    }

    //public void ClickButtonAddStudent(){
    //    SceneManager.LoadScene("CreationEtudiantScene");
    //}

    public void ClickButtonReturn(){
        StartCoroutine(APIManager.GetProf(ProfClass.loggedTeacher.idProf, SetProf));
    }

    public void SetProf(ProfClass prof)
    {
        ProfClass.loggedTeacher = prof;
        SceneManager.LoadScene("ListeClassesScene");
    }

    //public void ClickButtonModifStudent(){
    //    SceneManager.LoadScene("ListeElevesSceneModif");
    //}
    //public void ClickButtonDeleteStudent(){
    //    SceneManager.LoadScene("ListeElevesSceneDelete");
    //}
}
