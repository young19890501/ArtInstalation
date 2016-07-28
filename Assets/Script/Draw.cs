using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;

public class Draw : NetworkBehaviour 
{

	
     private EventSystem es;
    [SerializeField] private GameObject linePrefab,trailPrefab,drawingPrefab,cubeFab;
    [SerializeField] private List<GameObject> trailPos = new List<GameObject>();
    [SerializeField]private Text textOnScreen;

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
    private bool loop = false;
    private UnityWebRequest fanOn;
    private UnityWebRequest fanOff;
    private string fanOnUrl;
    private string fanOffUrl;
    private float textY;
    private bool moveText;





    // Use this for initialization
    void Start () 
	{
		es = GameObject.FindGameObjectWithTag("ES").GetComponent<EventSystem>();
		trailCounter = 0;
        makeTrail = false;
        linesInfo = new Dictionary<int, Lines>();
        fadeTime.Clear();
        fanOnUrl = "https://maker.ifttt.com/trigger/FanOn/with/key/mU8HaIFTHcFT98HKNz_l-8-QQNqjbzB6Ogf44ibLorM";
        fanOffUrl = "https://maker.ifttt.com/trigger/FanOff/with/key/mU8HaIFTHcFT98HKNz_l-8-QQNqjbzB6Ogf44ibLorM";
        fanOn = UnityWebRequest.Get(fanOnUrl);
        fanOff = UnityWebRequest.Get(fanOffUrl);
        moveText = false;
	}
	
	void Update()
	{
        if(!isServer)
            return;


        if(Input.touchCount != 0 &&!isDrawing)
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


        if(drawing.Count >3 && !loop)
        {
            drawing[0].SetActive(true);
            drawing[UnityEngine.Random.Range(1,drawing.Count-1)].SetActive(true);
            loop = true;
        }

        if((trailCounter!= 0 || isDrawing) && !moveText)
        {
            textOnScreen.gameObject.SetActive(false);
        }
        else
        {
            textOnScreen.gameObject.SetActive(true);
        }

        if(moveText)
        {
            textY += 40f * Time.deltaTime;
            textOnScreen.rectTransform.anchoredPosition = new Vector2(0,textY);
        }
        else
        {
            textY = 0f;
            textOnScreen.rectTransform.anchoredPosition = Vector2.zero;
        }
		

	}

    [Command]
    public void CmdSpawnTrail(GameObject obj)
    {
        NetworkServer.Spawn(obj);
    }



	public void SendButton(Button btn)
	{

        if(!isServer ||lines.Count == 0)
            return;
        
        fanOn = UnityWebRequest.Get(fanOnUrl);
        fanOff = UnityWebRequest.Get(fanOffUrl);

        
        StartCoroutine(FanOn(btn));
		makeTrail = true;
		trailCounter = 0;
		foreach (GameObject obj in lines)
		{
			obj.transform.GetChild(0).gameObject.SetActive (false);
		}
            
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




	public GameObject GetCurParent()
	{
		return curDrawingParent;
	}

    public Vector3 GetRandomOffest()
    {
        return trailPos[UnityEngine.Random.Range(0,trailPos.Count -1)].transform.position;
    }

    IEnumerator FanOn(Button btn )
    {
        btn.gameObject.SetActive(false);
        textOnScreen.text = "And Now....";
        yield return new WaitForSeconds(2.0f);
        moveText = true;
        textOnScreen.text = "Look at the curtain.\nAnd the wind will show you the way...";
        fanOn.Send();
        yield return new WaitForSeconds(22.0f);
        fanOff.Send();
        yield return new WaitForSeconds(2.0f);
        btn.gameObject.SetActive(true);
        moveText = false;
        fanOn.Dispose();
        fanOff.Dispose();
        textOnScreen.text = "1. Play with the scene on the other ipad.\n\n2. Draw or write something here.";
        StopAllCoroutines();

    }

}
