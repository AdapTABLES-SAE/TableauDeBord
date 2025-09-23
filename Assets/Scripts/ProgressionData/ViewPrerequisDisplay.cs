using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewPrerequisDisplay : MonoBehaviour
{
    private ObjectifsClass objectif;
    private LevelClass level;
    private float seen;
    private float success;
    public TMP_Text textObjectif;
    public TMP_Text textLevel;
    public TMP_Text textVue;
    public TMP_Text textSuccess;
    public void SetParameters(ObjectifsClass objectif, LevelClass level, float seen, float success){
        this.objectif=objectif;
        this.level=level;
        this.seen=seen;
        this.success=success;
    }
    public void Display(){
        textObjectif.text=objectif.name;
        textLevel.text=level.name;
        textVue.text=seen+"/"+level.SeenWanted;
        textSuccess.text=success+"/"+level.SuccessWanted;
    }
}
