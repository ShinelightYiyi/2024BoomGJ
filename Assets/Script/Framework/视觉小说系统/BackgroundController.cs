using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.VisualNovel
{
    public class BackgroundController : SingletonMono<BackgroundController>
    {
        public Image backgroundImageUp;
        public Image backgroundImageDown;

        private CanvasGroup canvasGroup;

        protected void Awake()
        {
            backgroundImageUp = GameObject.Find("BackgroundUp").GetComponent<Image>();
            backgroundImageDown = GameObject.Find("BackgroundDown").GetComponent<Image>();
            if (backgroundImageUp == null || backgroundImageDown == null)
            {
                Debug.LogError("BackgroundImage ������");
            }
            canvasGroup = backgroundImageDown.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = backgroundImageDown.gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void ChangeBackground(Sprite sprite, float duration, Action callback)
        {
            // ���ñ���ͼƬ����͸������Ϊ 0
            SetImageAlpha(backgroundImageUp, 0f);
            backgroundImageUp.sprite = sprite;

            // ��ʼ����Ч��
            StartCoroutine(Fade(duration, () => {
                callback?.Invoke();
                backgroundImageDown.sprite = sprite;
                SetImageAlpha(backgroundImageUp, 1f); // ȷ������͸����Ϊ 1
            }));
        }

        // ����͸���ȵ�Э��
        private IEnumerator Fade(float duration, Action callback)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                SetImageAlpha(backgroundImageUp, elapsedTime / duration); // ���Խ���͸����
                SetImageAlpha(backgroundImageDown, 1-elapsedTime / duration);
                yield return null;
            }
            callback?.Invoke();
        }

        // ��������������Image��Alpha͸����
        private void SetImageAlpha(Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}

