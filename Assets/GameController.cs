﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController Instance;

	private int coins;
	private int health;
	public float GameSpeed = 1f;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
			return;
		}
		Instance = this;
	}

	private void Start()
	{
		//Take base values from SO - health, coins
	}

	public void AddReward(int coinsToAdd)
	{
		coinsToAdd += coins;
		//update hud
		//update radial menu if active
	}

	public void TakeDamage(int damageToDeal)
	{
		health -= damageToDeal;
		if (health <= 0)
		{
			//gameover
		}
	}
}
