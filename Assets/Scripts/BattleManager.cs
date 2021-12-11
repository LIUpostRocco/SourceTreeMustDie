using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class BattleManager : MonoBehaviour {
	public static int turn;
	public static string turnName;
	public TextMeshProUGUI turnT;
	private NamesMananger nm;
	public Animator battlePanel;
	public GameObject[] turnIndicators;
	public UnityEvent Win;
	public UnityEvent Lose;
	private string endScene;

	private void Start() {
		nm = FindObjectOfType<NamesMananger>();
		
		if (nm == null) {
			throw new MissingReferenceException("No Names Manager");
		}

		UpdateTurnT();
	}

	[ContextMenu("Log Turn")]
	public void LogTurn() {
		Debug.Log(turn + " " + turnName);
	}

	public void EndTurn(int inc = 1) {
		if (inc == 0) {
			inc = 1;
		}

		turn += inc;

		if (turn > 3) {
			turn = 0;
		}

		if (chars[turn].gameObject.activeInHierarchy == false || chars[turn].alive == false) {
			EndTurn();
		}

		#region outcomes
		if (chars[2].alive == false && chars[3].alive == false) {
			Win.Invoke();
		}

		if (chars[0].alive == false && chars[1].alive == false) {
			Lose.Invoke();
		}
		#endregion

		UpdateTurnT();
	}

	public void EndBattle(string whereToNext) {
		endScene = whereToNext;
		Invoke("DoEndBattle", 1f);
	}

	private void DoEndBattle() {
		SceneManager.LoadScene(endScene);
	}

	private void UpdateTurnT() {
		if (turn == 0) {
			turnName = nm.names[0];
		} else if (turn == 1) {
			turnName = nm.names[1];
		} else if (turn == 2) {
			turnName = nm.names[2];
		} else if (turn == 3) {
			turnName = nm.names[3];
		}

		turnT.text = turnName + "'s Turn...";

		foreach (GameObject e in turnIndicators) {
			e.SetActive(false);
		}

		turnIndicators[turn].SetActive(true);
	}

	///////////////////////////////////////////////
	/// Different Attacks n' shit go here
	///////////////////////////////////////////////

	public Character[] chars;

	public void PerformAttack(int which) {
		Debug.Log("do " + turnName + " attacks" + chars[which].name);
		chars[turn].DoAttack();
		chars[which].DoDamage(chars[turn].weapon.damage);
	}

	public void PlayerPray(int who = -1) {
		if (who == -1) {
			Debug.Log("pray called from outside");
			who = turn;
		} else {
			Debug.Log("pray called from script with " + who);
		}

		chars[who].DoPrayer();
		Pray(who, nm.names[who]);
	}

	private void Pray(int who, string theirName) {
		Debug.Log("do " + turnName + " prays");
	}
}

// ~ Rocco Russo