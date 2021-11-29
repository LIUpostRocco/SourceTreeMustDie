using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DisallowMultipleComponent]
public class BattleMenu : MonoBehaviour {
	public static int turn;
	public static string turnName;
	public TextMeshProUGUI turnT;
	private NamesMananger nm;

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

		UpdateTurnT();
	}

	private void UpdateTurnT() {
		if (turn == 0) {
			turnName = nm.player1;
		} else if (turn == 1) {
			turnName = nm.player2;
		} else if (turn == 2) {
			turnName = nm.enemy1;
		} else if (turn == 3) {
			turnName = nm.enemy2;
		}

		turnT.text = turnName + "'s Turn...";
	}

	///////////////////////////////////////////////
	/// Different Attacks n' shit go here
	///////////////////////////////////////////////

	public void PlayerAttack() {
		Debug.Log("do " + turnName + " attack");
	}

	public void EnemyAttack() {
		Debug.Log("do " + turnName + " attack");
	}

	public void PlayerPray() {
		Debug.Log("do " + turnName + " prays");
	}
}

// ~ Rocco Russo