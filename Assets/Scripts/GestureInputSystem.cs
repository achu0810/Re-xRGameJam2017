using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GestureInputSystem : MonoBehaviour {
    public IConnectableObservable<Pair<RaycastHit>> lineOfSightObj;
    public Subject<Pair<RaycastHit>> lineOfSightObj2 = new Subject<Pair<RaycastHit>>();
    public RaycastHit hit;

    // Use this for initialization
    void Start () {
        this.UpdateAsObservable()
            .Select(_ => hit)
            .Pairwise()
            .Where(x => x.Previous.transform != x.Current.transform)
            .Subscribe(x => {
                lineOfSightObj2.OnNext(x);
            });
    }

    private void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out hit, 100f);
    }
}