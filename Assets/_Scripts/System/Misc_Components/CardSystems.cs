using System.Collections;
using TMPro;
using UnityEngine;

public static class CardSystems
{

    /// <summary>
    ///     Higher Number is displayed on top. Default number is 1.
    /// </summary>
    public static Card SetCanvasSortingOrder(this Card Card, int i)
    {
        Card.Canvas.sortingOrder = i;
        return Card;
    }

    public static Card SetCanvasRenderMode(this Card card, RenderMode renderMode)
    {
        card.Canvas.renderMode = renderMode;
        return card;
    }

    public static Card SetCanvasWorldCamera(this Card card, Camera worldCam)
    {
        card.Canvas.worldCamera = worldCam;
        return card;
    }

    public static Card SetFont(this Card Card, TMP_FontAsset f)
    {
        Card.TMP.font = f;
        return Card;
    }

    public static Card SetFontStyle(this Card Card, FontStyles f)
    {
        Card.TMP.fontStyle = f;
        return Card;
    }

    public static Card SetFontScale(this Card Card, float min, float max)
    {
        Card.TMP.fontSizeMin = Card.CanvasScaler.referenceResolution.x * .043125f * min;
        Card.TMP.fontSizeMax = Card.CanvasScaler.referenceResolution.x * .043125f * max;
        return Card;
    }

    public static Card AutoSizeFont(this Card Card, bool tf)
    {
        Card.TMP.enableAutoSizing = tf;
        return Card;
    }

    public static Card AutoSizeTextContainer(this Card Card, bool tf)
    {
        Card.TMP.autoSizeTextContainer = tf;
        return Card;
    }

    public static Card AllowWordWrap(this Card Card, bool tf)
    {
        Card.TMP.enableWordWrapping = tf;
        return Card;
    }

    public static Card SetTextString(this Card Card, string s)
    {
        Card.TMP.text = s;
        return Card;
    }

    public static Card SetTextColor(this Card Card, Color c)
    {
        Card.TMP.color = c;
        return Card;
    }

    public static Card SetTextAlignment(this Card Card, TextAlignmentOptions a)
    {
        Card.TMP.alignment = a;
        return Card;
    }

    public static Card SetTMPRectPivot(this Card Card, float x, float y)
    {
        Card.TMP.rectTransform.pivot = new Vector2(x, y);
        return Card;
    }

    public static Card SetTMPRectPivot(this Card Card, Vector2 piv)
    {
        Card.TMP.rectTransform.pivot = piv;
        return Card;
    }

    public static Card SetTMPRectAnchor(this Card Card, Vector2 anc)
    {
        Card.TMP.rectTransform.anchorMin = anc;
        Card.TMP.rectTransform.anchorMax = anc;
        return Card;
    }

    /// <summary>
    ///     Use this to set the TMP Size.
    /// </summary>
    public static Card SetTMPSize(this Card Card, float x, float y)
    {
        return Card.SetTMPSize(new Vector2(x, y));
    }

    /// <summary>
    ///     Use this to set the TMP Size.
    /// </summary>
    public static Card SetTMPSize(this Card Card, Vector2 v)
    {
        Card.TMP.rectTransform.sizeDelta =
            .5f * Card.CanvasScaler.referenceResolution.y * v / Cam.Io.UICamera.orthographicSize;
        return Card;
    }

    /// <summary>
    ///     Use this to set the TMP position.
    /// </summary>
    public static Card SetTMPPosition(this Card Card, float x, float y)
    {
        return Card.SetTMPPosition(new Vector3(x, y, 0));
    }

    /// <summary>
    ///     Use this to set the TMP position.
    /// </summary>
    public static Card SetTMPPosition(this Card Card, Vector3 v)
    {
        Vector2 sPos = Cam.Io.UICamera.WorldToScreenPoint(v);
        Vector2 sSize = new(Cam.Io.UICamera.pixelWidth, Cam.Io.UICamera.pixelHeight);

        Card.TMP.rectTransform.localPosition = new Vector2(sPos.x - sSize.x * .5f, sPos.y - sSize.y * .5f);
        return Card;
    }

