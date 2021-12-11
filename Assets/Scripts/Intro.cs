using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class Intro : MonoBehaviour {
	[Multiline]
	public string[] lore;
	public Sprite[] art;
	public AudioClip[] vo;
	public float minSpeed = 6f;
	private int at;
	public TextMeshProUGUI text;
	public Image bg;
	private float nextTimer;
	private Animator anim;
	private float speed;
	public AudioSource voSource;

	private void Start() {
		anim = GetComponent<Animator>();
		SetCurrentPage();
	}

	private void Update() {
		nextTimer += Time.deltaTime;

		if (nextTimer >= speed) {
			nextTimer = 0f;
			anim.SetTrigger("Next");
		}
	}

	public void Next() {
		at += 1;

		if (at >= lore.Length) {
			Finish();
			return;
		}

		SetCurrentPage();
	}

	private void SetCurrentPage() {
		text.text = lore[at];
		bg.sprite = art[at];

		if (art[at] == null) {
			bg.color = Color.black;
		} else {
			bg.color = Color.white;
		}
		
		if (vo[at] == null) {
			speed = minSpeed;
		} else {
			if (vo[at].length <= minSpeed) {
				speed = minSpeed;
			} else {
				speed = vo[at].length;
			}

			voSource.clip = vo[at];
			voSource.Play();
		}
	}

	public void Finish() {
		SceneManager.LoadScene("Battle");
	}
}

// ~ Rocco Russo