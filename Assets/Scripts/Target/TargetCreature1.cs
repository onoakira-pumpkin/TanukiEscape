using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCreature1 : MonoBehaviour
{

    public int targetId = 2;

    private int[] CharIdList;
    private List<GameObject> charImageList;

    int targetNameLen;

    // Start is called before the first frame update
    void Start()
    {
        charImageList = GetComponent<TargetManager>().charImageList;
        CharIdList = GetComponent<TargetManager>().CharIdList;
        targetNameLen = CharIdList.Length;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator Vanish(int charId)
    {
        charImageList = GetComponent<TargetManager>().charImageList;
        CharIdList = GetComponent<TargetManager>().CharIdList;

        for (int i = 0; i < targetNameLen; i++)
        {

            if (CharIdList[i] == charId)
            {
                yield return StartCoroutine(charImageList[i].GetComponent<CharMove>().Vanish());
            }
        }

        yield return null;
    }

}
