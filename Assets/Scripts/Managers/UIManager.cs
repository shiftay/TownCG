﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Sprite[] arrows;
	public Color[] colors;
	public GameObject mainUI;
	public GameObject TurnOVERUI;
	public GameObject EventUI;
	public RNGEvents evnt;
	public CardManager cm;
	public TurnOVER over;
	public List<string> titles;
	public Sprite[] newsPaperImgs;
	
	// Update is called once per frame
	void Update () {
		
	}


	public void TurnOVER() {
		mainUI.SetActive(false);
		TurnOVERUI.SetActive(true);
		EventUI.SetActive(false);
		for(int i = 0; i < over.cardsPlayed.Length; i++) {
			over.cardsPlayed[i].sprite = newsPaperImgs[GameManager.instance.turnCardPlayed[i]];
			over.imageTitles[i].text = ImgTitle(GameManager.instance.turnCardPlayed[i]);
		}

		GameManager.instance.CalculateTurn();

		over.summaryInfo[0].text = "Workers: \t\t\t" + GameManager.instance.populationVal.ToString();
		over.summaryInfo[1].text = "Happiness: \t\t" + GameManager.instance.happinessVal.ToString();
		over.summaryInfo[2].text = "Objective: \t\t\t" + GameManager.instance.objectiveVal.ToString();

		over.tMods[0].text = (GameManager.instance.populationVal - GameManager.instance.prevPop).ToString();
		over.modifiers[0].sprite = arrows[ArrowMod(GameManager.instance.populationVal, GameManager.instance.prevPop)];
		over.modifiers[0].color = colors[ArrowMod(GameManager.instance.populationVal, GameManager.instance.prevPop)];

		over.tMods[1].text = (GameManager.instance.happinessVal - GameManager.instance.prevHapp).ToString();
		over.modifiers[1].sprite = arrows[ArrowMod(GameManager.instance.happinessVal, GameManager.instance.prevHapp)];
		over.modifiers[1].color = colors[ArrowMod(GameManager.instance.happinessVal, GameManager.instance.prevHapp)];

		over.tMods[2].text = (GameManager.instance.objectiveVal - GameManager.instance.prevObjec).ToString();
		over.modifiers[2].sprite = arrows[ArrowMod(GameManager.instance.objectiveVal, GameManager.instance.prevObjec)];
		over.modifiers[2].color = colors[ArrowMod(GameManager.instance.objectiveVal, GameManager.instance.prevObjec)];

		over.title.text = titles[titleChoice()];

	}


	public void EventActivate() {
		mainUI.SetActive(false);
		EventUI.SetActive(true);
		TurnOVERUI.SetActive(false);
	}

	public void TurnStart() {
		mainUI.SetActive(true);
		EventUI.SetActive(false);
		TurnOVERUI.SetActive(false);
	}


	public int ArrowMod(int x, int y) {
		int retVal = -1;

		if(x > y) {
			retVal = 0;
		} else if (x < y) {
			retVal = 1;
		} else {
			retVal = 2;
		}



		return retVal;
	}



	int titleChoice() {
		int retVal = -1;

		if(GameManager.instance.currentTurn < 3) {
			retVal = 0;
		}


		while(retVal == -1){
			switch(Random.Range(0,3)) {
				case 0:
					if(GameManager.instance.populationVal > GameManager.instance.prevPop) {
						retVal = 5;
					}
					break;
				
				case 1:
					if (GameManager.instance.objectiveVal > GameManager.instance.prevObjec ) {
						retVal = 1;
					} else if ( GameManager.instance.objectiveVal == GameManager.instance.prevObjec) {
						retVal = 3;
					}

					break;

				case 2:
					if (GameManager.instance.happinessVal < GameManager.instance.populationVal) {
						retVal = 4;
					} else if (GameManager.instance.happinessVal > GameManager.instance.populationVal) {
						retVal = 3;
					}
					break;
			}
		}

		return retVal;
	}


	string ImgTitle(int num) {
		string retVal = "";

		switch(GameManager.instance.cardData[num].TYPE()) {
			case TILETYPE.COMMERCIAL:
				retVal = "Attractions opening up.";
				break;

			case TILETYPE.RESIDENTIAL:
				retVal = "Neighbours moving in.";
				break;

			case TILETYPE.INDUSTRIAL:
				retVal = "More Jobs coming.";
				break;
		}



		return retVal;
	}

}