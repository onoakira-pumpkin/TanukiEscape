using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    // 自動で消える
public class MessageTimer: MonoBehaviour
{
    public float timer = 5.0f;

    void Start()
    { 
        Destroy(this.gameObject, timer);
    }

}