    public static Card OffsetTMPPosition(this Card Card, Vector2 v2)
    {
        Card.TMP.rectTransform.localPosition += (Vector3)(Card.TMP.rectTransform.sizeDelta * v2);
        return Card;
    }

    public static Card TMPClickable(this Card Card)
    {
        WaitAStep().StartCoroutine();
        return Card;

        IEnumerator WaitAStep()
        {
            yield return null;
            Card.SetClickable(Card.TMP.gameObject.AddComponent<Clickable>());
            var bc = Card.TMP.gameObject.GetComponent<BoxCollider2D>();
            bc.size = Card.TMP.rectTransform.sizeDelta;
            bc.offset = new Vector2(Card.TMP.rectTransform.sizeDelta.x * (-Card.TMP.rectTransform.pivot.x + .5f), 0);
        }
    }



    public static Card OffsetImageFromTMP(this Card Card, Vector2 v)
    {
        WaitAStep().StartCoroutine();
        return Card;

        IEnumerator WaitAStep()
        {
            yield return null;
            Card.Image.rectTransform.localPosition +=
            (Vector3)((Card.TMP.rectTransform.sizeDelta * .5f * v) +
                      (Card.Image.rectTransform.sizeDelta * .5f * v));
        }
    }

    public static Card OffsetImagePosition(this Card Card, Vector2 v)
    {
        Card.Image.rectTransform.localPosition +=
        (Vector3)(Card.Image.rectTransform.sizeDelta * v);
        return Card;
    }

    public static Card SetImageColor(this Card Card, Color c)
    {
        Card.Image.color = c;
        return Card;
    }

    public static Card SetImagePosition(this Card Card, float x, float y)
        => Card.SetImagePosition(new Vector2(x, y));
    public static Card SetImagePosition(this Card Card, Vector2 pos)
    {
        Vector2 sPos = Cam.Io.UICamera.WorldToScreenPoint(pos);
        Vector2 sSize = new(Cam.Io.UICamera.pixelWidth, Cam.Io.UICamera.pixelHeight);

        Card.Image.rectTransform.localPosition = new Vector2(sPos.x - sSize.x * .5f, sPos.y - sSize.y * .5f);
        return Card;
    }

    public static Card ScaleImageSizeToTMP(this Card Card, float scale)
    {
        WaitAStep().StartCoroutine();
        return Card;

        IEnumerator WaitAStep()
        {
            yield return null;
            Card.Image.rectTransform.sizeDelta = Card.TMP.rectTransform.sizeDelta * scale;
        }
    }

    public static Card SetImageSizeUnscaled(this Card Card, Vector3 size)
    {
        Card.Image.rectTransform.sizeDelta = size; return Card;
    }

    /// <summary>
    /// Scaled to resolution: .5f * Card.CanvasScaler.referenceResolution.y * v2 / Cam.Io.UICamera.orthographicSize
    /// </summary>
    public static Card SetImageSize(this Card Card, float x, float y)
    {
        return Card.SetImageSize(new Vector2(x, y));
    }
    /// <summary>
    /// Scaled to resolution: .5f * Card.CanvasScaler.referenceResolution.y * v2 / Cam.Io.UICamera.orthographicSize
    /// </summary>
    public static Card SetImageSize(this Card Card, Vector2 v2)
    {
        Card.Image.rectTransform.sizeDelta =
            .5f * Card.CanvasScaler.referenceResolution.y * v2 / Cam.Io.UICamera.orthographicSize;
        return Card;
    }

    public static Card SetImageRectPivot(this Card card, float x, float y)
    {
        card.Image.rectTransform.pivot = new Vector2(x, y);
        return card;
    }

    public static Card SetImageSprite(this Card Card, Sprite s)
    {
        Card.Image.sprite = s;
        return Card;
    }

    public static Card ImageClickable(this Card Card)
    {
        WaitAStep().StartCoroutine();
        return Card;

        IEnumerator WaitAStep()
        {
            yield return null;
            Card.SetClickable(Card.Image.gameObject.AddComponent<Clickable>());
            var bc = Card.Image.gameObject.GetComponent<BoxCollider2D>();
            bc.size = Card.Image.rectTransform.sizeDelta;
            bc.offset = new Vector2(Card.Image.rectTransform.sizeDelta.x * (-Card.Image.rectTransform.pivot.x + .5f),
                0);
        }
    }


