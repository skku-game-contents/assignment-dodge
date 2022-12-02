using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	public int health;

	public void OnHit(int dmg)
	{
		health -= dmg;

		if(health <= 0) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "BorderBullet")
			Destroy(gameObject);
		else if(collision.gameObject.tag == "PlayerBullet"){
			Bullet bullet = collision.gameObject.GetComponent<Bullet>();
			OnHit(bullet.dmg);

			Destroy(collision.gameObject);
		}
	}
}