using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMove : MonoBehaviour {

    private bool dirRight = true;
    public float speed = 2f;
    //public GameObject parent;

    // Use this for initialization
    void Start () {
        //gameObject.transform.parent = parent.transform;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (dirRight)
        {
            //parent.transform.Translate(Vector3.right * (speed * 3) * Time.deltaTime);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            //parent.transform.Translate(Vector3.left * (speed * 3) * Time.deltaTime);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (transform.position.x >= 7.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= 4)
        {
            dirRight = true;
        }
    }
}
