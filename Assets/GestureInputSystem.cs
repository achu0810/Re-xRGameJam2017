using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine.XR.WSA.Input;

public class GestureInputSystem : MonoBehaviour {

    [SerializeField]
    private GameObject ally;

	// Use this for initialization
	void Start () {
        InteractionManager.InteractionSourcePressed += SourcePressed;
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,8)){
            hit.transform.GetComponent<Renderer>().material.color = new Color(255,0,0);
        }
    }

    void SourcePressed(InteractionSourcePressedEventArgs state){
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        if(hit.transform.tag == "test")
        {
            hit.transform.GetComponent<Rigidbody>().useGravity = true;
        }
        //Instantiate(ally,hit.transform.position,Quaternion.identity);
    }

}
