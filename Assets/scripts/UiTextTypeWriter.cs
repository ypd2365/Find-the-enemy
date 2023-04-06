using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UiTextTypeWriter : MonoBehaviour
{

	Text txt;
	string story;
	public GameObject playBtn;

	void Awake()
	{
		txt = GetComponent<Text>();
		story = txt.text;
		txt.text = "";

		
		StartCoroutine("PlayText");//start text animation
	}

	IEnumerator PlayText()
	{
		foreach (char c in story)
		{
			txt.text += c;
			yield return new WaitForSeconds(0.02f);//each character displayed after 0.1sec
		}
		playBtn.SetActive(true);//show play button when all character displayed
	}

}