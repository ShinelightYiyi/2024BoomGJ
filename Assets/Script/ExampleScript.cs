using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public DialogueTreeController dialogueTreeController;
    void Start()
    {
        //事件中心/事件系统
        EventCenter.Instance.Subscribe("发送msg",SendMsg);
        EventCenter.Instance.Publish("发送msg");
        //对话树
        dialogueTreeController.StartDialogue();
        //设置系统
        new Commands.Settings.SetBGMVolumeCommand(50).Execute();
    }
    
    void SendMsg()
    {
        //Mono托管
        MonoManager.Instance.StartCoroutine(Msg());
        
    }
    IEnumerator Msg()
    {
        Debug.Log("Message");
        yield return null;
    }
    void Update()
    {
        
    }
}
