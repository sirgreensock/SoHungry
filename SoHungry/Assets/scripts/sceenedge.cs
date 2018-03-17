using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceenedge : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.transform.parent.parent.gameObject.tag == "FoodItem")
            coll.gameObject.transform.parent.parent.gameObject.SendMessage("ResetSelf");

    }
}
