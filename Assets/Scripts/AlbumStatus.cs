using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ----------------------------------------------------------//
//
//  Albumの状態を保存しておくclass
//  photoNum : photoの総数
//  isAddedPhoto : photo_idのphotoを所持しているかの
//                  bool値を返すbool[]
//
//-----------------------------------------------------------//


public class AlbumStatus
{
    public int photoNum;
    public bool[] isAddedPhoto = new bool[] { };

    public AlbumStatus(int PhotoNum)
    {
        this.photoNum = PhotoNum;
        this.isAddedPhoto = new bool[this.photoNum];

        for (int i = 0; i < this.photoNum; i ++)
        {
            this.isAddedPhoto[i] = false;
        }
    }

    // アルバムステータスの変更
    public void changeAlbumStatus(int PhotoId, bool isAdded)
    {
        this.isAddedPhoto[PhotoId] = isAdded;
    }

    // アルバムに存在するかどうか
    public bool existPhotoInAlbum(int PhotoId)
    {
        return this.isAddedPhoto[PhotoId];
    }


}
