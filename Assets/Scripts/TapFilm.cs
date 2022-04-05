using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapFilm : MonoBehaviour, IPointerClickHandler
{

    public GameManager gameManager;
    private int targetId;

    private void Awake()
    {
        targetId = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData data)
    {
        // gameManager.ShowBigPicture(targetId);
        gameManager.ShowAlbum();
     
    }
}
