﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMove : MonoBehaviour
{



	private Vector2 startTouchPosition, endTouchPosition;
	private Vector3 startModelPosition, endModelPosition;
	private float moveTime;
	private float moveDuration = 0.1f;

	public GameObject shipAscended;

	// positionMid = -0.0f
	private float positionLeft = 0.25f;
	private float positionRight = -0.25f;
	private float moveSpace = 0.25f;


	private bool isPlaying;
	public bool isTouchMode = false;

	bool clickable = false;

	// Update is called once per frame
	private void Start(){


	}

	private void Update()
	{
		//Updates bool variable isPlaying from "Controller" Tag - referenced in GameControl GO
		isPlaying = GameObject.FindGameObjectWithTag ("Controller").GetComponent<UserInterfaceButtons> ().Playing;

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			startTouchPosition = Input.GetTouch(0).position;

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			endTouchPosition = Input.GetTouch(0).position;


			//Check if swipe direction goes right
			if ((endTouchPosition.x < startTouchPosition.x) && ((transform.position.x <= positionLeft) && (transform.position.x > positionRight + 0.1f)))
			{
				//(transform.position.x >= positionRight) && (transform.position.x < positionLeft)
				if (isPlaying && isTouchMode && !clickable) {
					clickable = true;
					StartCoroutine (Move ("right"));
					Debug.Log ("right");

					if (shipAscended) {

						shipAscended.GetComponent<ShipAscend> ().RightShipAnim ();
					}
				}
			}

			//Check if swipe direction goes left
			if ((endTouchPosition.x > startTouchPosition.x) && ((transform.position.x >= positionRight) && (transform.position.x < positionLeft - 0.1f)))
			{                
				//(transform.position.x <= positionLeft) && (transform.position.x > positionRight)
				if (isPlaying && isTouchMode && !clickable) {
					clickable = true;
					StartCoroutine (Move ("left"));
					Debug.Log ("left");


					if (shipAscended) {

						shipAscended.GetComponent<ShipAscend> ().LeftShipAnim ();
					}
				}
			}
		}
	}

	private IEnumerator Move(string whereToMove)
	{
		switch (whereToMove)
		{

		//move model to the left
		case "left":
			moveTime = 0f;
			startModelPosition = transform.position;
			endModelPosition = new Vector3 (startModelPosition.x + moveSpace, transform.position.y, transform.position.z);

			while (moveTime < moveDuration) {
				moveTime += Time.deltaTime;
				transform.position = Vector2.Lerp
					(startModelPosition, endModelPosition, moveTime / moveDuration);
				
				yield return null;
			}

			clickable = false;

			break;


			//move model to the right
		case "right":
			
			moveTime = 0f;
			startModelPosition = transform.position;
			endModelPosition = new Vector3
				(startModelPosition.x - moveSpace, transform.position.y, transform.position.z);

			while (moveTime < moveDuration)
			{
				moveTime += Time.deltaTime;
				transform.position = Vector2.Lerp
					(startModelPosition, endModelPosition, moveTime / moveDuration);

				yield return null;
			}

			clickable = false;
			break;
		}

	}

	public void MoveRight()
	{
		if ((transform.position.x >= positionRight) && (transform.position.x < positionLeft - 0.1f) && !clickable) {
			clickable = true;
			StartCoroutine (Move ("left"));
		}


	}

	public void MoveLeft()
	{
		if ((transform.position.x <= positionLeft) && (transform.position.x > positionRight + 0.1f) && !clickable) {
			clickable = true;
			StartCoroutine (Move ("right"));
		}
	}
}
