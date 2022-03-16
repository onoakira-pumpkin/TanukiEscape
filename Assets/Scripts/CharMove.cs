using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharMove : MonoBehaviour
{
    public Sprite[] sprites1;
    public Sprite[] sprites2;

    public Sprite sprite1;
    public Sprite sprite2;

    Image charImage;
    IEnumerator routine;


    private void Awake()
    {
        charImage = GetComponent<Image>();
    }


    // Start is called before the first frame update
    void Start()
    {
        routine = CharMoving();
        StartCoroutine(CharMoving());
    }

    void OnEnable()
    {
        if (routine != null)
        {
            StartCoroutine(routine);
        }
    }


    // 文字が動くコルーチン
    IEnumerator CharMoving()
    {
        while(true)
        {
            charImage.sprite = sprite1;

            yield return new WaitForSeconds(0.5f);

            charImage.sprite = sprite2;

            yield return new WaitForSeconds(0.5f);
        }
 


    }


    // Idの文字をspriteに設定する
    public void SetCharSprite(int charId)
    {
        sprite1 = sprites1[charId];
        sprite2 = sprites2[charId];

    }


}
