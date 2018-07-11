﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum STATES { MAINMENU, PREGAME, COLLECTION, GAME, OPTIONS}

public class Deck {
	public List<int> cards = new List<int>();
	public string name;
	public int imageNumber;

	public void SetDeck(List<int> decks, string name, int img) {
		cards = decks;
		this.name = name;
		imageNumber = img;
	}
}

public class SavedGame {
	public List<int> tileSpace = new List<int>();

	public int objectiveVal, populationVal, happinessVal;
	public List<int> prevObjective = new List<int>();
	public List<int> prevPopulation = new List<int>();
	public List<int> prevHappiness = new List<int>();
}

public class BackEndManager : MonoBehaviour {

	public static BackEndManager instance;
	string SAVEPATH = "decks";
	string GAMEPATH = "lastGame";
	char DELIMITER = '/';
	public List<Deck> decks = new List<Deck>();
	public int currentState;
	public int prvState;
	public List<GameObject> states = new List<GameObject>();
	public bool deleteFiles = false;
	public SavedGame sGame;

	public bool editDeck = false;
	public int deckToEdit = 0;
	// Use this for initialization
	void Start () {
		instance = this;

		for(int i = 0; i < transform.childCount; i++) {
			states.Add(transform.GetChild(i).gameObject);
		}

		if(deleteFiles) {
			ClearFiles();
		} else {
			ReadDecks();
		}
	}
	
	public void ChangeState(STATES state) {
		if(state == STATES.OPTIONS) {
			prvState = currentState;
			currentState = (int)state;
			states[prvState].SetActive(true);
			states[currentState].SetActive(true);
		} else {
			prvState = currentState;
			currentState = (int)state;
			states[prvState].SetActive(false);
			states[currentState].SetActive(true);
		}

		// prvState = currentState;
		// currentState = (int)state;
		// states[prvState].SetActive(false);
		// states[currentState].SetActive(true);
	}

	void ReadDecks() {
		if(File.Exists(Application.persistentDataPath + SAVEPATH)) {
			StreamReader sr = new StreamReader(Application.persistentDataPath + SAVEPATH);

			string line;

			while((line = sr.ReadLine()) != null) {
				string[] split = Encryption(line).Split(DELIMITER);
				Deck temp = new Deck();
				List<int> deck = new List<int>();

				for(int i = 2; i < split.Length; i++) {
					deck.Add(int.Parse(split[i]));
				}

				temp.SetDeck(deck, split[0], int.Parse(split[1]));

				decks.Add(temp);
			}

			sr.Close();
		} else {
			FileStream sr = File.Open(Application.persistentDataPath + SAVEPATH, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			sr.Close();
		}
	}

	void SaveDecks() {
		StreamWriter test = new StreamWriter(Application.persistentDataPath + SAVEPATH, false);

		for (int i = 0; i < decks.Count; i++){
			test.WriteLine(Encryption(createDeckString(decks[i])));
		}

		test.Close();
	}

	string createDeckString(Deck temp) {
		string retVal = "";

		retVal += temp.name + DELIMITER + temp.imageNumber;

		for(int i = 0; i < temp.cards.Count; i++) {
			retVal += DELIMITER + temp.cards[i];
		}

		return retVal;
	}


	void OnApplicationPause(bool pauseStatus)	{
		//TODO: Save the game.
		//		Save the decks.
	}

// ============= BACK END UTILITIES ====================
	string Encryption(string input) {
		string s1 = "!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
		string s2 = "PQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNO";

		string ret = "";

		for (int i = 0; i < input.Length; i++) {
			ret += s2[s1.IndexOf(input.ToCharArray()[i])];
		}

		return ret;
	}
	void ClearFiles() {
		File.Delete(Application.persistentDataPath + GAMEPATH);
		File.Delete(Application.persistentDataPath + SAVEPATH);
	}
// ============= BACK END UTILITIES ====================
}
