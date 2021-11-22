using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorkBoardController : MonoBehaviour
{
	[SerializeField] private GameObject[] corkBoards;
	[SerializeField] private float rotationDuration;

	private float rotationAmount = 180f;

	private bool rotatedClockWise = false;

	private Quaternion targetRotation;
	private Quaternion startingRotation;

	private float timer = 0;

	private bool rotating = false;
	private bool rotatingBack = false;

	public bool Rotating
    {
		get 
		{ 
			return rotating; 
		}
    }

	private void Start()
	{
		MenuHandler.RotateBoards += RotateBoard;
		NameHandler.RotateBoards += RotateBoard;
	}

    private void Update()
    {
        if(rotating)
        {
			timer += Time.deltaTime / rotationDuration;
			transform.rotation = Quaternion.Slerp(startingRotation, targetRotation ,timer);
			if(timer >= 1)
            {
				if(rotatingBack)
                {
					rotatingBack = false;
					DeactivateAll();
                }
				rotating = false;
				timer = 0;
            }
        }
    }

    private void RotateBoard(string board)
	{
		Vector3 target = transform.eulerAngles;
		startingRotation = transform.rotation;
		switch (board)
		{
			case "Back":
				// rotate backwards to show main menu again.
				target.y = 0;
				targetRotation = Quaternion.Euler(target);
				rotating = true;
				rotatingBack = true;
				break;

			case "Settings":
				target.y = 180;
				targetRotation = Quaternion.Euler(target);
				rotating = true;
				ActivateBoard(0);
				// set settings board to be active 
				// rotate towards it
				break;

			case "HowTo":
				target.y = 180;
				targetRotation = Quaternion.Euler(target);
				rotating = true;
				ActivateBoard(1);
				// set How to board to be active 
				// rotate towards it
				break;

			case "Credits":
				target.y = 180;
				targetRotation = Quaternion.Euler(target);
				rotating = true;
				ActivateBoard(2);
				// set Credits board to be active 
				// rotate towards it
				break;

			case "Name":
				target.y = 180;
				targetRotation = Quaternion.Euler(target);
				rotating = true;
				ActivateBoard(3);
				// set Name board to be active 
				// rotate towards it
				break;

			default:
				Debug.LogWarning(board + " Was not found in the swithc!");
				break;
		}
    }

	private void DeactivateAll()
    {
        foreach (var board in corkBoards)
        {
			board.SetActive(false);
        }
    }
	private void ActivateBoard(int index)
    {
		corkBoards[index].SetActive(true);
	}

    private void OnDestroy()
    {
		MenuHandler.RotateBoards -= RotateBoard;
		NameHandler.RotateBoards -= RotateBoard;
	}
}
