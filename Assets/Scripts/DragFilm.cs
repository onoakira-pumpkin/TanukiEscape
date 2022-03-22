using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragFilm : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Vector2 prevPos; //保存しておく初期position
    private RectTransform rectTransform; // 移動したいオブジェクトのRectTransform
    private RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform

    public GameObject flashEffect;

    public GameManager gameManager;

    public int pictureId = 0;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
    }


    // ドラッグ開始時の処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        // ドラッグ前の位置を記憶しておく
        // RectTransformの場合はpositionではなくanchoredPositionを使う
        prevPos = rectTransform.anchoredPosition;
        gameManager.isDragPhoto = true;

    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        // eventData.positionから、親に従うlocalPositionへの変換を行う
        // オブジェクトの位置をlocalPositionに変更する
        Vector2 localPosition = GetLocalPosition(eventData.position);
        rectTransform.anchoredPosition = localPosition;

        // 追従する写真にはRaycastTargetしない
        GetComponent<Image>().raycastTarget = false;

        // 背景を暗くする
        gameManager.SetBlackBack();

    }

    // ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData)
    {

        Debug.Log("PictureOnDrop");

        rectTransform.anchoredPosition = prevPos;
       
        var raycastResults = new List<RaycastResult>();
        bool _flag = false;

        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("Target"))
            {
                string targetName = hit.gameObject.name;
                Debug.Log("picuture shotting : " + targetName);
                TargetManager targetManager = hit.gameObject.GetComponent<TargetManager>();
                gameManager.ChangeTarget(targetManager.targetId, pictureId, hit.gameObject.GetComponent<RectTransform>().localPosition);
                _flag = true;
            }

        }

        if (!_flag)
        {
            gameManager.SetRoomBack();
        }

        // Raycastを戻す
        GetComponent<Image>().raycastTarget = true;
    
        gameManager.isDragPhoto = false;


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
