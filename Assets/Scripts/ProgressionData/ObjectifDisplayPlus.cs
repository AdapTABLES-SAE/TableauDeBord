using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectifDisplayPlus : MonoBehaviour
{
    public LearningPathDisplay parent;

    public void SetParent(LearningPathDisplay parent){
        this.parent=parent;
    }

    public void Add()
    {
        parent.AddObjectif();
    }
}
