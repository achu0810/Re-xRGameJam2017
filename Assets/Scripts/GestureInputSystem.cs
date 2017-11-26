using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GestureInputSystem : MonoBehaviour {
    public IConnectableObservable<Pair<RaycastHit>> lineOfSightObj;
    public Subject<Pair<Transform>> lineOfSightObj2 = new Subject<Pair<Transform>>();
    public Transform hit;

    // Use this for initialization
    void Start () {
        this.UpdateAsObservable()
            .Select(_ => hit)
            .Pairwise()
            .Where(x => x.Previous != x.Current)
            .Subscribe(x => {
                lineOfSightObj2.OnNext(x);
            });
    }

    private void Update()
    {
        hit = Service.RaycastSystem2(transform, 100f);
    }
}