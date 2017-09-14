using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        transform.position = Vector3.zero;
        Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    //private void OnMouseDown()
    //{
    //    Destroy(gameObject);
    //}
}
