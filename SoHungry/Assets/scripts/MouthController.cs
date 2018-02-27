using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthController : MonoBehaviour {

    private GameObject managerObject;

    public GameObject ManagerObject
    {
        get { return managerObject; }
        set
        {
           managerObject = value;
        }
    }    

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (managerObject != null)
        {            
            if (coll.gameObject.tag == "GoodTarget")
              managerObject.gameObject.SendMessage("GoodTarget",coll);

            if (coll.gameObject.tag == "BadTarget")
               managerObject.gameObject.SendMessage("BadTarget",coll);
        }         

    }

}
