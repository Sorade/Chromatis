using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : ScriptableObject {

	public Color color;
	public Vector4 modifier;

	public Ray ray;
	public RaycastHit hit;

	// Use this for initialization
	void Start () {
		
	}

	public void CopyRay(Ray newRay){
		ray.origin = newRay.origin;
		ray.direction = newRay.direction;
	}

	public void UpdateModifier () {
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

	public void SetColor (Color other){
		color = other;
		UpdateModifier ();
	}
}
