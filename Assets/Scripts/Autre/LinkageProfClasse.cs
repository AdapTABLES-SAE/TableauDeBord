using System.Collections;
using System.Collections.Generic;
using System;

public class LinkageProfClasse
{
    static List<LinkageProfClasse> lienProfClasse= new List<LinkageProfClasse>();

    public List<Guid> idClasses;
    public Guid idProf;

    public LinkageProfClasse(Guid idProf){
        this.idProf=idProf;
        this.idClasses=new List<Guid>();
        lienProfClasse.Add(this);
    }

    public Guid GetIdProf(){
        return idProf;
    }

    public List<Guid> GetListIdClasse(){
        return idClasses;
    }

    public void AddIdClasse(Guid idClasse){
        idClasses.Add(idClasse);
    }

    public static LinkageProfClasse GetLinkageProfClasseWithProfId(Guid idProf){
        LinkageProfClasse resultfinal=null;
        foreach(LinkageProfClasse linkageProfClasseAct in lienProfClasse)
        {
            if(linkageProfClasseAct.idProf==idProf)
                resultfinal=linkageProfClasseAct;
        }
        return resultfinal;
    }
}
