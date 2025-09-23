using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class EnseignantDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text idEnseignant;

    private GameObject popup;
    private Button buttonconfirm;
    private Button buttoncancel;
    private TMP_Text textDelete;
    private ProfClass representedEnseignant;

    public InitSceneEnseignants init;

    public void SetEnseignant(ProfClass enseignant) {
        representedEnseignant = enseignant;
        Display();
    }

    public void DeleteEnseignant()
    {
        Coroutine coroutine = StartCoroutine(APIManager.DeleteProf(representedEnseignant.idProf, DeleteSuccess));
    }

    public void DeleteSuccess(bool Succeded)
    {
        Debug.Log("delete prof successful " + Succeded);
        SceneManager.LoadScene("ListeProfScene");
    }

    public void SetPopupDelete(GameObject popup, Button Confirm, Button Cancel, TMP_Text textDelete)
    {
        this.popup = popup;
        buttonconfirm = Confirm;
        buttoncancel = Cancel;
        this.textDelete = textDelete;
    }

    public void RemoveDeletePopup()
    {
        popup.SetActive(false);
    }

    public void DeleteConfirmation()
    {
        popup.SetActive(true);
        buttonconfirm.onClick.RemoveAllListeners();
        buttoncancel.onClick.RemoveAllListeners();
        buttonconfirm.onClick.AddListener(DeleteEnseignant);
        buttoncancel.onClick.AddListener(delegate
        {
            RemoveDeletePopup();
        });
        textDelete.text = representedEnseignant.name;
    }

    private void Display() {
        nameText.text = representedEnseignant.name;
        idEnseignant.text = representedEnseignant.idProf;
    }

}
