using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    private void ChangeTile(Vector2 vec)
    {
        
        var cellPos = new Vector3Int((int)vec.x, (int)vec.y);
        Debug.Log(cellPos);
        GetComponent<Tilemap>().SetTileFlags(cellPos, TileFlags.None);
        GetComponent<Tilemap>().SetColor(cellPos, Color.black);
    }

    #region EVENTS REGISTER
    private void OnEnable()
    {
        RegisterEvent();
    }
    public void RegisterEvent()
    {
        PlayerManager.OnMove += ChangeTile;
    }
    private void OnDisable()
    {
        UnregisterEvent();
    }
    public void UnregisterEvent()
    {
        PlayerManager.OnMove += ChangeTile;
    }
    #endregion
}
