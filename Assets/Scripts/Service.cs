using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public static class Service {
    public static IObservable<RaycastHit> RaycastSystem(Transform transform, float length) {
        return Observable.Create<RaycastHit>(observer => {
            RaycastHit hit = new RaycastHit();
            Observable.EveryUpdate()
                .Select(_ => hit)
                .Pairwise()
                .Subscribe(h => {
                    Physics.Raycast(transform.position, transform.forward, out hit, length);
                    if (h.Previous.transform != hit.transform) {
                        observer.OnNext(hit);
                    }
                }).AddTo(transform.gameObject);
            return new CompositeDisposable();
        });
    }
}
