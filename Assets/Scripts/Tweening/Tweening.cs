using UnityEngine;
using DG.Tweening;

public static class Tweening
{
    //Scale UP + Fade In tween for UI elements in Canvas
    public static void TweenIn(GameObject obj_tween, float timeDuration)
    {
        obj_tween.SetActive(true);
        AttachCG(obj_tween);

        obj_tween.GetComponent<CanvasGroup>().alpha = 0;
        (obj_tween.transform as RectTransform).localScale = Vector3.one * 0.5f;
        //(obj_tween.transform as RectTransform).localPosition = new Vector3(0, -1000, 0);
        (obj_tween.transform as RectTransform).DOScale(Vector3.one, timeDuration).SetEase(Ease.Linear);
        //(obj_tween.transform as RectTransform).DOAnchorPos(new Vector2(0, 0), 0.3f, false).SetEase(Ease.InQuint);
        obj_tween.GetComponent<CanvasGroup>().DOFade(1, timeDuration);
    }

    //Scale UP tween for gameobjects in World Space
    public static void TweenIn_gameObject(GameObject obj_tween, float timeDuration, Vector3 startScale, Vector3 maxScale)
    {
        obj_tween.SetActive(true);
        
        obj_tween.transform.localScale = startScale;
        obj_tween.transform.DOScale(maxScale, timeDuration).SetEase(Ease.Linear);
    }


    //Scale DOWN + Fade Out tween for UI elements in Canvas
    public static void TweenOut(GameObject obj_tween, float timeDuration)
    {
        AttachCG(obj_tween);

        obj_tween.GetComponent<CanvasGroup>().alpha = 1;
        (obj_tween.transform as RectTransform).localScale = Vector3.one * 1;
        //(obj_tween.transform as RectTransform).localPosition = new Vector3(0, 0, 0);
        (obj_tween.transform as RectTransform).DOScale(Vector3.one * 0.5f, timeDuration);
        //(obj_tween.transform as RectTransform).DOAnchorPos(new Vector2(0, -1000), 0.2f, false).SetEase(Ease.OutFlash);
        obj_tween.GetComponent<CanvasGroup>().DOFade(0, timeDuration);
        DOVirtual.DelayedCall(timeDuration, () =>
        {
            obj_tween.SetActive(false);
        });
    }

    //Scale DOWN tween for gameobjects in world space
    public static void TweenOut_gameobject(GameObject obj_tween, float timeDuration, Vector3 startScale, Vector3 minScale)
    {
        obj_tween.transform.localScale = startScale;
        obj_tween.transform.DOScale(minScale, timeDuration);
    
        DOVirtual.DelayedCall(timeDuration, () =>
        {
            obj_tween.SetActive(false);
        });
    }

    //Move + Fade IN tween for UI elements in Canvas
    public static void TweenMove(GameObject obj_tween, float timeduration, Vector2 endPosition, Vector2 startPosition)
    {
        obj_tween.SetActive(true);

        AttachCG(obj_tween);

        obj_tween.GetComponent<CanvasGroup>().alpha = 0;
        (obj_tween.transform as RectTransform).localPosition = startPosition;
        (obj_tween.transform as RectTransform).DOAnchorPos(endPosition, timeduration, false).SetEase(Ease.Linear);
        obj_tween.GetComponent<CanvasGroup>().DOFade(1, timeduration);
    }

    //Move tween for gameobjects in world space
    public static void TweenMove_gameobject(GameObject obj_tween, float timeduration, Vector3 endPosition, Vector3 startPosition)
    {
        obj_tween.SetActive(true);
  
        obj_tween.transform.localPosition = startPosition;
        obj_tween.transform.DOMove(endPosition, timeduration, false).SetEase(Ease.InQuint);
    }

    // Punch tween for gameobjects and UI elements
    public static void TweenPunch(GameObject obj_tween, float timeduration, Vector3 punch)
    {
        obj_tween.transform.DOPunchPosition(punch, timeduration);
    } 

    // Fade IN tween for fading and turning off UI element
    public static void AlphaFadeIn(GameObject obj_tween, float timeduration)
    {
        obj_tween.SetActive(true);

        AttachCG(obj_tween);

        obj_tween.GetComponent<CanvasGroup>().alpha = 0;
        obj_tween.GetComponent<CanvasGroup>().DOFade(1, timeduration);
    }

    // Fade OUT tween for fading and turning off UI element
    public static void AlphaFadeOut(GameObject obj_tween, float timeduration)
    {
        AttachCG(obj_tween);

        obj_tween.GetComponent<CanvasGroup>().alpha = 1;
        obj_tween.GetComponent<CanvasGroup>().DOFade(0, timeduration);
        DOVirtual.DelayedCall(timeduration, () =>
        {
            obj_tween.SetActive(false);
        });
    }

    // Adding CanvasGroup whenever handling Alpha of UI element
    public static void AttachCG(GameObject obj_tween)
    {
        if (!obj_tween.GetComponent<CanvasGroup>())
            obj_tween.AddComponent<CanvasGroup>();
    }

    // Bubble Popout for UI elements
    public static void BubbleOut(GameObject obj_tween, float duration, Vector3 maxScale)
    {
        (obj_tween.transform as RectTransform).localScale = Vector3.one;
        (obj_tween.transform as RectTransform).DOScale(maxScale, duration / 2).SetEase(Ease.InExpo)
            .OnComplete(() => {
                (obj_tween.transform as RectTransform).DOScale(Vector3.one, duration / 2);
            });
    }

    // Bubble Popout for gameobjects
    public static void BubbleOut_gameobject(GameObject obj_tween, float duration, Vector3 maxScale, Vector3 normScale)
    {
        obj_tween.transform.localScale = normScale;
        obj_tween.transform.DOScale(maxScale, duration / 2).SetEase(Ease.InCirc)
            .OnComplete(() => {
                obj_tween.transform.DOScale(normScale, duration / 2);
            });
    }
}
