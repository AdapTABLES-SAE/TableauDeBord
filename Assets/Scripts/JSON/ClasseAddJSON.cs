using System.Collections;
using System.Collections.Generic;
using System;

public class ClasseAddJSON
{
    public string idProf;
    public ClasseJSON classe;

    public ClasseAddJSON(string idProf,ClassesClass classe)
    {
        this.idProf=idProf;
        this.classe=new ClasseJSON(classe.id,classe.name);
    }
}
