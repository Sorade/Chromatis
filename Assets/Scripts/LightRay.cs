using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour {

	public Color color;
	public Vector4 modifier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void UpdateModifier () {
		Vector4 newMod = (Vector4)color;
		newMod.Normalize ();
		newMod = new Vector4 (Mathf.Ceil(newMod.x), Mathf.Ceil(newMod.y), Mathf.Ceil(newMod.z), Mathf.Ceil(newMod.w));
		modifier = newMod;
	}

	public void AddColor (Color other){
		color += other;
		UpdateModifier ();
	}

	public void RemColor (Color other){
		color -= other;
		UpdateModifier ();
	}
}
