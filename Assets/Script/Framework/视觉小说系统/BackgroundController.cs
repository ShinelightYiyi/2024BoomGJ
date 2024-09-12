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
                Debug.LogError("BackgroundImage 不存在");
            }
            canvasGroup = backgroundImageDown.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = backgroundImageDown.gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void ChangeBackground(Sprite sprite, float duration, Action callback)
        {
            // 设置背景图片并将透明度设为 0
            SetImageAlpha(backgroundImageUp, 0f);
            backgroundImageUp.sprite = sprite;

            // 开始渐变效果
            StartCoroutine(Fade(duration, () => {
                callback?.Invoke();
                backgroundImageDown.sprite = sprite;
                SetImageAlpha(backgroundImageUp, 1f); // 确保最终透明度为 1
            }));
        }

        // 渐变透明度的协程
        private IEnumerator Fade(float duration, Action callback)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                SetImageAlpha(backgroundImageUp, elapsedTime / duration); // 线性渐变透明度
                SetImageAlpha(backgroundImageDown, 1-elapsedTime / duration);
                yield return null;
            }
            callback?.Invoke();
        }

        // 辅助函数：设置Image的Alpha透明度
        private void SetImageAlpha(Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}

