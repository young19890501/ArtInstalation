using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Drawing : NetworkBehaviour
{
	private bool done = true;
	private bool drawingDone;
	public int index;
	private int i;



	// Use this for initialization
	void Start () 
	{
        index = Draw.drawingIndex;
        Draw.curDrawingParent = gameObject;
        Draw.drawing.Add(gameObject);
        Draw.drawingIndex ++;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(Transform child in transform)
		{
			drawingDone = done && child.GetComponent<Trail>().done;
		}

		if(drawingDone)
		{
            
			if(Draw.drawing.Count > 1 )
			{
				i = (index + 1) % Draw.drawing.Count;
               
				Draw.drawing[i].SetActive(true);
			}
			gameObject.SetActive(false);
			
		}
		
	}
}
