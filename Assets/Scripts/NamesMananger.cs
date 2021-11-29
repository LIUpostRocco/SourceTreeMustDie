using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NamesMananger : MonoBehaviour {
	public string player1;
	public string player2;
	public string enemy1;
	public string enemy2;
	[Space]
	public TextMeshProUGUI attackTText1;
	public TextMeshProUGUI attackTText2;

	private void Start() {
		attackTText1.text = enemy1;
		attackTText2.text = enemy2;
	}
}

// ~ Rocco Russo