using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GestureInputSystem : MonoBehaviour {
    public Subject<RaycastHit> lineOfSightObj = new Subject<RaycastHit>();
    public IObservable<Pair<RaycastHit>> lineOfSightObjChange;
    public RaycastHit hit;

    // Use this for initialization
    void Start () {
        this.UpdateAsObservable()
            .Subscribe(_ => {
            Physics.Raycast(transform.position, transform.forward, out hit, 100f);
            lineOfSightObj.OnNext(hit);
            });
        lineOfSightObjChange = lineOfSightObj
            .Pairwise()
            .Where(x => x.Previous.transform != x.Current.transform);
    }
}