    //public static Card SetImageOrder(this Card card, int i)
    //{
    //    if (card.Image.TryGetComponent(out MeshRenderer mr)) mr.sortingOrder = i;
    //    return card;
    //}

    public static Card SetGOSize(this Card Card, Vector2 v)
    {
        Card.GO.transform.localScale = v;
        return Card;
    }

    public static Card SetGOPosition(this Card Card, Vector3 v)
    {
        Card.GO.transform.position = v;
        return Card;
    }




    public static Card SetSpriteSize(this Card Card, Vector2 v)
    {
        Card.GO.transform.localScale = v;
        return Card;
    }

    public static Card SetSpritePosition(this Card Card, float x, float y) => SetSpritePosition(Card, new Vector3(x, y, 0));
    public static Card SetSpritePosition(this Card Card, float x, float y, float z) => SetSpritePosition(Card, new Vector3(x, y, z));
    public static Card SetSpritePosition(this Card Card, Vector3 v)
    {
        if (Card.GO == null) return Card;
        Card.GO.transform.position = v;
        return Card;
    }

    public static Card SetSprite(this Card Card, Sprite s)
    {
        Card.SpriteRenderer.sprite = s;
        return Card;
    }

    public static Card SetSpriteColor(this Card Card, Color c)
    {
        Card.SpriteRenderer.color = c;
        return Card;
    }

    public static Card ClickableSprite(this Card Card)
    {
        Card.SetClickable(Card.GO.AddComponent<Clickable>());
        //Card.GO.GetComponent<CardCollider2D>().size = Card.GO.transform.localScale;
        return Card;
    }






    /// <summary>
    /// Don't use if there is no Image/TMP or  the call will create an empty canvas.
    /// </summary>
    public static Card SetSizeAll(this Card Card, Vector2 v)
    {
        if (Card.SRExists) Card.SetSpriteSize(v);
        if (Card.CanvasExists) Card.SetImageSize(v);
        if (Card.TMPExists) Card.SetTMPSize(v);
        return Card;
    }
    /// <summary>
    ///  Don't use if there is no Image/TMP or  the call will create an empty canvas.
    /// </summary>
    public static Card SetPositionAll(this Card Card, Vector3 v)
    {
        Card.SetSpritePosition(v);
        Card.SetImagePosition(v);
        return Card.SetTMPPosition(v);
    }
    /// <summary>
    ///  Don't use if there is no Image/TMP or  the call will create an empty canvas.
    /// </summary>
    public static Card SetPositionAll(this Card Card, float x, float y)
    {
        return Card.SetPositionAll(new Vector3(x, y));
    }

    public static Card SetImageToDefaultLayer(this Card card)
    {
        card.Image.gameObject.layer = 0;
        return card;
    }

    public static Card SetTMPToDefaultLayer(this Card card)
    {
        card.TMP.gameObject.layer = 0;
        return card;
    }
    public static Card SetSpriteToDefaultLayer(this Card card)
    {
        card.SpriteRenderer.gameObject.layer = 0;
        return card;
    }
    public static Card SetImageToUILayer(this Card card)
    {
        card.Image.gameObject.layer = 5;
        return card;
    }

    public static Card SetTMPToUILayer(this Card card)
    {
        card.TMP.gameObject.layer = 5;
        return card;
    }
    public static Card SetSpriteToUILayer(this Card card)
    {
        card.SpriteRenderer.gameObject.layer = 5;
        return card;
    }


    public static Card SetOutlineWidth(this Card card, float f)
    {
        card.TMP.outlineWidth = f;
        return card;
    }

    public static Card SetOutlineColor(this Card card, Color c)
    {
        card.TMP.outlineColor = c;
        return card;
    }


    public static Card SetUIGO(this Card card, GameObject go)
    {
        card.UIGO = go;
        return card;
    }

}