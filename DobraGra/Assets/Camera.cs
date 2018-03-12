using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform Gracz;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = this.transform.position - Gracz.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Gracz.position + offset;
	}
}
