using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // インスペクターから設定するオブジェクト
    public GameObject canvasRoom;
    public GameObject canvasUi;
    public Image imageBlackBack;
    public GameObject messagePrefab;
    public GameObject bigPhotoPrefab;
    public GameObject untouchPanel;
    public GameObject Album;
    public GameObject AlbumBase;
    public GameObject AlbumContent;

    // ターゲットプレハブ
    public GameObject doa;
    public GameObject nuki;

    // 参照
    private RectTransform canvasRoomTransform;
    private GameObject message;
    private Dictionary<string, int> targetName2Id;
    private string[] Id2targetName;
    private BigPhotoManager bigPhotoManager;
    private GameObject bigPhoto;

    // 変数
    public bool isDragPhoto = false; // 写真をドラッグ中はture
    public bool isUnknown = false;
    public AlbumStatus albumStatus;

    int PHOTONUM = 10;

    // Start is called before the first frame update
    void Start()
    {
        canvasRoomTransform = canvasRoom.GetComponent<RectTransform>();
        LoadTargetName2IdDic();

        albumStatus = new AlbumStatus(PHOTONUM);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ターゲット名称からidに変換する辞書をロード
    void LoadTargetName2IdDic()
    {
        targetName2Id = new Dictionary<string, int>();
        targetName2Id.Add("none", 0);
        targetName2Id.Add("Tanuki", 1);
        targetName2Id.Add("Creature1", 2);
        targetName2Id.Add("Doa", 3);
        targetName2Id.Add("Nuki", 4);


        Id2targetName = new string[] { "none", "Tanuki", "Creature1", "Doa", "Nuki" };


    }

    // メッセージウィンドウの表示
    public void OpenMessage(string sentence)
    {
        Transform canvasUITransform = canvasUi.GetComponent<Transform>();
        message = (GameObject)Instantiate(messagePrefab, canvasUITransform);
        message.GetComponent<MessageManager>().SetMessage(sentence);
    }

    // 写真撮影
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
        if (albumStatus.existPhotoInAlbum(target_id) == false)
        {
            albumStatus.changeAlbumStatus(target_id, true);
            Debug.Log("<color=green>target is added to Album </color>");
        }
        else
        {
            Debug.Log("<color=green>target has been added to Album. Nothing is changed. </color>");
        }
       

        // 写真の表示
        ShowBigPicture(target_id);
        
    }


    // 写真の拡大表示
    public void ShowBigPicture(int targetId)
    {
        bigPhoto = Instantiate(bigPhotoPrefab, canvasRoomTransform);
        bigPhotoManager = bigPhoto.GetComponent<BigPhotoManager>();
    }


    // 背景を黒に
    public void SetBlackBack()
    {
        imageBlackBack.color = new Color32(0, 0, 0, 255);
    }

    // 背景を部屋に
    public void SetRoomBack()
    {
        imageBlackBack.color = new Color32(0, 0, 0, 0);
    }



    // ----------------------------------------------------------//
    //
    //
    //                  Album handle
    //
    //
    //-----------------------------------------------------------//

    // アルバムの表示

    public void ShowAlbum()
    {
        MakeAlbumcontents();
        Album.SetActive(true);
        PrintAlbumStatus();
    }

    // アルバムの非表示
    public void DeleteAlbum()
    {
        Album.SetActive(false);
    }

    // アルバムの更新
    public void SetPhoto2Album(int PhotoId, bool isAdded)
    {
        albumStatus.changeAlbumStatus(PhotoId, isAdded);
    }

    // アルバムに登録

    // アルバムから全要素削除
    void CleanPhoto()
    {
        foreach (Transform n in AlbumBase.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }
    

// アルバム表示の作成
void MakeAlbumcontents()
    {
        CleanPhoto();

        for (int pId = 1; pId < albumStatus.photoNum; pId ++)
        {
            GameObject albumContent = Instantiate(AlbumContent, AlbumBase.transform);

            if (albumStatus.existPhotoInAlbum(pId))
            {
                albumContent.GetComponent<AlbumContent>().AlbumContentSelect(pId, true);
            }
            else
            {
                albumContent.GetComponent<AlbumContent>().AlbumContentSelect(0, false);
                // albumContent.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            }
        }
    }

    // アルバム情報のデバッグ表示
    public void PrintAlbumStatus()
    {
        string _s = "";
        foreach (bool isAdded in albumStatus.isAddedPhoto)
        {
            _s += isAdded.ToString() + " ";
        }

        print(_s);
    }


    // ----------------------------------------------------------//
    //
    //
    //                  change target
    //
    //
    //-----------------------------------------------------------//



    // ターゲットを変化させるための処理
    public void ChangeTarget(int targetId, int pictureId, Vector3 targetLocalPosition)
    {
        print(targetId);
        print(pictureId);

        GameObject targetObj = canvasRoomTransform.Find(Id2targetName[targetId]).gameObject;

        switch (pictureId)
        {
            case 1:
                switch (targetId)
                {
                    case 1:
                        StartCoroutine(Object2Object(targetObj, nuki, 16));
                        break;

                    case 2:
                        StartCoroutine(Object2Object(targetObj, doa, 16));
                        break;

                    default:
                        SetRoomBack();
                        targetObj.GetComponent<TargetManager>().DeleteCharImage();
                        break;
                }
                break;

            default:
                SetRoomBack();

                break;
        }
    }


    IEnumerator Object2Object(GameObject targetObj, GameObject afterObj, int charId)
    {
        untouchPanel.SetActive(true);
        canvasUi.SetActive(false);
        targetObj.GetComponent<TargetManager>().SetCharImage();
        yield return StartCoroutine(targetObj.GetComponent<TargetManager>().CharVanish(charId));
        yield return StartCoroutine(targetObj.GetComponent<TargetManager>().VanishSprite());
        afterObj.SetActive(true);
        untouchPanel.SetActive(false);
        SetRoomBack();
        canvasUi.SetActive(true);
    }



    // ----------------------------------------------------------//
    //
    //
    //                  click event
    //
    //
    //-----------------------------------------------------------//


    public void ClickEvent(int targetId)
    {
        switch(targetId)
        {
            case 1: // tanuki
                OpenMessage("たぬき だ。");
                break;

            case 2: // creature1
                if (isUnknown)
                {
                    OpenMessage("何かは わからない。");
                }
                else
                {
                    OpenMessage("こいつは どたたあた だ。");
                }
                break;

            case 3: // doa
                OpenMessage("ドアを開けますか？");
                break;

            case 4: // nuki
                if (isUnknown)
                {
                    OpenMessage("楽しそうだ。");
                }
                else
                {
                    OpenMessage("こいつは ぬき だ。");
                }
                break;

            default:
                break;
        }
    }

}
