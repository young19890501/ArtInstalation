using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class Draw : NetworkBehaviour 
{

	
     private EventSystem es;
    [SerializeField] private GameObject linePrefab,trailPrefab,drawingPrefab,cubeFab;


    private int i;
	public static Dictionary<int, Lines> linesInfo;
    public static List<float> fadeTime = new List<float>();
    public static List<GameObject> lines = new List<GameObject>();
    public static List<GameObject> drawing = new List<GameObject>();
	
    public static int trailCounter;
    public static bool makeTrail;
    public static bool isDrawing;
    public static GameObject curDrawingParent;
    public static int drawingIndex;



    // Use this for initialization
    void Start () 
	{
		es = GameObject.FindGameObjectWithTag("ES").GetComponent<EventSystem>();
		trailCounter = 0;
        makeTrail = false;
        linesInfo = new Dictionary<int, Lines>();
        fadeTime.Clear();
	}
	
	void Update()
	{
        if(!isServer)
            return;


		if(Input.GetMouseButtonDown(0)&&!isDrawing)
		{
			Instantiate(linePrefab);
			isDrawing = true;
		}
		
		if(trailCounter < linesInfo.Count)
		{
			if(!makeTrail)
			return;
            GameObject trail = (GameObject)Instantiate(trailPrefab);
            trail.GetComponent<Trail>().pos = Draw.linesInfo[Draw.trailCounter].GetPosInfo();
            CmdSpawnTrail(trail);
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

    [Command]
    public void CmdSpawnTrail(GameObject obj)
    {
        NetworkServer.Spawn(obj);
    }







	public void SendButton()
	{

        if(!isServer)
            return;
		makeTrail = true;
		trailCounter = 0;
		foreach (GameObject obj in lines)
		{
			obj.transform.GetChild(0).gameObject.SetActive (false);
		}
		if(drawing.Count>1)
		//button2.SetActive(true);


		if(lines.Count == 0)
		return;
        GameObject p = (GameObject)Instantiate(drawingPrefab);
        p.name = "Drawing_" + (drawingIndex+1).ToString();
        CmdSpawnDraw(p);
	
		
	
	}


    [Command]
    public void CmdCubetest()
    {
        GameObject cube = (GameObject)Instantiate(cubeFab);

        NetworkServer.Spawn(cube);
        RpcCube(cube);
      
    }
    [ClientRpc]
    public void RpcCube(GameObject obj)
    {
        obj.AddComponent<Rigidbody>();
    }

    [Command]
    public void CmdSpawnDraw(GameObject p)
    {
        
        NetworkServer.Spawn(p);
        p.name = "Drawing_" + (drawingIndex+1).ToString();

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
