using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject panelNameSpace;
    public GameManager gameManager;
    public GameObject charPrefab;
    public List<GameObject> charImageList;
    public int targetId = 1;
    public int[] CharIdList;


    int targetNameLen;
    float vanishSpeed;


    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        charImageList = new List<GameObject>();

        targetNameLen = CharIdList.Length;
        vanishSpeed = 0.7f;

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
        if (gameManager.isDragPhoto)
        {
            DeleteCharImage();
        }
        
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

    public void VanishTargetSprite()
    {
        StartCoroutine(VanishSprite());
    }

    public IEnumerator VanishSprite()
    {
        float tick = 0.0f;

        while (tick < 1.0f)
        {
            tick += Time.deltaTime * vanishSpeed;

            this.GetComponent<Image>().color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 1.0f), new Color(1.0f, 1.0f, 1.0f, 0.0f), tick);

            yield return null;
        }

        Destroy(this.gameObject);
    }


    void ShowTargetName()
    {
        for (int i = 0; i < targetNameLen; i++)
        {
            GameObject charImage = Instantiate(charPrefab, panelNameSpace.transform);
            charImage.GetComponent<CharMove>().SetCharSprite(CharIdList[i]);
            charImageList.Add(charImage);
        }
        
    }



    public IEnumerator CharVanish(int charId)
    {
        charImageList = GetComponent<TargetManager>().charImageList;
        CharIdList = GetComponent<TargetManager>().CharIdList;

        for (int i = 0; i < targetNameLen; i++)
        {

            if (CharIdList[i] == charId)
            {
                yield return StartCoroutine(charImageList[i].GetComponent<CharMove>().Vanish());
            }
        }

        yield return null;
    }


}
