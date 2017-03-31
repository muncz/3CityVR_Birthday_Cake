using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowableCandle : MonoBehaviour {

    InspectBlow _blow;
    Rigidbody _rigidBody; 
	// Use this for initialization
	void Start () {
        _blow = FindObjectOfType<InspectBlow>();
        _rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = (_blow.transform.position - this.transform.position).normalized;
        //Debug.Log(direction);
        _rigidBody.AddForce(-direction * 100 * _blow.BlowForce);

        
	}
}
