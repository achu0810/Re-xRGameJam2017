using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
    [SerializeField]
    Text text;
    [SerializeField]
    MeshRenderer show;

    [SerializeField]
    Texture selectTexture, pushedTexture;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void selected() {
        show.material.mainTexture = selectTexture;
    }

    public void pushed() {
        show.material.mainTexture = pushedTexture;
    }
}
