using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine.XR.WSA.Input;

public class GestureInputSystem : MonoBehaviour {

   [SerializeField]
   private GameObject ally;

    public IConnectableObservable<Pair<RaycastHit>> lineOfSightObj;
    public RaycastHit hit;

    // Use this for initialization
    void Start () {
        InteractionManager.InteractionSourcePressed += SourcePressed;
        lineOfSightObj = Service.RaycastSystem(transform, 10f)
                                .Pairwise()
                                .Publish();
        lineOfSightObj.Subscribe(x => hit = x.Current).AddTo(gameObject);
   }

   // Update is called once per frame
   void Update () {
       RaycastHit hit;
       if (Physics.Raycast(transform.position, transform.forward, out hit)){
           hit.transform.GetComponent<Renderer>().material.color = new Color(255,0,0);
       }
   }

   void SourcePressed(InteractionSourcePressedEventArgs state){
       RaycastHit hit;
       if(Physics.Raycast(transform.position, transform.forward, out hit, 8)){
           Instantiate(ally,hit.transform.position,Quaternion.identity);
       }
   }

}