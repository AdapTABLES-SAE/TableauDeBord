using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerDataEtuModif : MonoBehaviour
{
    public TMP_InputField inputFieldNom;
    public TMP_InputField inputFieldPrenom;
    private string originalNom;
    private string originalPrenom;
    void Start(){
        inputFieldNom.text = EleveClass.studentChosen.nomEleve;
        inputFieldPrenom.text = EleveClass.studentChosen.prenomEleve;
        originalNom = EleveClass.studentChosen.nomEleve;
        originalPrenom = EleveClass.studentChosen.prenomEleve;
    }

    public void ModifStudent(){
        EleveClass.studentChosen.nomEleve = inputFieldNom.text;
        EleveClass.studentChosen.prenomEleve = inputFieldPrenom.text;
        StartCoroutine(APIManager.ModifStudent(EleveClass.studentChosen,Success));
        
    }

    public void Success(bool Succeded){
        if(!Succeded){
            EleveClass.studentChosen.nomEleve = originalNom;
            EleveClass.studentChosen.prenomEleve = originalPrenom;
        }
        SceneManager.LoadScene("ListeElevesSceneModif");
    }

    public void CancelModify(){
        SceneManager.LoadScene("ListeElevesSceneModif");
    }
}
