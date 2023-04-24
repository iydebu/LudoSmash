using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Pawn> pawns;
    public int currentPlayerID;
    public bool isPlayerMoving;
    public TextMeshProUGUI diceText;
    public TextMeshProUGUI playerText;
    public FadeEffect fade;
    public bool isDebugON;

    // Method to roll the dice and move the current player's pawn
    public void RollDiceAndMove()
    {
        if (!isPlayerMoving)
        {
            int steps;
            if (isDebugON) steps = 35;
            else steps = Random.Range(1, 7);

            Pawn currentPawn = pawns[currentPlayerID];
            currentPawn.Move(steps);
            diceText.text = steps.ToString();

            // Wait for the pawn to finish moving before switching to the next player
            StartCoroutine(WaitForPawnToFinishMoving(currentPawn));
        }
    }

    // Method to handle game over
    public void GameOver(int player)
    {
        playerText.text = "Player " + (player + 1) + " Wins!";
        StartCoroutine(waitForWin());
        
    }

    // Coroutine to display the winner and fade out the scene
    private IEnumerator waitForWin()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("fade");
        fade.FadeOut();
    }

    // Coroutine to wait for the pawn to finish moving and switch to the next player
    private IEnumerator WaitForPawnToFinishMoving(Pawn currentPawn)
    {
        yield return new WaitForSeconds(1); // Wait for a short duration before checking the pawn's movement

        // Check if the current pawn is still moving
        Transform currentTileTransform = currentPawn.tileArray[currentPawn.currentTile];
        while (Vector3.Distance(currentPawn.transform.position, currentTileTransform.position) > 0.01f)
        {
            yield return null;
        }

        // Switch to the next player
        currentPlayerID = (currentPlayerID + 1) % pawns.Count;
        playerText.text = "Player Turn: " + (currentPlayerID + 1);
    }
}
