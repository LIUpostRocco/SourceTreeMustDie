using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DisallowMultipleComponent]
public class BattleMenu : MonoBehaviour {
	public static int turn;
	public TextMeshProUGUI turnT;

	private void Start() {
		UpdateTurnT();
	}

	private void EndTurn(int inc = 1) {
		turn += inc;

		if (turn > 3) {
			turn = 0;
		}

		UpdateTurnT();
	}

	private void UpdateTurnT() {
		if (turn == 0) {
			turnT.text = "Chad's Turn...";
		} else if (turn == 1) {
			turnT.text = "Jacknife's Turn...";
		} else if (turn == 2) {
			turnT.text = "First Enemy's Turn!";
		} else if (turn == 3) {
			turnT.text = "Second Enemy's Turn!";
		}
	}

	///////////////////////////////////////////////
	/// Different Attacks n' stuff go here
	///////////////////////////////////////////////

	public void TestAttack() {
		EndTurn();
	}
}

// ~ Rocco Russo