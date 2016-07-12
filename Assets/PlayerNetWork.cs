using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerNetWork :  NetworkBehaviour
{

	public GameObject cam;
	private GameObject mainCam;
	public GameObject cube,image;
	public Transform panel;

	[SyncVarAttribute] private Color col;
	//[SyncVarAttribute] private GameObject ObjID;

   public override void OnStartLocalPlayer()
    {
        col = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),0.5f);
		//GetComponentInChildren<Image>().color = col;
    }
	// Use this for initialization
	void Start () 
	{
		if(Camera.main)
		{
			mainCam = Camera.main.gameObject;
			mainCam.SetActive(false);
			
		}

		gameObject.name = "Player " + GetComponent<NetworkIdentity>().netId;
		
		if(isLocalPlayer)
		{
		
			return;
			
		}

		if(cam)
		cam.SetActive(false);


		
	}

	void Update()
	{
		if (!isLocalPlayer)
        return;

		if(Input.GetMouseButtonDown(0))
		{
		  Spawn(col);	
		}
	}

	public void Spawn(Color col)
	{
		//CmdCubeTest(col);
		CmdUITest(col);
	}
	[ClientRpcAttribute]
	void RpcColor(GameObject obj, Color col)
	{
		if(obj.GetComponent<MeshRenderer>())
		{
			obj.GetComponent<MeshRenderer>().material.color = col;
		
		}
		else
		{
			obj.transform.SetParent(panel,false);
			obj.transform.localScale = new Vector3(1,1,1);
			obj.GetComponent<Image>().color = col;
		}
		
	}



	[Command]
	public void CmdCubeTest(Color col)
	{
		Debug.Log("Here");
		//cube = GameObject.FindGameObjectWithTag("Cube");		
		GameObject obj = (GameObject)Instantiate(image);
		
		
		obj.GetComponent<MeshRenderer>().material.color = col;
		obj.AddComponent<Rigidbody>();
		
		NetworkServer.Spawn(obj);		
		RpcColor(obj, col);
		
		//NetworkServer.

		Destroy(obj,3.5f);
		//obj.tag = "Untagged";

		
	}
	[Command]
	public void CmdUITest(Color col)
	{
		Debug.Log("Here2");
		//cube = GameObject.FindGameObjectWithTag("Cube");		
		GameObject obj = (GameObject)Instantiate(image);
		
		
		obj.transform.SetParent(panel,false);
		obj.transform.localScale = new Vector3(1,1,1);
		obj.GetComponent<Image>().color = col;
		
		NetworkServer.Spawn(obj);		
		RpcColor(obj, col);
		
		//NetworkServer.

		Destroy(obj,3.5f);
		//obj.tag = "Untagged";

		
	}
}
