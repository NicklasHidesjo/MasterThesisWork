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

			transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, timer);

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
		if(rotating)
        {
			return;
        }

		startingRotation = transform.rotation;

		switch (board)
		{
            case "Back":
                GoBackToMain();
                break;

            case "Settings":
				HandleActivatingBoard(0);
				break;

            case "HowTo":
                HandleActivatingBoard(1);
                break;

            case "Credits":
				HandleActivatingBoard(2);
				break;

			case "Name":
				HandleActivatingBoard(3);
				break;

			default:
				Debug.LogWarning(board + " Was not found in the switch!");
				break;
		}
    }

    private void GoBackToMain()
    {
		Vector3 target = transform.eulerAngles;
		target.y = 0;
        targetRotation = Quaternion.Euler(target);
        rotating = true;
        rotatingBack = true;
    }

    private void HandleActivatingBoard(int index)
    {
		Vector3 target = transform.eulerAngles;
		target.y = 180 * (Random.Range(0, 2) * 2 - 1);
        targetRotation = Quaternion.Euler(target);
        rotating = true;
        ActivateBoard(index);
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
