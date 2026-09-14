using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintDisplay : MonoBehaviour

{

	public int randomHint;
	public GameObject hintBar;
	public bool generateHints = false;
	
	void Update()
	{
		if (generateHints = false)

		StartCoroutine(Hints());
	}

	
	IEnumerator Hints()
	{
		randomHint = Random.Range(1,10);
		if (randomHint == 1)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Never run from rogues. Never.";
		}
		if (randomHint == 2)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Archers have pets. They can only use 1 at a time";
		}
		if (randomHint == 3)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Knights are pretty sturdy and rarely die.";
		}
		if (randomHint == 4)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Mages are able to expand their magic and even specialize.";
		}
		if (randomHint == 5)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Warriors are able to overwhelm opponents.";
		}
		if (randomHint == 6)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Mages are able to regenerate magic slowly at all times.";
		}
		if (randomHint == 7)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Priests can heal and deal with opponents. Don't sleep on them.";
		}
		if (randomHint == 8)
		{
			hintBar.GetComponent<TextMeshPro>().text = "When the pet stats are finished, they will rely on the players stats";
		}
		if (randomHint == 9)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Beware of Mardonis the Lich Lord.";
		}
		if (randomHint == 10)
		{
			hintBar.GetComponent<TextMeshPro>().text = "Number 3, Never trust nobody.";
		}
		yield return new WaitForSeconds(9);
		generateHints = false;
	}
	
}