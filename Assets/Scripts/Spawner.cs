using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject[] enemies;
	public Transform[] spawnPoints;
	public float spawnDelay;
	public int initialNumber;

	public GameObject[] gates;
	public Vector3[] gateZone = new Vector3[1];
	int gateNumber;

	//Color[] colors = new Color[Color.blue, Color.red, Color.green];
	float timer;

	// Use this for initialization
	void Start () {
		//colors = new Color[Color.blue, Color.red, Color.green];
		for (int i = 0; i < initialNumber; i++) {
			Spawn ();
		}

		/*for (int x = (int)gateZone[1].x; x < (int)(Mathf.Abs(gateZone[0].x - gateZone[1].x)/4); x++) {
			for (int y = (int)gateZone[1].y; y < (int)(Mathf.Abs(gateZone[0].y - gateZone[1].y)/4); y++) {
				GameObject gatePrefab = gates [Random.Range (0, gates.Length)];

				GameObject gate = Instantiate (gatePrefab, new Vector3 (x, 1f, y), Quaternion.identity);
				gate.transform.Rotate (new Vector3 (0f, Random.Range (0f, 360f), 0f));
			}
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > spawnDelay) {
			timer = 0f;
			Spawn ();
		}
	}

	void Spawn(){
		Transform point = spawnPoints[Random.Range (0, spawnPoints.Length)];
		GameObject enemy = enemies[Random.Range (0, enemies.Length)];

		/*int colorNum = Random.Range (1, 3);
		Color color = colors[Random.Range (0, colors.Length)];

		if (colorNum == 2) {
			color += colors [Random.Range (0, colors.Length)];
		}*/

		GameObject newEnemy = Instantiate (enemy, point.position, point.rotation);
		/*newEnemy.GetComponent<ReactionDamage> ().color = color;
		newEnemy.GetComponent<ReactionDamage> ().UpdateColor ();*/
	}
}
