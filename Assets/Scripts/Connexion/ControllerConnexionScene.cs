using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;


public class ControllerConnexionScene : MonoBehaviour
{
    public TMP_InputField inputText;
    public Image loginField;
    public Button okButton;

    private Color savedColor;

    private void Start()
    {
        okButton.interactable = false;
    }

    public void ClickButton(){
        string identifiantConnexion = inputText.text;
        okButton.interactable = false;

        if (identifiantConnexion=="ADMIN")
            SceneManager.LoadScene("ListeProfScene");
        else
            StartCoroutine(APIManager.CheckLoginProf(identifiantConnexion, ChangeScene ));

    }

    public void ChangeScene(string result) {
        //Debug.Log("" + prof.classes[0].nbStudents);
        string identifiantConnexion = inputText.text;
        inputText.text = "";
        if (result=="OK")
        {
            StartCoroutine(APIManager.GetProf(identifiantConnexion, SetProf));
        } else
        {
            savedColor = loginField.color;
            loginField.color = Color.red;
            
            okButton.interactable = false;
            StartCoroutine(Wait1Sec());
        }
    }

    public void SetProf(ProfClass prof)
    {
        ProfClass.loggedTeacher = prof;
        SceneManager.LoadScene("ListeClassesScene");
    }

    public void editing()
    {
        if (inputText.text.Length > 2)
        {
            okButton.interactable = true;
        } else
        {
            okButton.interactable = false;
        }
    }

    IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(1);

        loginField.color = savedColor;
        inputText.text = "";
    }
} 



