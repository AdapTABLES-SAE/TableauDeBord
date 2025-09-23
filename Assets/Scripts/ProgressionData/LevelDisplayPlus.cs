using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplayPlus : MonoBehaviour
{
    public ObjectifDisplay parent;
    public void SetParent(ObjectifDisplay parent){
        this.parent=parent;
    }

    public void Add(){
        parent.PlusLevel();
    }
}
