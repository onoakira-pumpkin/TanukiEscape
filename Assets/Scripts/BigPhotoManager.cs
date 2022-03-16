using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigPhotoManager : MonoBehaviour
{

    public GameObject imageBigPhotoContent;
    public Text textBigPhoto;
    public GameObject buttonBackFromBigPhoto;

    private bool isFront;

    private RectTransform imageBigPhotoContentTransform;
    private RectTransform textBigPhotoTransform;
    private RectTransform imageBigPhotoTransform;


    private void Awake()
    {
        imageBigPhotoContentTransform = imageBigPhotoContent.GetComponent<RectTransform>();
        textBigPhotoTransform = textBigPhoto.GetComponent<RectTransform>();
        imageBigPhotoTransform = this.GetComponent<RectTransform>();

    }

    // Start is called before the first frame update
    void Start()
    {
        isFront = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // 写真を閉じるボタンの押された時の処理
    public void OnTapButtonBackFromBigPhoto()
    {
        Destroy(this.gameObject);
    }


    public void ChangeBigPhoto()
    {
        isFront = !isFront;

        if (isFront)
        {
            imageBigPhotoContent.SetActive(true);
            textBigPhoto.text = "たぬき";
        }
        else
        {
            imageBigPhotoContent.SetActive(false);
            textBigPhoto.text = "裏面";
        }
    }
}
