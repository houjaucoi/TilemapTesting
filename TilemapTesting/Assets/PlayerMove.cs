//https://www.youtube.com/watch?v=YnwOoxtgZQI
//https://www.youtube.com/watch?v=b0AQg5ZTpac

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap obstacleTilemap;

    private Vector2 movement;
    private int offset = 2; //Grid's Cell Size
    private float timer = 0f;

    // Update is called once per frame
    private void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        //Move(movement);

        timer += Time.deltaTime;
        movement.x = Input.GetAxisRaw("Horizontal") * offset;
        movement.y = Input.GetAxisRaw("Vertical") * offset;

        if (timer >= 0.5f)
        {
            Move(movement);
            timer = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        if (CanMove(movement)) 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + (Vector3)movement, new Vector3(2, 2, 0));
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + (Vector3)movement, new Vector3(2, 2, 0));
        }
    }

    private void Move(Vector2 direction) 
    {
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction;
        }
    }

    private bool CanMove(Vector2 direction) 
    {
        Vector3Int gridPos = groundTilemap.WorldToCell(transform.position + (Vector3)direction);

        if (!groundTilemap.HasTile(gridPos) || obstacleTilemap.HasTile(gridPos))
        {
            return false;
        }

        return true;
    }
}
