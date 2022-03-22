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
    public int charId;

    Image charImage;
    IEnumerator routine;

    static float speed = 1.0f;


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
    public void SetCharSprite(int char_Id)
    {
        sprite1 = sprites1[char_Id];
        sprite2 = sprites2[char_Id];
        charId = char_Id;


    }

    public void StartVanish()
    {
        StartCoroutine(Vanish());
    }


    // 文字が消える演出
    public IEnumerator Vanish()
    {
        Vector3 startScale = this.GetComponent<RectTransform>().localScale;
        Vector3 endScale = new Vector3(0.0f, 0.0f, 1.0f) ;
        Vector3 localScale = new Vector3();

        float tick = 0.0f;

        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(startScale, endScale, tick); // 線形補間

            this.GetComponent<RectTransform>().localScale = localScale;

            yield return null;
        }

        Destroy(this.gameObject);
    }


}
