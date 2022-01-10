using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {
	public GameObject meteor;
	public GameObject fire;
	public bool canAttack;
	public Animator animator;

	private void Update()
	{
		if (canAttack) {
			FireMeteor();
			animator.SetBool("Attack", true);
		}
	}
	private void FireMeteor() 
	{
		fire.SetActive(false);
		meteor.SetActive(true);
		StartCoroutine(DelayAttack());
	}
	private IEnumerator DelayAttack() 
	{
		yield return new WaitForSeconds(10f);
		fire.SetActive(true);
		meteor.SetActive(false);
		StartCoroutine(WaitForNextAttack());
	}
	private IEnumerator WaitForNextAttack() 
	{
		canAttack = false;
		yield return new WaitForSeconds(10f);
		canAttack = true;
	}
}
