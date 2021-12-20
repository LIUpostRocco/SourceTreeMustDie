using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public Weapon weapon;
	public int startHealth = 100;
	public int health;
	public bool alive {
		get {
			if (health > 0 && isOut == false) {
				return true;
			} else {
				return false;
			}
		}
	}
	private Animator anim;
	public bool doAnimator = true;
	public bool isOut;

	private void Awake() {
		health = startHealth;
	}

	private void Start() {
		anim = GetComponent<Animator>();
	}

	private void Update() {
		anim.SetBool("Ded", !alive);
	}

	public void DoAttack() {
		anim.SetTrigger("Attack");
	}

	public void DoPrayer() {
		anim.SetTrigger("Pray");
	}

	public void DoDamage(int amount = 0) {
		if (amount == 0) {
			Debug.LogWarning("DoDamage was called with no amount - I'll set it to five but you really should set this to something on your own!");
			amount = 5;
		}

		health -= amount;

		if (alive == true) {
			anim.SetTrigger("Ow");
		}
	}

	[ContextMenu("Revive")]
	public void Revive() {
		if (health <= 0) {
			health = Mathf.RoundToInt(startHealth * 0.10f);
		}
	}

	public void Heal(int amount) {
		if (health > 0) {
			health += amount;
		}
	}
}

// ~ Rocco Russo