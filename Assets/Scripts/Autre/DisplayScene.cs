using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayScene : MonoBehaviour
{    
    
    public string[] nomScene;
    public void ChangePage(int numScene)
    {
        SceneManager.LoadScene(nomScene[numScene], LoadSceneMode.Single);
    }
}
