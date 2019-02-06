using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setFill : MonoBehaviour {
    
    public GameObject btn;

    private Slider slider;
    private int val;

    // Use this for initialization
    void Start () {
        slider = GetComponent<Slider>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        val = btn.GetComponent<CursorTimer>().counter;
        slider.value = val / (5 * 60);
    }
}
