using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class InitSceneClassRoomsDelete : MonoBehaviour
{
    public Transform contentScrollView;
    public GameObject prefabLineClassRoom;
    public TMP_Text teacherNameText;

    
    void Start()
    {
        Debug.Log("" + ProfClass.loggedTeacher.classes[0].nbStudents);
        teacherNameText.text = ProfClass.loggedTeacher.name;

        foreach (ClassesClass aClassRoom in ProfClass.loggedTeacher.classes) {
            Debug.Log("ajout d'une classe");
            GameObject line = Instantiate(prefabLineClassRoom, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            
            line.transform.SetParent(contentScrollView, false);

            ClassroomDisplayDelete display = line.GetComponent<ClassroomDisplayDelete>();
            display.SetClassRoom(aClassRoom);
        }
    }    
}
