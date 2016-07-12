using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Lines : MonoBehaviour 
{
	private List<Vector3> linePos = new List<Vector3>();
	private float fadeTimer;
    private bool isStopped;
    private float stopTimer;
    private int i;
    private int count;
	private EventSystem es;
	private LineRenderer lr;
	private bool done;





    // Use this for initialization
    void Start () 
	{
		fadeTimer = 0;
		isStopped = false;
		stopTimer = 0f;
		i = 0;
		count = 0;
		es = GameObject.FindGameObjectWithTag("ES").GetComponent<EventSystem>();
		lr = GetComponent<LineRenderer>();

	}
	
	void FixedUpdate () 
	{
		if(done)
		return;

		if(Input.GetMouseButton(0))
		{
			if(isStopped)
			return;	
			Drawing();
		}

//X trail
		if(!Input.GetMouseButton(0))
		{
			isStopped = true;
			if(fadeTimer >= 0.05f)
			{
				Draw.lines.Add(gameObject);
				Draw.fadeTime.Add(fadeTimer+1f);
				Draw.linesInfo.Add(Draw.trailCounter, this);
				Draw.trailCounter++;
				//linePos.Clear();
				this.enabled = false;
			}
			else
			{
				DestroyImmediate(gameObject);
			}
			Draw.isDrawing = false;
			done = true;
		}

	}
	void Drawing()
	{
		if(es.IsPointerOverGameObject())
		return;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Vector3 pos;
		
		if(Physics.Raycast(ray,out hit,Mathf.Infinity,1<<8))
		{
			pos = hit.point + new Vector3(0f,0f,-2f);

			if(linePos.Count != 0)
			{
				if((linePos[count-1]- (hit.point + new Vector3(0f,0f,-2f))).magnitude <=0.04f)
				return;
			}
			count ++;						
			fadeTimer += Time.deltaTime;
			lr.SetVertexCount(count);
			lr.SetPosition(count-1,pos);
			lr.transform.GetChild(0).position = pos;
			linePos.Add(pos);
		}
		
	}

	public List<Vector3> GetPosInfo()
	{
		return linePos;
	}
}
