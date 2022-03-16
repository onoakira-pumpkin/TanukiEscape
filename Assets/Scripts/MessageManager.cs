using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{

    [SerializeField] Text textMessage;
    private string sentence;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
 
        
    }

    public void SetMessage(string sent)
    {
        sentence = sent;
        textMessage.text = sentence;

    }


}
