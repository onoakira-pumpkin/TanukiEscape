using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject panelNameSpace;
    public GameManager gameManager;
    public GameObject charPrefab;
    public int targetId = 1;
    public int[] CharIdList;

    int targetNameLen;


    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetNameLen = CharIdList.Length;

        // ターゲットの名前表示
        ShowTargetName();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameManager.isDragPhoto)
        {
            SetCharImage();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DeleteCharImage();
    }


    public void SetCharImage()
    {
        panelNameSpace.SetActive(true);
    }

    public void DeleteCharImage()
    {
        panelNameSpace.SetActive(false);
    }


    public void ChangeCharImage()
    {
        print("ChangeCharImage");
        panelNameSpace.SetActive(false);
    }

    void ShowTargetName()
    {
        for (int i = 0; i < targetNameLen; i++)
        {
            GameObject charImage = Instantiate(charPrefab, panelNameSpace.transform);
            charImage.GetComponent<CharMove>().SetCharSprite(CharIdList[i]);
        }
        
    }
       
   
}
