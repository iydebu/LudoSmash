using System.Collections;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public int playerID;
    public int currentTile;
    public int maxTileCount;
    public Transform[] tileArray;
    public float moveSpeed = 2f;
    public float jumpHeight = 0.5f;
    public GameManager manager;

    // Method to move the pawn
    public void Move(int steps)
    {
        // Return if moving would go beyond the maximum tile count
        if (currentTile + steps >= maxTileCount)
        {
            return;
        }

        StartCoroutine(MoveSteps(steps));
    }

    // Method to check if the game is over
    private void checkGameOver()
    {
        if (currentTile == maxTileCount - 1)
        {
            Debug.Log("GameOver");
            manager.GameOver(playerID);
        }
    }

    // Coroutine to move the pawn in steps
    private IEnumerator MoveSteps(int steps)
    {
        manager.isPlayerMoving = true;
        for (int i = 0; i < steps; i++)
        {
            currentTile++;
            Transform targetTile = tileArray[currentTile];

            Vector3 startPosition = transform.position;
            Vector3 endPosition = targetTile.position;
            Vector3 jumpPeak = new Vector3(endPosition.x, endPosition.y + jumpHeight, endPosition.z);
            float halfWayTime = moveSpeed / 2;

            // Move up to the jump peak
            float elapsedTime = 0;
            while (elapsedTime < halfWayTime)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, jumpPeak, elapsedTime / halfWayTime);
                yield return null;
            }

            // Move down to the target position
            elapsedTime = 0;
            while (elapsedTime < halfWayTime)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(jumpPeak, endPosition, elapsedTime / halfWayTime);
                yield return null;
            }

            // Set the final position
            transform.position = endPosition;
            // Check for game over
            checkGameOver();
        }
        manager.isPlayerMoving = false;
    }
}
