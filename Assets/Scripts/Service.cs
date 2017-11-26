using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Service {
    public static Subject<Pair<RaycastHit>> RaycastSystem(Transform transform, float length) {
        Subject<Pair<RaycastHit>> lineOfSightObj = new Subject<Pair<RaycastHit>>();
        RaycastHit hit = new RaycastHit();
        transform.UpdateAsObservable()
                 .Subscribe(_ => {
                     Physics.Raycast(transform.position, transform.forward, out hit, length);
                 });
        transform.UpdateAsObservable()
                 .Select(_ => hit)
                 .Pairwise()
                 .Subscribe(x => {
                     lineOfSightObj.OnNext(x);
                 });
        return lineOfSightObj;
    }
}
