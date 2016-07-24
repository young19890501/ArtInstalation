using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Trail :  NetworkBehaviour
{

	private bool complete;
	private int i;
	public int k;
	public List<Vector3> pos = new List<Vector3>();
    public bool done;
	private TrailRenderer tr;
	private float  t;

    public Vector3 offset;
    private Drawing drawingParent;

		// Use this for initialization
	void Start () 
    {

        if(!isServer)
        return;

        i = 0;
		
		GetComponent<TrailRenderer>().time = Mathf.Max(0f,Draw.fadeTime[k]);
		Draw.trailCounter ++;
		gameObject.layer = 9;
     
        transform.SetParent(GameObject.FindGameObjectWithTag("Drawer").GetComponent<Draw>().GetCurParent().transform);
		tr = this.GetComponent<TrailRenderer>();
		
	}
	void OnEnable()
	{
        drawingParent = GetComponentInParent<Drawing>();
		i = 0;
		t = 0;
		done = false;
        if(drawingParent != null)
        offset = drawingParent.GetOffest();
       

	}
	
	// Update is called once per frame
	void Update () 
	{
        if(!isServer)
            return;


		while(i < pos.Count)
		{
            transform.position = pos[i] * 0.4f + offset;
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
