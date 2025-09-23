using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class initListeStudentModif : MonoBehaviour
{
    public Transform contentScrollView;
    public GameObject prefabLineEleve;
    public TMP_Text textClass;

    
    void Start()
    {
        textClass.text = ClassesClass.classeChosen.name;

        foreach (EleveClass aEleve in EleveClass.studentsOfClassroom) {
            Debug.Log("ajout d'une classe");
            GameObject line = Instantiate(prefabLineEleve, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            
            line.transform.SetParent(contentScrollView, false);

            StudentsDisplayModif display = line.GetComponent<StudentsDisplayModif>();
            display.SetStudent(aEleve);
        }
    }
}
