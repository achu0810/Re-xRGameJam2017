using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Service {
    public static IObservable<Pair<RaycastHit>> RaycastSystem(Transform transform, float length) {
        return Observable.Create<Pair<RaycastHit>>(observer => {
            RaycastHit hit = new RaycastHit();
            Observable.EveryUpdate()
                      .Select(_ => hit)
                      .Pairwise()
                      .Subscribe(h => {
                          Physics.Raycast(transform.position, transform.forward, out hit, length);
                          if (h.Previous.transform != hit.transform) {
                              observer.OnNext(h);
                          }
                      }).AddTo(transform.gameObject);
            return new CompositeDisposable();
        });
    }

    public static RaycastHit RaycastSystem2(Transform transform, float length) {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, length);
        return hit;
    }
}
