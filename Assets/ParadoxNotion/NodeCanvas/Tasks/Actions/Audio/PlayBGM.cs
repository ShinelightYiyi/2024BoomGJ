using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    [Category("Audio")]
    public class PlayBGM : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<AudioClip> audioClip;
        [SliderField(0, 100)]
        public int volume = 100;
        public bool isLoop;

        protected override string info
        {
            get { return "²¥·ÅBGM" + audioClip.ToString(); }
        }

        protected override void OnExecute()
        {
            AudioController.Instance.PlayBGM(audioClip.value,isLoop);
            new Commands.Settings.SetBGMVolumeCommand(volume).Execute();
            EndAction();
            
        }
    }
}