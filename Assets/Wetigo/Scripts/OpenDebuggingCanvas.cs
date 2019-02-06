using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDebuggingCanvas : MonoBehaviour {
	public GameObject debuggingcanvas;

	public void TriggerDebuggingCanvas(GameObject debuggingcanvas)
	{
		if (debuggingcanvas.active)
		{
			debuggingcanvas.SetActive(false);
		}
		else
		{
			debuggingcanvas.SetActive(true);
		}
	}
}
