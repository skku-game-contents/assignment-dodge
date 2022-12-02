using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public GameObject[] enemyObjs;
	public Transform[] spawnPoints;

	public float maxSpawnDelay;
	public float curSpawnDelay;

	public GameObject player;
	[SerializeField] private Texture2D[] playerImage;

	[SerializeField] private GameObject gameOverUI;

	public GameObject item1;

	public static event Action LevelChanged; 

	void Update()
	{
		curSpawnDelay += Time.deltaTime;

		if(curSpawnDelay > maxSpawnDelay) {
			SpawnEnemy();
			maxSpawnDelay = Random.Range(0.5f, 3f);
			curSpawnDelay = 0;
		}
	}

	void SpawnEnemy()
	{
		int ranEnemy = Random.Range(0, 2);
		int ranPoint = Random.Range(0, 9);
		GameObject enemy = Instantiate(enemyObjs[ranEnemy],
									   spawnPoints[ranPoint].position,
									   spawnPoints[ranPoint].rotation);
		Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
		Enemy enemyLogic = enemy.GetComponent<Enemy>();

		if(ranPoint == 7 || ranPoint == 8) {
		   rigid.velocity = new Vector2(enemyLogic.speed*(-1), -1);
		}
		else if(ranPoint == 5 || ranPoint == 6) {
				rigid.velocity = new Vector2(enemyLogic.speed, -1);
		}
		else {
			rigid.velocity = new Vector2(0, enemyLogic.speed*(-1));
		}
	}

	public void RespawnPlayer()
	{
		Invoke("RespawnPlayerExe", 2f);
	}

	public void RespawnPlayerExe()
	{
		if (++LevelController.main.CurrentLevel > 4)
		{
			gameOverUI.SetActive(true);
			return;
		};
		
		LevelChanged?.Invoke();

		var texture = playerImage[LevelController.main.CurrentLevel - 1]; 
		Rect rect = new Rect(0, 0, texture.width, texture.height);
		player.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
		
		player.transform.position = Vector3.down * 3.5f;
		player.SetActive(true);
	}
}
