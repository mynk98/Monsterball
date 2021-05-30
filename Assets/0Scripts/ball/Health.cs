using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public enum deathAction {loadLevelWhenDead,doNothingWhenDead};
	
	public float healthPoints = 100f;
	public float respawnHealthPoints = 100f;        //base health points
	public const float maxHealth = 100f;
	public int numberOfLives = 3;                   //lives and variables for respawning
	public const int maxLives = 5;
	public bool isAlive = true;
	public bool dead = false;

	//public GameObject explosionPrefab;
	
	public deathAction onLivesGone = deathAction.doNothingWhenDead;
	
	public string LevelToLoad = "";
	
	private Vector3 respawnPosition;
	private Quaternion respawnRotation;
	Animator animator;
	[SerializeField] AudioSource takeDamageSound;



	// Use this for initialization
	void Start () 
	{
		// store initial position as respawn location
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;

		animator = GetComponent<Animator>();
		
		if (LevelToLoad=="") // default to current scene 
		{
			LevelToLoad = Application.loadedLevelName;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (healthPoints <= 0 && !dead) {				// if the object is 'dead'
			numberOfLives--;                    // decrement # of lives, update lives GUI
			animator.SetBool("isDying", true);
			animator.SetBool("isKillerBall", false);
			dead = true;
			
			if (numberOfLives < 0) { 
			 // here is where you do stuff once ALL lives are gone)
				isAlive = false;
				
				switch(onLivesGone)
				{
				case deathAction.loadLevelWhenDead:
					Application.LoadLevel (LevelToLoad);
					break;
				case deathAction.doNothingWhenDead:
					// do nothing, death must be handled in another way elsewhere
					break;
				}
				
			}
		}
	}
	
	public void ApplyDamage(float amount)
	{	
		healthPoints = healthPoints - amount;
		takeDamageSound.Play();
		print(healthPoints);
	}
	
	public void ApplyHeal(float amount)
	{
        if (healthPoints + amount <= maxHealth)
        {
			healthPoints+=amount;

		}
		else
        {
			healthPoints=100;

		}

	}

	public void ApplyBonusLife()
	{
        if (numberOfLives < maxLives)
        {
			numberOfLives = numberOfLives + 1;
		}
	}
	
	public void updateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation) {
		respawnPosition = newRespawnPosition;
		respawnRotation = newRespawnRotation;
	}

	public void Respawn()
    {
		transform.position = respawnPosition;   // reset the player to respawn position
		transform.rotation = respawnRotation;
		healthPoints = respawnHealthPoints;
		dead = false;
		animator.SetBool("isDying", false);
		
	}

	/*public void DamagePerSecond(int damageAmount)
	{
		if (Time.time - savedTime >= 1)
		{
			savedTime = Time.time;
			ApplyDamage(damageAmount);
		}
	}*/
}
