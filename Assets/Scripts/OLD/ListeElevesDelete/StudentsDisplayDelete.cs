using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentsDisplayDelete : MonoBehaviour
{
    [SerializeField]
    private TMP_Text lastNameText;
    [SerializeField]
    private TMP_Text firstNameText;
    [SerializeField]
    private TMP_Text progressText;
    [SerializeField]
    private Selectable toggle;

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

    public void ClickToggle(bool selected){
        if(selected)
            FunctionButtonListeStudentDelete.elevesSelected.Add(eleveRepresented);
        else 
            FunctionButtonListeStudentDelete.elevesSelected.Remove(eleveRepresented);
    }
}
