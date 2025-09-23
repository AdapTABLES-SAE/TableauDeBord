using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StudentFailureDisplay : MonoBehaviour
{
    private EleveClass student;
    public TMP_Text nom;
    public TMP_Text prenom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetStudent(EleveClass student)
    {
        this.student = student;
    }
    public void Display()
    {
        nom.text = student.nomEleve;
        prenom.text = student.prenomEleve;
    }
}
