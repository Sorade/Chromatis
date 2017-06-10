using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {

	public Vector4 health;
	public Color color;
	private float maxHealth;
	private MeshRenderer renderer;


	// Use this for initialization
	void Start () {
		renderer = GetComponent<MeshRenderer> ();
		maxHealth = Mathf.Max (health.x, health.y, health.z, health.w);
	}
	
	// Update is called once per frame
	public void TakeDamage (Vector4 modifier) {
		health -= modifier;
		if (Mathf.Min (health.x,health.y,health.z,health.w) < 0) {
			health = new Vector4 (
				Mathf.Max (health.x, 0f), 
				Mathf.Max (health.y, 0f), 
				Mathf.Max (health.z, 0f), 
				Mathf.Max (health.w, 0f));
		}
		UpdateColor ();
	}

	void UpdateColor(){

		Vector4 temp = new Vector4 (
			Normalise(health.x, maxHealth), 
			Normalise(health.y, maxHealth),  
			Normalise(health.z, maxHealth), 
			Normalise(health.w, maxHealth));
		
		color = (Color)temp;
		renderer.material.color = color;
	}

	void Update(){
		TakeDamage (new Vector4(0f,1f,0f,0f));
	}

	float Normalise(float x, float max){
		if (x == max)
		{
			return max;
		}
		else if (x == 0f)
		{
			return 0f;
		}
		else
		{
			return x / max;
		}
	}
}
