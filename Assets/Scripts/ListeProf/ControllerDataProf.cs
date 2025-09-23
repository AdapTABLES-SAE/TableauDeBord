using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerDataProf : MonoBehaviour
{
    public TMP_InputField inputFieldId;
    public TMP_InputField inputFieldName;
    public GameObject SuccessCreate;
    public GameObject ErrorCreate;

    void Start()
    {
        SuccessCreate.SetActive(false);
        ErrorCreate.SetActive(false);
    }

    public void AddProf(){
        //StartCoroutine(APIManager.AddProf(inputFieldId.text, inputFieldName.text, Success));        
    }

    public void CancelAdd(){
        SceneManager.LoadScene("ConnexionScene");
    }

    public void Success(bool Succeded){
        if(Succeded)
            SuccessCreate.SetActive(true);
        else
            ErrorCreate.SetActive(true);

        StartCoroutine(Wait1Sec());
    }

    IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(1);

        SuccessCreate.SetActive(false);
        ErrorCreate.SetActive(false);
        inputFieldId.text = "";
        inputFieldName.text = "";
    }
}
