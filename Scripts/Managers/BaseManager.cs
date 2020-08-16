using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour {

	protected GlobalManager globalManager;
	public void Awake(){
		
	}
	// Use this for initialization
	public void Start () {

	}
	
	// Update is called once per frame
	public void Update () {
		
	}

    public virtual void Init(){
        
    }

	public void SetGlobalManager(GlobalManager mgr){
		globalManager=mgr;
	}
}
