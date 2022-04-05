using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAlbumContent : MonoBehaviour
{
    GameManager gameManager;

    public void TappingAlbumContent(int picture_id)
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.DeleteAlbum();
    }
}
