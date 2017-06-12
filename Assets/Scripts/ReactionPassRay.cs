using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionPassRay : Reaction {

	public Color color;

	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float timeBetweenBullets = 0.15f;        // The time between each shot.
	public float range = 100f;                      // The distance the gun can fire.

	Ray shootRay = new Ray() ;                                   // A ray from the gun end forwards.
	RaycastHit shootHit = new RaycastHit();                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	//ParticleSystem gunParticles;                    // Reference to the particle system.
	LineRenderer gunLine;                           // Reference to the line renderer.
	//AudioSource gunAudio;                           // Reference to the audio source.
	//Light gunLight;                                 // Reference to the light component.
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
	int bounces;

	float timer;
	float timeSinceHit;
	bool reacting;

	void Awake ()
	{
		// Create a layer mask for the Shootable layer.
		shootableMask = LayerMask.GetMask ("Shootable");

		// Set up the references.
		//gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		//gunAudio = GetComponent<AudioSource> ();
		//gunLight = GetComponent<Light> ();
	}

	public override void React(LightRay lightRay){
		PassOnRay (lightRay);
	}

	void Update ()
	{
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(!reacting)
		{
			// ... disable the effects.
			DisableEffects ();
		}
		reacting = false;
	}

	public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		//gunLight.enabled = false;
	}
	
	void PassOnRay (LightRay lightRay)
	{
		reacting = true;

		if (bounces > 0) {
			bounces = 0;
			return;
		}
		bounces++;
		// Play the gun shot audioclip.
		//gunAudio.Play ();

		// Enable the light.
		//gunLight.enabled = true;

		// Stop the particles from playing if they were, then start the particles.
		//gunParticles.Stop ();
		//gunParticles.Play ();

		// Enable the line renderer and set it's first position to be the end of the gun.
		gunLine.enabled = true;
		gunLine.SetPosition (0, lightRay.hit.point);
		gunLine.SetPosition(0, lightRay.hit.point + lightRay.ray.direction.normalized); //to remove
		//gunLine.SetColors (Color.blue, Color.blue);
		//gunLine.material = new Material (Shader.Find ("Unlit/Texture"));
		gunLine.material.color = lightRay.color + color;

		gunLine.startWidth = 0.1f;

		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = lightRay.hit.point + lightRay.ray.direction.normalized;
		shootRay.direction = lightRay.ray.direction;
		lightRay.AddColor (color);

		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			// Try and find an EnemyHealth script on the gameobject hit.
			Reaction[] reactions = shootHit.collider.GetComponents<Reaction> ();
			// If the EnemyHealth component exist...
			if(reactions != null)
			{
				lightRay.hit = shootHit;
				// ... the target should trigger all its reactions
				for (int i = 0; i < reactions.Length; i++) {					
					reactions [i].React (lightRay);
				}
			}

			// Set the second position of the line renderer to the point the raycast hit.
			gunLine.SetPosition (1, shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else
		{
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}
