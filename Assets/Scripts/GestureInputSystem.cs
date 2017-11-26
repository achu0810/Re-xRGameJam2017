using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GestureInputSystem : MonoBehaviour {
    public IObservable<Pair<RaycastHit>> lineOfSightObj2;
    public RaycastHit hit;

    // Use this for initialization
    void Start () {
        lineOfSightObj2 = Service.RaycastSystem(transform, 10f).Where(x => x.Previous.transform != x.Current.transform);
    }
}
