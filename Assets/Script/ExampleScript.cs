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
        //�¼�����/�¼�ϵͳ
        EventCenter.Instance.Subscribe("����msg",SendMsg);
        EventCenter.Instance.Publish("����msg");
        //�Ի���
        dialogueTreeController.StartDialogue();
        //����ϵͳ
        new Commands.Settings.SetBGMVolumeCommand(50).Execute();
    }
    
    void SendMsg()
    {
        //Mono�й�
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
