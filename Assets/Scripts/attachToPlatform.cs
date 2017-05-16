using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachToPlatform : MonoBehaviour {

    //public GameObject attachedObject;

	// Use this for initialization
	void Start () {

        print("attach script active");
    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale = transform.localScale;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "platform")
        {
            print("attach");
            //attachedObject = col.transform.parent.gameObject;
            transform.parent = col.transform;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "platform")
        {
            print("attach");
            transform.parent = null;
        }
    }
}
