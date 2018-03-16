using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreBoard : MonoBehaviour {
	
	public Text[] players;
	private int i = 0;
	
	void OnEnable()
	{
		ShowText();
	}

	void ShowText()
	{
		//displays the score on a scoreboard
		foreach(var x in ScoreManager.instance.Scores)
		{
			string text = x.Key + "\n";
			foreach(var y in ScoreManager.instance.GetTopScores(x.Key, 5))
			{
				text += "\n" + y.Key +" - "+ y.Value;
			}
			players[i].text = text;
			i++;
		}
		i = 0;
	} 
}
