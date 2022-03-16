using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPhoto : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Vector2 prevPos; //保存しておく初期position
    private RectTransform rectTransform; // 移動したいオブジェクトのRectTransform
    private RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform

    public GameObject imageAim;
    public GameObject flashEffect;
    public GameObject panelPhotoAreaPrefab;

    public GameManager gameManager;
    public AudioClip audioShutter;
    public AudioClip audioCameraRunning;

    private GameObject aim;
    private GameObject panelPhotoArea;
    private RectTransform aimRectTransform;
    private AudioSource audioSource;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
        audioSource = GetComponent<AudioSource>();
    }


    // ドラッグ開始時の処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        // ドラッグ前の位置を記憶しておく
        // RectTransformの場合はpositionではなくanchoredPositionを使う
        prevPos = rectTransform.anchoredPosition;


        // aimのinstance化
        aim = (GameObject)Instantiate(imageAim, parentRectTransform);
        aimRectTransform = aim.GetComponent<RectTransform>();
        panelPhotoArea = (GameObject)Instantiate(panelPhotoAreaPrefab, parentRectTransform);

        audioSource.clip = audioCameraRunning;
        audioSource.loop = true;
        audioSource.Play();


    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        // eventData.positionから、親に従うlocalPositionへの変換を行う
        // オブジェクトの位置をlocalPositionに変更する
        Vector2 localPosition = GetLocalPosition(eventData.position);
        aimRectTransform.anchoredPosition = localPosition;
    }

    // ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData)
    {
        // aimを削除する
        Destroy(aim.gameObject);

        Debug.Log("OnDrop");

        // エイム表示の終了
        Destroy(panelPhotoArea.gameObject);

        // 音楽停止
        audioSource.Stop();

       

        var raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("Target"))
            {
                string targetName = hit.gameObject.name;
                Debug.Log("shotting : " + targetName);
                ShotPictureFlashEffect();
                gameManager.TakePicture(targetName);


                // 音楽
                audioSource.PlayOneShot(audioShutter);

            }

        }
    }

    private void ShotPictureFlashEffect()
    {
        Instantiate(flashEffect, parentRectTransform);
    }






    // ScreenPositionからlocalPositionへの変換関数
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;

        // screenPositionを親の座標系(parentRectTransform)に対応するよう変換する.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);

        return result;
    }

}
