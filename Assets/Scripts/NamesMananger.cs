using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NamesMananger : MonoBehaviour {
	public string[] names;
	[Space]
	public TextMeshProUGUI attackTText1;
	public TextMeshProUGUI attackTText2;

	public void Start() {
		attackTText1.text = names[2];
		attackTText2.text = names[3];
	}
}

// ~ Rocco Russo