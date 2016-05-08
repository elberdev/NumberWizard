using UnityEngine;
using System.Collections;
using System.Reflection; // this will help us clear the console programmatically

public class NumberWizard : MonoBehaviour {
	int max, min, guess;

	// Use this for initialization
	void Start () {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			// the ternary assignment will make sure that max and min pointers
			// don't 'cross' each other.
			min = guess > max ? max : guess; 
			print ("Min = " + min);
			NextGuess ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			max = guess < min ? min : guess; 
			print ("Max = " + max);
			NextGuess ();
		} else if (Input.GetKeyDown (KeyCode.Return)) {
			ClearLog ();
			print ("+++++ I WON! +++++");
			StartCoroutine (Wait2SecondsThenRestart ());
		}
	}

	void StartGame() {
		ClearLog ();
		max = 1000;
		min = 1;
		guess = 500;

		print ("==============================================");
		print ("Welcome to Number Wizard");
		print ("Pick a number in your head, but don't tell me!");

		print ("The highest number you can pick is " + max);
		print ("The lowest number you can pick is " + min);

		// make the real max 1001 so that 1000 becomes a valid guess
		// by the computer.
		max += 1; 

		print ("Is the number higher or lower than " + guess + "?");
		print ("Up = higher | Down = lower | Return = equal");
	}

	void NextGuess() {
		//guess = (max + min) / 2; // binary search

		// a less efficient binary search to feel more human
		// Random.Range is inclusive on both ends so we tweak the min.
		double unadjustedGuess = Random.Range(min, max);
		//print ("Unadjusted guess: " + unadjustedGuess);
		guess = (int) unadjustedGuess; 

		print ("Higher or lower than " + guess + "?");
		print ("Up = higher | Down = lower | Return = equal");
	}

	// This is the most current way to clear the log entries from console programmaticallyq
	void ClearLog() {
		Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
		var type = assembly.GetType("UnityEditorInternal.LogEntries");
		MethodInfo method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}

	IEnumerator Wait2SecondsThenRestart() {
		yield return new WaitForSeconds (2.0f);
		StartGame ();
	}
}
