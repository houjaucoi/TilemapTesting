//https://www.youtube.com/watch?v=YnwOoxtgZQI
//https://www.youtube.com/watch?v=b0AQg5ZTpac

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMoveXZ : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap obstacleTilemap;

    private Vector3 movement;                                          

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
        movement.z = Input.GetAxisRaw("Vertical") * offset;            

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
            Gizmos.DrawWireCube(transform.position + movement, new Vector3(2, 0, 2));
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + movement, new Vector3(2, 0, 2));
        }
    }

    private void Move(Vector3 direction)
    {
        if (CanMove(direction))
        {
            transform.position += direction;
        }
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3Int gridPos = groundTilemap.WorldToCell(transform.position + direction);
        
        if (!groundTilemap.HasTile(gridPos) || obstacleTilemap.HasTile(gridPos))
        {
            return false;
        }

        return true;
    }
}
