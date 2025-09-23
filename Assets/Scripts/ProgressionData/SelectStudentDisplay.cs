using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectStudentDisplay : MonoBehaviour
{
    private EleveClass student;
    private Action<EleveClass> add;
    private Action<EleveClass> remove;
    public TMP_Text nom;
    public TMP_Text prenom;
    public Toggle select;
    void Start()
    {
        select.onValueChanged.AddListener(delegate
            {
                if (select.isOn == true) 
                    add(student); 
                else
                    remove(student);
            }
        );
    }

    public void SetStudent(EleveClass student)
    {
        this.student = student;
    }
    public void SetFunction(Action<EleveClass> Add,Action<EleveClass> Remove) 
    {
        this.add = Add;
        this.remove = Remove;
    }
    public void Display()
    {
        nom.text = student.nomEleve;
        prenom.text = student.prenomEleve;
    }

}
