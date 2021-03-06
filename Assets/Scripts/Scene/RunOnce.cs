﻿//attached on canvas
//make splashscreen run once and intropanel run once too lol

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOnce : MonoBehaviour {

    public GameObject howToPopPanel;
	public GameObject unlockPanel;
	Animator howToPopAnimz;
	Animator unlockPanelAnim;

	public GameObject Splash;

	// Use this for initialization
	void Start () {
		
		//run intro-instruction panel once in a lifetime
        if (!PlayerPrefs.HasKey("isFirstTime") || PlayerPrefs.GetInt("isFirstTime") != 1)
        {
			howToPopAnimz = howToPopPanel.GetComponent<Animator> ();

			howToPopAnimz.Play ("Panel In");
            PlayerPrefs.SetInt("isFirstTime", 1);
            PlayerPrefs.Save();
        }


		//splash once per session
		if (PlayerPrefs.GetInt("firstSplash") != 1)
		{
			Splash.SetActive(true);
			PlayerPrefs.SetInt("firstSplash", 1);
			PlayerPrefs.Save();
		}

		unlockPanelAnim = unlockPanel.GetComponent<Animator> ();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("firstSplash", 0);
		PlayerPrefs.SetInt("showUnlockInfo", 0);
	}

	public void UnlockInfo(){
		
		if (PlayerPrefs.GetInt ("starTotal") < 500) {
			
			if (!PlayerPrefs.HasKey ("showUnlockInfo") || PlayerPrefs.GetInt ("showUnlockInfo") != 1) {


				if (!PlayerPrefs.HasKey ("unlockToggle") || PlayerPrefs.GetInt ("unlockToggle") < 5) {
		
					int count;
					count = PlayerPrefs.GetInt ("unlockToggle");
					unlockPanelAnim.Play ("Panel In");
					PlayerPrefs.SetInt ("unlockToggle", count + 1);
					PlayerPrefs.Save ();

				}

				PlayerPrefs.SetInt ("showUnlockInfo", 1);
			}
		}
	}
}
