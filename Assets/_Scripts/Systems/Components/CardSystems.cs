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
        Card.TMPCanvas.sortingOrder = i;
        return Card;
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
        Card.TMP.fontSizeMin = Card.TMPCanvasScaler.referenceResolution.x * .043125f * min;
        Card.TMP.fontSizeMax = Card.TMPCanvasScaler.referenceResolution.x * .043125f * max;
        return Card;
    }

    public static Card AutoSizeFont(this Card Card, bool tf)
    {
        Card.TMP.enableAutoSizing = tf;
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

    public static Card OffsetImagePosition(this Card card, Vector2 v)
    {
        card.Image.rectTransform.localPosition += (Vector3)(card.Image.rectTransform.sizeDelta * v);
        return card;
    }

    public static Card SetImageLayer(this Card card, int l)
    {
        card.Image.canvas.sortingOrder = l;
        return card;
    }

    public static Card SetImageColor(this Card card, Color c)
    {
        card.Image.color = c;
        return card;
    }

    public static Card SetImagePosition(this Card card, Vector3 pos)
    {
        Vector2 spos = Cam.Io.Camera.WorldToScreenPoint(pos);
        var ssize = new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);

        card.Image.rectTransform.localPosition = new Vector2(spos.x - ssize.x * .5f, spos.y - ssize.y * .5f);
        return card;
    }

    public static Card ScaleImageSizeToTMP(this Card card, float scale)
    {
        card.Image.rectTransform.sizeDelta = card.TMP.rectTransform.sizeDelta * scale;
        return card;
    }

    public static Card SetImageSize(this Card card, float x, float y)
    {
        return card.SetImageSize(new Vector2(x, y));
    }

    public static Card SetImageSize(this Card card, Vector2 v2)
    {
        card.Image.rectTransform.sizeDelta =
            .45f * card.ImageCanvasScaler.referenceResolution.y * v2 / Cam.Io.Camera.orthographicSize;
        return card;
    }

    public static Card SetImageSprite(this Card card, Sprite s)
    {
        card.Image.sprite = s;
        return card;
    }

    public static Card AutoSizeTextContainer(this Card Card, bool tf)
    {
        Card.TMP.autoSizeTextContainer = tf;
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
            .45f * Card.TMPCanvasScaler.referenceResolution.y * v / Cam.Io.Camera.orthographicSize;
        return Card;
    }

    /// <summary>
    ///     Use this to set the sprites size.
    /// </summary>
    public static Card SetGOSize(this Card Card, Vector2 v)
    {
        Card.GO.transform.localScale = v;
        return Card;
    }

    /// <summary>
    ///     Use this to set the sprite & TMP size. Don't use if there is no TMP or the call will create an empty canvas etc.
    /// </summary>
    public static Card SetSizeAll(this Card Card, Vector2 v)
    {
        Card.SetGOSize(v);
        Card.SetImageSize(v);
        return Card.SetTMPSize(v);
    }

    /// <summary>
    ///     Use this to set the sprites position.
    /// </summary>
    public static Card SetGOPosition(this Card Card, Vector3 v)
    {
        Card.GO.transform.position = v;
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
        Vector2 spos = Cam.Io.Camera.WorldToScreenPoint(v);
        var ssize = new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);

        Card.TMP.rectTransform.localPosition = new Vector2(spos.x - ssize.x * .5f, spos.y - ssize.y * .5f);
        return Card;
    }

    public static Card OffsetTMPPosition(this Card card, Vector2 v2)
    {
        card.TMP.rectTransform.localPosition += (Vector3)(card.TMP.rectTransform.sizeDelta * v2);
        return card;
    }

    /// <summary>
    ///     Use this to set the sprite & TMP position. Don't use if there is no TMP or the call will create an empty canvas
    ///     etc.
    /// </summary>
    public static Card SetPositionAll(this Card Card, Vector3 v)
    {
        Card.SetGOPosition(v);
        Card.SetImagePosition(v);
        return Card.SetTMPPosition(v);
    }

    public static Card SetPositionAll(this Card card, float x, float y)
    {
        return card.SetPositionAll(new Vector3(x, y));
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

    public static Card SpriteClickable(this Card Card)
    {
        Card.Clickable = Card.GO.AddComponent<Clickable>();
        //Card.GO.GetComponent<BoxCollider2D>().size = Card.GO.transform.localScale;
        return Card;
    }

    public static Card ImageClickable(this Card Card)
    {
        WaitAStep().StartCoroutine();
        return Card;

        IEnumerator WaitAStep()
        {
            yield return null;
            Card.Clickable = Card.Image.gameObject.AddComponent<Clickable>();
            var bc = Card.Image.gameObject.GetComponent<BoxCollider2D>();
            bc.size = Card.Image.rectTransform.sizeDelta;
            bc.offset = new Vector2(Card.Image.rectTransform.sizeDelta.x * (-Card.Image.rectTransform.pivot.x + .5f),
                0);
        }
    }

    public static Card TMPClickable(this Card Card)
    {
        WaitAStep().StartCoroutine();
        return Card;

        IEnumerator WaitAStep()
        {
            yield return null;
            Card.Clickable = Card.TMP.gameObject.AddComponent<Clickable>();
            var bc = Card.TMP.gameObject.GetComponent<BoxCollider2D>();
            bc.size = Card.TMP.rectTransform.sizeDelta;
            bc.offset = new Vector2(Card.TMP.rectTransform.sizeDelta.x * (-Card.TMP.rectTransform.pivot.x + .5f), 0);
        }
    }
}