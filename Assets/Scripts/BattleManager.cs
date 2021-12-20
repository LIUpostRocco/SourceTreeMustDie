using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(BattleOptionDisabler))]
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
	private BattleOptionDisabler bod;
	public Animator enemyPanel;
	public Button enemyButton;
	public TextMeshProUGUI enemySummary;
	private string lastEnemySummary;
	public TextMeshProUGUI imrealtiredofthisallofasudden;
	public TextMeshProUGUI prayOutcome;
	public TextMeshProUGUI playerAttackOutcome;

	private void Update() {
		if (turn == 2 || turn == 3) {
			if (enemyPanel.gameObject.activeInHierarchy == false) {
				enemyPanel.gameObject.SetActive(true);
			}
		}
	}

	private void Start() {
		nm = FindObjectOfType<NamesMananger>();
		bod = GetComponent<BattleOptionDisabler>();
		
		if (nm == null) {
			throw new MissingReferenceException("No Names Manager");
		}

		UpdateTurnT();
	}

	[ContextMenu("Log Turn")]
	public void LogTurn() {
		Debug.Log(turn + " " + turnName);
	}

	[ContextMenu("End Turn")]
	public void EndTurn() {//int inc = 1) {
		//if ur bringing this back ur gonna needa get rid of that int below
		//if (inc == 0) {
			int inc = 1;
		//}

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
			return;
		}

		if (chars[0].alive == false && chars[1].alive == false) {
			Lose.Invoke();
			return;
		}
		#endregion

		UpdateTurnT();
		bod.CheckEnemyAtkButtons();
		nm.Start();

		if (turn == 2 || turn == 3) {
			battlePanel.Play("Out");
			enemyPanel.transform.parent.gameObject.SetActive(true);
			enemyPanel.gameObject.SetActive(true);
			enemyButton.Select();
			DoAI();
			enemySummary.text = turnName + " " + lastEnemySummary;
		} else {
			battlePanel.transform.parent.gameObject.SetActive(true);
		}
	}

	private void DoAI() {
		int a = Random.Range(0, 2);

		if (a == 0) {
			//RNG chose to attack
			int t = Random.Range(0, 2);

			if (chars[t].alive == true) {
				PerformAttack(t);
				lastEnemySummary = "attacked " + nm.names[t] + " for " + chars[turn].weapon.damage + "!";
				return;
			} else {
				t += 1;

				if (t >= 2) {
					t = 1;
				}

				PerformAttack(t);
				return;
			}
		} else {
			//RNG chose to Pray
			Pray(turn, turnName);
			return;
		}
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

		imrealtiredofthisallofasudden.text = nm.names[0] + ": " + chars[0].health + "\n" + nm.names[1] + ": " + chars[1].health + "\n";
	}

	///////////////////////////////////////////////
	/// Different Attacks n' shit go here
	///////////////////////////////////////////////

	public Character[] chars;

	public void PerformAttack(int which) {
		Debug.Log("do " + turnName + " attacks" + nm.names[which]);
		chars[turn].DoAttack();
		chars[which].DoDamage(chars[turn].weapon.damage);
		playerAttackOutcome.text = turnName + " attacked " + nm.names[which] + " for " + chars[turn].weapon.damage + "!";
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
		prayOutcome.text = nm.names[turn] + " " + lastEnemySummary;
	}

	private void Pray(int who, string theirName) {
		int p = Random.Range(0, 5);
		lastEnemySummary = "prayed but nothing happened...";

		if (p == 1) {
			chars[turn].Heal(5);
			lastEnemySummary = "prayed but the Fortnite Gods only blessed them with 5 HP.";
		} else if (p == 2) {
			if (turn == 0 || turn == 1) {
				if (chars[0].alive == false) {
					chars[0].Revive();
					lastEnemySummary = "prayed and " + nm.names[0] + " stands up!";
				} else if (chars[1].alive == false) {
					chars[1].Revive();
					lastEnemySummary = "prayed and " + nm.names[1] + " stands up!";
				}
			} else {
				if (chars[2].alive == false && chars[2].isOut == false) {
					chars[2].Revive();
					lastEnemySummary = "prayed and " + nm.names[2] + " stands up!";
				} else if (chars[3].alive == false && chars[3].isOut == false) {
					chars[3].Revive();
					lastEnemySummary = "prayed and " + nm.names[3] + " stands up!";
				}
			}
		} else if (p == 3) {
			if (turn == 2) {
				nm.names[3] = "Block Man";
				chars[3].gameObject.SetActive(true);
				chars[3].isOut = false;
				lastEnemySummary = "prayed and Block Man came to beat you up!";
			} else {
				lastEnemySummary = "prayed but only felt inspired...";
			}
		} else if (p == 4) {
			lastEnemySummary = "prayed for a good grade... who knows if it'll work...";
		} else {
			lastEnemySummary = "prayed but nothing happened...";
		}
	}
}

// ~ Rocco Russo