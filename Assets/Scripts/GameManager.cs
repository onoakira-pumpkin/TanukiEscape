using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject canvasRoom;
    public Image imageBlackBack;
    public GameObject messagePrefab;
    public GameObject bigPhotoPrefab;

    public GameObject doa2;


    private RectTransform canvasRoomTransform;
    private GameObject message;
    private Dictionary<string, int> targetName2Id;
    private string[] Id2targetName;
    private BigPhotoManager bigPhotoManager;
    private GameObject bigPhoto;

    public bool isDragPhoto = false; // 写真をドラッグ中はture

    // Start is called before the first frame update
    void Start()
    {
        canvasRoomTransform = canvasRoom.GetComponent<RectTransform>();
        LoadTargetName2IdDic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadTargetName2IdDic()
    {
        targetName2Id = new Dictionary<string, int>();
        targetName2Id.Add("Tanuki", 1);
        targetName2Id.Add("Creature1", 2);
        targetName2Id.Add("Doa1", 3);

        Id2targetName = new string[] { "none", "Tanuki", "Creature1", "Doa1" };


    }

    // メッセージウィンドウの表示
    public void OpenMessage(string sentence)
    {
        Transform canvasRoomTransform = canvasRoom.GetComponent<Transform>();
        message = (GameObject)Instantiate(messagePrefab, canvasRoomTransform);
        message.GetComponent<MessageManager>().SetMessage(sentence);
    }


    public void TakePicture(string targetName)
    {
        // 被写体が辞書にない場合
        if (!targetName2Id.ContainsKey(targetName))
        {
            Debug.Log("<color=yellow>target name not in targetName2Id</color>");
            return;
        }

        // target_idの取得
        int target_id = targetName2Id[targetName];

        // 写真の表示
        ShowBigPicture(target_id);

        

        
    }


    // 写真の拡大表示
    public void ShowBigPicture(int targetId)
    {
        bigPhoto = Instantiate(bigPhotoPrefab, canvasRoomTransform);
        bigPhotoManager = bigPhoto.GetComponent<BigPhotoManager>();
    }

    public void SetBlackBack()
    {
        imageBlackBack.color = new Color32(0, 0, 0, 255);
    }

    public void SetRoomBack()
    {
        imageBlackBack.color = new Color32(0, 0, 0, 0);
    }



    // ターゲットを変化させるための処理
    public void ChangeTarget(int targetId, int pictureId, Vector3 targetLocalPosition)
    {
        print(targetId);
        print(pictureId);
        switch (targetId)
        {
            case 2:
                if (pictureId == 1)
                {
                    GameObject obj = Instantiate(doa2, canvasRoomTransform);
                    obj.GetComponent<RectTransform>().localPosition = targetLocalPosition;
                    Destroy(canvasRoomTransform.Find(Id2targetName[targetId]).gameObject);
                }
                break;

            default:

                break;
        }
    }



}
