using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    [SerializeField]
    public Button m_CursorLeftButton, m_CursorRightButton, m_TitleLeftButton, m_TitleRightButton, m_EnterLeftButtom, m_EnterRightButtom;

    [Space]
    [SerializeField]
    public GameObject cursorObject;
    [SerializeField]
    public Canvas titleCanvas, enterCanvas;
    [SerializeField]
    public Text debugText;

    [Space]

    [SerializeField]
    public Material[] materialList;
    public Texture[] titleList, enterList;

    private int cursorPointer;
    private int titlePointer;
    private int enterPointer;

    void Start()
    {
        cursorPointer = 0;
        titlePointer = 0;
        enterPointer = 0;
        

        //Calls the TaskOnClick/TaskWithParameters method when you click the Button

        // crb.onClick.AddListener(delegate { TaskWithParameters("Hello"); });

        debugText.text = Application.persistentDataPath;
    }


    public void clb()
    {
        cursorPointer--;
        
        if (0 > cursorPointer)
            cursorPointer = materialList.Length - 1;


        Renderer render = cursorObject.GetComponent<Renderer>();

        render.material = materialList[cursorPointer];
    }

    public void crb()
    {
        cursorPointer++;
        if (materialList.Length <= cursorPointer)
            cursorPointer = 0;

        Renderer render = cursorObject.GetComponent<Renderer>();

        render.material = materialList[cursorPointer];
    }

    public void tlb()
    {
        titlePointer--;

        if (0 > titlePointer)
            titlePointer = titleList.Length - 1;

        Image img = titleCanvas.GetComponent<Image>();

        img.material.SetTexture("_MainTex", titleList[titlePointer]);
    }

    public void trb()
    {
        titlePointer++;
        if (titleList.Length <= titlePointer)
            titlePointer = 0;

        Image img = titleCanvas.GetComponent<Image>();

        img.material.SetTexture("_MainTex", titleList[titlePointer]);
    }

    public void elb()
    {
        enterPointer--;
        if (0 > enterPointer)
            enterPointer = enterList.Length - 1;

        Image img = enterCanvas.GetComponent<Image>();

        img.material.SetTexture("_MainTex", enterList[enterPointer]);

    }

    public void erb()
    {
        enterPointer++;
        if (enterList.Length <= enterPointer)
            enterPointer = 0;

        Image img = enterCanvas.GetComponent<Image>();

        img.material.SetTexture("_MainTex", enterList[enterPointer]);
    }
}
