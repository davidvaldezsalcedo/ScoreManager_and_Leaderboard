using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreManager : Singleton<ScoreManager>
{

	#region Globals
	internal Text textBox;
	public static float Timeout = 5;
	private static int TotalScore = 0;
	public int Multiplier = 1;
	internal int CurrentScore;
	public int TotalKills = 0;
	public bool multOn = false;
	
	//create a dictionary for input of <"type of points to be gained">, <"User">, and <"Amount of points gained">
	public Dictionary<string, Dictionary<string, int>> Scores = new Dictionary<string, Dictionary<string, int>>();


	#endregion

	#region  UnityFunctions
	
	public override void SingletonAwake()
	{

	}

	private void DontDestroyOnLoad()
	{
		DontDestroyOnLoad(this);
	}

	void Start()
	{
		DontDestroyOnLoad();
		
		//default test scores, can be erased
		AddScore("BBB", "Score", 200);
		AddScore("BBB", "Kills", 5);
		
		if(Scores.Count>0)
			print(Scores.Count +", "+ Scores["Score"].Count);
	}


	void Update()
	{
		//update timeout timer for multiplier
		if(multOn)
			Timeout -= Time.deltaTime;
		
		//minus multiplier by one and restart count
		if(Multiplier > 0 && Timeout <= 0)
		{
			Multiplier -= 1;
			Timeout = 5;
		}
		else if(Multiplier < 0)		//stop multiplier on 0
		{
			Multiplier = 0;
			Timeout = 0;
			multOn = false;
		}

		if(TotalScore!=0)
			textBox.text = "Score\n" + TotalScore + "\nMult\nx" + Multiplier;

	}


	#endregion

	#region Functions
	
	//calculate the score and start multiplier then return total
	public int CalculateScore(int value)
	{
		Timeout = 5;
		multOn = true;
		CurrentScore = value * Multiplier;
		TotalScore = TotalScore + CurrentScore;
		CurrentScore = 0;
		
		//add to the score dictionary
		AddScore("AAA", "Score", TotalScore);

		return TotalScore;
	}
	
	//calculate enemies killed
	public int CalculateKills(int value)
	{
		TotalKills += value;

		//add to kills dictionary
		AddScore("AAA", "Kills", TotalKills);

		return TotalKills;
	}

	//add to the score with inputs of user name and type of score and score to add
	public void AddScore(string userName, string scoreName, int score)
	{
		//if there is no score name then it will make one
		if(Scores.ContainsKey(scoreName) == false)
		{
			Scores.Add(scoreName, new Dictionary<string, int>());

		}
		Scores[scoreName][userName] = score;
	}

	//gets the score desired and returns it
	public int GetScore(string userName, string scoreName)
	{
		if(Scores.ContainsKey(scoreName) == false)
		{
			return 0;
		}

		if(Scores[scoreName].ContainsKey(userName) == false) {
			return 0;
		}

		return Scores[scoreName][userName];
	}

	//uses linq to sort the scores in descending order
	public KeyValuePair<string, int>[] GetTopScores(string scoreName, int howMany)
	{
		return Scores[scoreName].OrderByDescending( n => GetScore(n.Key , scoreName) )
			.Take(howMany)
			.ToArray();
	}

	//resets the scores
	public void Reset() {
		Scores.Clear();
	}

	#endregion

	//prints it on gui for debuging
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		foreach(var x in Scores)
		{
			GUILayout.BeginVertical();
			GUILayout.Label(x.Key);
			GUILayout.Space(32);
			foreach(var y in GetTopScores(x.Key, 5))
			{
				GUILayout.Label(y.Key +" - "+ y.Value);
			}
			GUILayout.EndVertical();
			GUILayout.Space(100);
		}
		GUILayout.EndHorizontal();
	}

}

