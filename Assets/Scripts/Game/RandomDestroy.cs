﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroy : MonoBehaviour {

    private float destroyTime = 11.5f;

    // Use this for initialization
    void Start () {

        InvokeRepeating("Destroy", destroyTime, Random.Range(destroyTime, 21.5f));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Destroy() {

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }
}
