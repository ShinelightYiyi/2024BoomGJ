using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using static NodeCanvas.DialogueTrees.DialogueTree;

namespace NodeCanvas.DialogueTrees.UI.Examples
{

    public class DialogueUGUI : MonoBehaviour, IPointerClickHandler
    {

        [System.Serializable]
        public class SubtitleDelays
        {
            public float characterDelay = 0.05f;
            public float sentenceDelay = 0.5f;
            public float commaDelay = 0.1f;
            public float finalDelay = 1.2f;
        }

        //Options...
        [Header("输入设置")]
        [Tooltip("输入可跳过对话")]
        public bool skipOnInput;
        [Tooltip("等待输入进入下一句对话")]
        public bool waitForInput;

        //Group...
        [Header("对话框组件")]
        public RectTransform subtitlesGroup;
        public Text actorSpeech;
        public RectTransform waitInputIndicator;
        public SubtitleDelays subtitleDelays = new SubtitleDelays();
        public List<AudioClip> typingSounds;
        private AudioSource playSource;


        [Header("角色")]
        [SerializeField] public List<ActorParameter> actorParameters = new List<ActorParameter>();

        private AudioSource _localSource;
        private AudioSource localSource {
            get { return _localSource != null ? _localSource : _localSource = gameObject.AddComponent<AudioSource>(); }
        }


        private bool anyKeyDown;
        public void OnPointerClick(PointerEventData eventData) => anyKeyDown = true;
        void LateUpdate() => anyKeyDown = false;


        void Awake() 
        {
            if (BindCheck())
            {
                Subscribe();
                Hide();
            }
        }
        void OnEnable() { UnSubscribe(); Subscribe(); }
        void OnDisable() { UnSubscribe(); }

        /// <summary>
        /// 绑定检查
        /// </summary>
        bool BindCheck()
        {
            if (!subtitlesGroup||!actorSpeech)
            {
                Debug.LogError($"{this.gameObject.name}对话框，文本未绑定或丢失");
                return false;
            }
            return true;    
        } 

        void Subscribe() {
            DialogueTree.OnDialogueStarted += OnDialogueStarted;
            DialogueTree.OnDialoguePaused += OnDialoguePaused;
            DialogueTree.OnDialogueFinished += OnDialogueFinished;
            DialogueTree.OnSubtitlesRequest += OnSubtitlesRequest;
        }

        void UnSubscribe() {
            DialogueTree.OnDialogueStarted -= OnDialogueStarted;
            DialogueTree.OnDialoguePaused -= OnDialoguePaused;
            DialogueTree.OnDialogueFinished -= OnDialogueFinished;
            DialogueTree.OnSubtitlesRequest -= OnSubtitlesRequest;
        }

        void Hide() {
            subtitlesGroup.gameObject.SetActive(false);
            if(waitInputIndicator!=null)
                waitInputIndicator.gameObject.SetActive(false);
        }

        void OnDialogueStarted(DialogueTree dlg) {
            //nothing special...
        }

        void OnDialoguePaused(DialogueTree dlg) {
            subtitlesGroup.gameObject.SetActive(false);

            StopAllCoroutines();
            if ( playSource != null ) playSource.Stop();
        }

        void OnDialogueFinished(DialogueTree dlg) {
            subtitlesGroup.gameObject.SetActive(false);


            StopAllCoroutines();
            if ( playSource != null ) playSource.Stop();
        }

        ///----------------------------------------------------------------------------------------------

        /*
        void OnSubtitlesRequest(SubtitlesRequestInfo info) {
            StartCoroutine(Internal_OnSubtitlesRequestInfo(info));
        }*/

        void OnSubtitlesRequest(SubtitlesRequestInfo info)
        {
            // 仅处理特定角色的对话
            if (actorParameters.Any(actorParam => actorParam.actor == info.actor))
            {
                StartCoroutine(Internal_OnSubtitlesRequestInfo(info));
            }
        }
        IEnumerator Internal_OnSubtitlesRequestInfo(SubtitlesRequestInfo info) {

            var text = info.statement.text;
            var audio = info.statement.audio;
            var actor = info.actor;

            subtitlesGroup.transform.localPosition = actor.dialoguePosition;

            subtitlesGroup.gameObject.SetActive(true);
            actorSpeech.text = "";

            actorSpeech.color = actor.dialogueColor;



            if ( audio != null ) {
                var actorSource = actor.transform != null ? actor.transform.GetComponent<AudioSource>() : null;
                playSource = actorSource != null ? actorSource : localSource;
                playSource.clip = audio;
                playSource.Play();
                actorSpeech.text = text;
                var timer = 0f;
                while ( timer < audio.length ) {
                    if ( skipOnInput && anyKeyDown ) {
                        playSource.Stop();
                        break;
                    }
                    timer += Time.deltaTime;
                    yield return null;
                }
            }

            if ( audio == null ) {
                var tempText = "";
                var inputDown = false;
                if ( skipOnInput ) {
                    StartCoroutine(CheckInput(() => { inputDown = true; }));
                }

                for ( int i = 0; i < text.Length; i++ ) {

                    if ( skipOnInput && inputDown ) {
                        actorSpeech.text = text;
                        yield return null;
                        break;
                    }

                    if ( subtitlesGroup.gameObject.activeSelf == false ) {
                        yield break;
                    }

                    char c = text[i];
                    tempText += c;
                    yield return StartCoroutine(DelayPrint(subtitleDelays.characterDelay));
                    PlayTypeSound();
                    if ( c == '.' || c == '!' || c == '?' ) {
                        yield return StartCoroutine(DelayPrint(subtitleDelays.sentenceDelay));
                        PlayTypeSound();
                    }
                    if ( c == ',' ) {
                        yield return StartCoroutine(DelayPrint(subtitleDelays.commaDelay));
                        PlayTypeSound();
                    }

                    actorSpeech.text = tempText;
                }

                if ( !waitForInput ) {
                    yield return StartCoroutine(DelayPrint(subtitleDelays.finalDelay));
                }
            }

            if ( waitForInput && waitInputIndicator!=null) {
                waitInputIndicator.gameObject.SetActive(true);
                while ( !Input.anyKeyDown ) {
                    yield return null;
                }
                waitInputIndicator.gameObject.SetActive(false);
            }

            yield return null;
            subtitlesGroup.gameObject.SetActive(false);
            info.Continue();
        }

        void PlayTypeSound() {
            if ( typingSounds.Count > 0 ) {
                var sound = typingSounds[Random.Range(0, typingSounds.Count)];
                if ( sound != null ) {
                    localSource.PlayOneShot(sound, Random.Range(0.6f, 1f));
                }
            }
        }

        IEnumerator CheckInput(System.Action Do) {
            while ( !Input.anyKeyDown ) {
                yield return null;
            }
            Do();
        }

        IEnumerator DelayPrint(float time) {
            var timer = 0f;
            while ( timer < time ) {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        ///----------------------------------------------------------------------------------------------

       

    }
}