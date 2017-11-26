﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class SummonButton : MonoBehaviour {
    [SerializeField]
    GestureInputSystem input;

    public Text text;
    public MeshRenderer show;

    public GameObject summonCharacter;

    [SerializeField]
    Texture normalTexture, selectTexture, pushedTexture;
    Button button;
    [SerializeField]
    Texture disabledTexture;
    [SerializeField] private Transform m_point;

    // Use this for initialization
    void Start() {
        this.UpdateAsObservable()
            .SkipWhile(_ => input.lineOfSightObj2 == null)
            .First()
            .Subscribe(_ => {
                input.lineOfSightObj2
                     .Where(x => x.Current.transform == show.transform)
                     .Subscribe(x => selected());
                input.lineOfSightObj2
                     .Where(x => x.Previous.transform == show.transform)
                     .Subscribe(x => normal());
            });
        InteractionManager.InteractionSourcePressed += SourcePressed;
    }

    void SourcePressed(InteractionSourcePressedEventArgs state) {
        if (input.hit.transform == show.transform) {
            pushed();
        }
    }

    void normal() {
        show.material.mainTexture = normalTexture;
    }

    void disable() {
        show.material.mainTexture = disabledTexture;
    }

    void selected() {
        show.material.mainTexture = selectTexture;
    }

    void pushed() {
        show.material.mainTexture = pushedTexture;
        var summoned = Instantiate(summonCharacter, m_point.transform.position, m_point.transform.rotation);
    }

#if UNITY_EDITOR
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            input.lineOfSightObj2
                 .Subscribe(_ => {
                     pushed();
                 });
        }
    }
	#endif
}
