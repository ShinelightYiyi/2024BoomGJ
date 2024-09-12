
using Framework.VisualNovel;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{
    public class SwitchBackground : ActionTask<Transform>
    {
        [RequiredField]
        public Sprite background=default;
        public float fadeTime=1f;

        protected override string info 
        {
            get { return "�����л�Ϊ " + (background != null ? background.name : "δ����"); }
        }

        protected override void OnExecute()
        {
            BackgroundController.Instance.ChangeBackground(background, fadeTime,()=> EndAction());
        }

    }
}

