using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trail : MonoBehaviour 
{

	private bool complete;
	private int i;
	public int k;
	private List<Vector3> pos = new List<Vector3>();
    public bool done;
	private TrailRenderer tr;
	private float  t;
		// Use this for initialization
	void Start () 
	{
		i = 0;
		pos = Draw.linesInfo[Draw.trailCounter].GetPosInfo();
		GetComponent<TrailRenderer>().time = Mathf.Max(0f,Draw.fadeTime[k]);
		Draw.trailCounter ++;
		transform.SetParent(GameObject.FindGameObjectWithTag("Drawer").GetComponent<Draw>().GetCurParent().transform);
		gameObject.layer = 9;
		tr = GetComponent<TrailRenderer>();
	
		
		
	}
	void OnEnable()
	{
		i = 0;
		t = 0;
		done = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		while(i < pos.Count)
		{
			transform.position = pos[i];
			StartCoroutine(Wait());
			if(!complete)
			return;
			i++;
			complete = false;
		}

		t +=  Time.deltaTime;
		if(t>= (tr.time - 0.5f) * 2f)
		done = true;

			
		

	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(0.2f);
		complete = true;

	}


	

	
}
