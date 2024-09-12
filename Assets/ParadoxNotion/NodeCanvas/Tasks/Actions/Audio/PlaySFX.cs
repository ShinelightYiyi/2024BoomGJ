using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    [Category("Audio")]
    public class PlaySFX : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<AudioClip> audioClip;
        [SliderField(0, 100)]
        public int volume = 100;

        protected override string info
        {
            get { return "²¥·ÅSFX" + audioClip.ToString(); }
        }

        protected override void OnExecute()
        {
            AudioController.Instance.PlaySFX(audioClip.value);
            new Commands.Settings.SetSFXVolumeCommand(volume).Execute();
            EndAction();
            
        }
    }
}