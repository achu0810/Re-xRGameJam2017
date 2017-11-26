using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GestureInputSystem : MonoBehaviour {
    public IConnectableObservable<Pair<RaycastHit>> lineOfSightObj;
    public RaycastHit hit;

    // Use this for initialization
    void Start () {
        lineOfSightObj = Service.RaycastSystem(transform, 10f)
                                .Publish();
        lineOfSightObj.Subscribe(h => hit = h.Current).AddTo(gameObject);

        lineOfSightObj.Subscribe(h => {
            h.Current.transform.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        });
   }
}