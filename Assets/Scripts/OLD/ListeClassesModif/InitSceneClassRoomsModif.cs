using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class InitSceneClassRoomsModif : MonoBehaviour
{
    public Transform contentScrollView;
    public GameObject prefabLineClassRoom;
    public TMP_Text teacherNameText;

    
    void Start()
    {
        teacherNameText.text = ProfClass.loggedTeacher.name;

        foreach (ClassesClass aClassRoom in ProfClass.loggedTeacher.classes) {
            Debug.Log("ajout d'une classe");
            GameObject line = Instantiate(prefabLineClassRoom, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            
            line.transform.SetParent(contentScrollView, false);

            ClassroomDisplayModif display = line.GetComponent<ClassroomDisplayModif>();
            display.SetClassRoom(aClassRoom);
        }
    }    
}
