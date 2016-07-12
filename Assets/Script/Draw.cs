using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class Draw : NetworkBehaviour 
{

	
	private EventSystem es;
	[SerializeField] private GameObject linePrefab,trailPrefab,drawingPrefab;


	private int i;
	public static Dictionary<int, Lines> linesInfo;
	public static List<float> fadeTime = new List<float>();
	public static List<GameObject> lines = new List<GameObject>();
	public static List<GameObject> drawing = new List<GameObject>();
	
    public static int trailCounter;
    public static bool makeTrail;
	public static bool isDrawing;
	public GameObject curDrawingParent;
	[SyncVarAttribute] private int drawingIndex;
	[SerializeField] private GameObject button2;


    // Use this for initialization
    void Start () 
	{
		es = GameObject.FindGameObjectWithTag("ES").GetComponent<EventSystem>();
		trailCounter = 0;
        makeTrail = false;
        linesInfo = new Dictionary<int, Lines>();
        fadeTime.Clear();
	}
	
	void FixedUpdate()
	{
		if(Input.GetMouseButtonDown(0)&&!isDrawing)
		{
			Instantiate(linePrefab);
			isDrawing = true;
		}
		
		if(trailCounter < linesInfo.Count)
		{
			if(!makeTrail)
			return;
			Instantiate(trailPrefab);
		}
		else if(makeTrail)
		{

			foreach (GameObject item in lines)
			{
				DestroyImmediate(item);
			}
			lines.Clear();
			linesInfo.Clear();
			makeTrail = false;
			trailCounter = 0;
		}
		

	}
	public void SendButton()
	{

		makeTrail = true;
		trailCounter = 0;
		foreach (GameObject obj in lines)
		{
			obj.transform.GetChild(0).gameObject.SetActive (false);
		}
		if(drawing.Count>1)
		button2.SetActive(true);


		if(lines.Count == 0)
		return;
		GameObject p = Instantiate(drawingPrefab);
		p.name = "Drawing_" + (drawingIndex+1).ToString();
		drawing.Add(p);
		p.GetComponent<Drawing>().index = drawingIndex;		
		curDrawingParent = p;
		drawingIndex ++;
		curDrawingParent.SetActive(false);
	
		
		
		
	}



	public void LoopButton()
	{
		drawing[0].SetActive(true);
	}


	public GameObject GetCurParent()
	{
		return curDrawingParent;
	}


	

}
