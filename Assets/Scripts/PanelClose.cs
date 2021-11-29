using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelClose : MonoBehaviour {
	public void Close() {
		transform.parent.gameObject.SetActive(false);
	}

	public void TurnSummaryClose() {
		gameObject.SetActive(false);
	}
}

// ~ Rocco Russo