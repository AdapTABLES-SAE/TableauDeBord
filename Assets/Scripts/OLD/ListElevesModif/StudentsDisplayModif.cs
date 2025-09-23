using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StudentsDisplayModif : MonoBehaviour
{
    [SerializeField]
    private TMP_Text lastNameText;
    [SerializeField]
    private TMP_Text firstNameText;
    [SerializeField]
    private TMP_Text progressText;

    private EleveClass eleveRepresented;
    
    void Start()
    {
        
    }

    public void SetStudent(EleveClass eleve) {
        eleveRepresented = eleve;
        Display();
    }

    private void Display() {
        lastNameText.text = eleveRepresented.nomEleve;
        firstNameText.text = eleveRepresented.prenomEleve;
        progressText.text = "Non defini";
    }

    public void ClickOnEleve(){
        EleveClass.studentChosen = eleveRepresented;
        SceneManager.LoadScene("ModificationEtudiantScene");
    }
}
