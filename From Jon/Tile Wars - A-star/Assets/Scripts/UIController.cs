using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text unitNameText;
	public GameObject unitControlPanel;

	public void SetUnitInfo(Unit unit) {
		if (unit == null) {
			HideUnitPanel();
			return;
		}

		unitNameText.text = unit.unitName;
		ShowUnitPanel ();
	}

	public void ShowUnitPanel() {
		unitControlPanel.SetActive (true);
	}

	public void HideUnitPanel() {
		unitControlPanel.SetActive (false);
	}
}
