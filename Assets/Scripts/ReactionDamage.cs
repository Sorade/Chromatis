using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDamage : Reaction {

	public Vector4 health;
	public Color color;
	public float delay;
	public GameObject deathVFX;

	private float maxHealth;
	private MeshRenderer meshRenderer;


	float timer;


	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
		maxHealth = Mathf.Max (health.x, health.y, health.z, health.w);
		UpdateColor ();
	}

	public override void React(LightRay LightRay){
		if (timer > delay) {
			timer = 0f;
			TakeDamage (LightRay.modifier);
		}
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

		if ((health.x + health.y + health.z) <= 0f) {
			Death();
		}
	}

	public void UpdateColor(){

		Vector4 temp = new Vector4 (
			Normalise(health.x, maxHealth), 
			Normalise(health.y, maxHealth),  
			Normalise(health.z, maxHealth), 
			Normalise(health.w, maxHealth));
		
		color = (Color)temp;
		meshRenderer.material.color = color;
	}


	void Update(){
		timer += Time.deltaTime;
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

	void Death(){
		Instantiate (deathVFX, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
