using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour {

	public GameObject spawnPrefab;

	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;
	
	
	private float savedTime;
	private float secondsBetweenSpawning;
	[SerializeField]
	bool isMaximum=false;
	[SerializeField]
	int maxLimit=5;
	float radius;
	[SerializeField]
	float spawnRange=10f;

	// Use this for initialization
	void Start () {
		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		radius = spawnPrefab.GetComponent<SphereCollider>().radius;
	}
	
	// Update is called once per frame
	void Update () {
		int currentNumber = GameObject.FindGameObjectsWithTag(spawnPrefab.tag).Length;
		float spawnRandom = Random.Range(-spawnRange, spawnRange);

		if (!isMaximum)
        {
			if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
			{
				MakeThingToSpawn(spawnRandom);
				savedTime = Time.time; // store for next spawn
				secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
			}
        }
        else
        {
			if (Time.time - savedTime >= secondsBetweenSpawning ) // is it time to spawn again?
			{
				savedTime = Time.time; // store for next spawn
                if (currentNumber < maxLimit)
                {
					if(!Physics.CheckSphere(transform.position, radius))
                    {
						MakeThingToSpawn(0);
						secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
                    }
                    
                }
			}
		}
			
	}

	void MakeThingToSpawn(float spawnRandom)
	{
		// create a new gameObject
		GameObject clone = Instantiate(spawnPrefab,new Vector3(transform.position.x+spawnRandom,
			transform.position.y,transform.position.z+spawnRandom),transform.rotation) as GameObject;

		
	}
}
