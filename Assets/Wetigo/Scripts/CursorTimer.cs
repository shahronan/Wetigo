using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorTimer : MonoBehaviour {

    public float second;
    public int counter;
    public Text debugText;
    public Slider slider;

    private GameObject obj;
    private Button btn;
    private bool isCounting;

	// Use this for initialization
	void Start () {
        counter = 0;
        isCounting = false;
        btn = GetComponent<Button>();

        EventTriggerListener.Get(gameObject).onEnter += OnPointerEnter;
        EventTriggerListener.Get(gameObject).onExit += OnPointerExit;
    }

	// Update is called once per frame
	void Update () {
        if (isCounting)
            counter++;

        if (counter > 60 * second)
        {
            btn.onClick.Invoke();
            counter = 0;
        }
        debugText.text = counter.ToString();
        slider.value = (float)counter / (second * 60);
    }

    public void OnPointerEnter(GameObject go)
    {
        isCounting = true;
    }
    public void OnPointerExit(GameObject go)
    {
        isCounting = false;
        counter = 0;
    }
}
