using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMoveTesting : MonoBehaviour
{
    public Tilemap[] obstacle;
    public Tilemap[] ground;
    float horizontalInput;
    float verticalInput;
    private Vector3 targetPosition;
    private Vector3Int gridPosition;
    private bool canMove = true;

    private float timer = 0f;

    private void Update()
    {
        //ControllerPlayer();

        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            ControllerPlayer();
            timer = 0f;
        }
    }

    private void ControllerPlayer() 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 && verticalInput == 0)
        {
            Move(new Vector3(horizontalInput * 2, 0f, 0f));
        }

        if (horizontalInput == 0 && verticalInput != 0)
        {
            Move(new Vector3(0f, 0f, verticalInput * 2));
        }
    }

    private void Move(Vector3 direction)
    {
        if (CanMove(direction))
        {
            Debug.Log("Can move" + direction);
            //transform.position += direction.normalized;
            transform.position += direction;
        }
    }

    private bool CanMove(Vector3 direction) 
    {
        targetPosition = new Vector3(transform.position.x + direction.x, 0, transform.position.z + direction.z);
        gridPosition = ground[0].WorldToCell(targetPosition);

        if (ground[0].HasTile(gridPosition))
        {
            Debug.Log(ground[0].GetTile(gridPosition).name);
        }

        //Debug.LogWarning($"playerPos: {transform.position}, dir: {direction}, targetPosition: {targetPosition}, gridPos: {gridPosition}");

        canMove = ground[0].HasTile(gridPosition) && !obstacle[0].HasTile(gridPosition);

        if (!canMove)
        {
            Debug.Log($"Can't move => gridPosition: {gridPosition}");

            Debug.LogWarning($"playerPos: {transform.position}, dir: {direction}, targetPosition: {targetPosition}, gridPos: {gridPosition}");

            if (obstacle[0].HasTile(gridPosition))
            {
                Debug.Log(obstacle[0].GetTile(gridPosition).name);
            }
        }

        return canMove;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2, 0, 2));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(targetPosition, new Vector3(2, 0, 2));
      
        if (!canMove)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(gridPosition, new Vector3(2, 0, 2));

            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(ground[0].GetCellCenterWorld(gridPosition), new Vector3(1, 0, 1));
            //Debug.Log($"gridPosition {gridPosition}");
        }
    }
}
