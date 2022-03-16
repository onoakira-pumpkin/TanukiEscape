using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBigPhoto : MonoBehaviour
{

    public GameObject imageBigPhotoContent;
    public GameObject textBigPhoto;
    public GameObject imageBigPhoto;
    public float speed;

    private RectTransform imageBigPhotoContentTransform;
    private RectTransform textBigPhotoTransform;
    private RectTransform imageBigPhotoTransform;

    private void Awake()
    {
        imageBigPhotoContentTransform = imageBigPhotoContent.GetComponent<RectTransform>();
        textBigPhotoTransform = textBigPhoto.GetComponent<RectTransform>();
        imageBigPhotoTransform = imageBigPhoto.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 4.0f;

    }

    // コルーチンの開始
    public void StartSwap()
    {
        StartCoroutine(Swap());
    }

    // 写真を回転させる
    IEnumerator Swap()
    {
        float tick = 0f;

        Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 endScale = new Vector3(0f, 1.0f, 1.0f);
        Vector3 localScale = new Vector3(0f, 0f, 0f);

        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(startScale, endScale, tick);

            imageBigPhotoContentTransform.localScale = localScale;
            textBigPhotoTransform.localScale = localScale;
            imageBigPhotoTransform.localScale = localScale;

            yield return null;
        }

        this.GetComponent<BigPhotoManager>().ChangeBigPhoto();

        tick = 0f;
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * speed;

            localScale = Vector3.Lerp(endScale, startScale, tick);

            imageBigPhotoContentTransform.localScale = localScale;
            textBigPhotoTransform.localScale = localScale;
            imageBigPhotoTransform.localScale = localScale;

            yield return null;
        }



    }
}
