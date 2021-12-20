using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BattleManager))]
public class BattleOptionDisabler : MonoBehaviour {
	private BattleManager bm;
	public Button atk1;
	public Button atk2;

	private void Start() {
		bm = GetComponent<BattleManager>();

		CheckEnemyAtkButtons();

		foreach (Character c in bm.chars) {
			if (c.gameObject.activeInHierarchy == false) {
				c.isOut = true;
			}
		}
	}

	public void CheckAtkButtons(int of, Button button) {
		if (bm.chars[of].gameObject.activeInHierarchy == false || bm.chars[of].alive == false) {
			button.interactable = false;
		} else {
			button.interactable = true;
		}
	}

	public void CheckEnemyAtkButtons() {
		CheckAtkButtons(2, atk1);
		CheckAtkButtons(3, atk2);
	}
}

// ~ Rocco Russo