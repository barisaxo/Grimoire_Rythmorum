using UnityEngine;
using UnityEngine.Video;

namespace Dialog
{
    public sealed class Dialog
    {
        public Dialog(Dialogue dialogue)
        {
            Dialogue = dialogue;
            _ = DialogCard;
            _ = DialogBG;
            _ = TextBackground;

            CurrentLine = Dialogue.FirstLine;
            NPCIcon(Dialogue.FirstLine);
        }

        public void SelfDestruct()
        {
            Object.Destroy(_parent);
        }

        public bool LetType;
        public Line CurrentLine;
        public Dialogue Dialogue;

        private GameObject _parent;
        public GameObject Parent => _parent != null ? _parent : _parent = new GameObject(nameof(Dialog));

        private Card _textBackground;
        private Card TextBackground => _textBackground ??= new Card(nameof(TextBackground), Parent.transform)
            .SetImageSize(Vector2.one * 9001)
            .SetImageSprite(Assets.White)
            .SetImageColor(new Color(0f, .0f, 0f, .666f))
            .SetCanvasSortingOrder(1);

        private Card[] _npcIcon;
        public void NPCIcon(Line line) => NPCIcon(line.Speaker.Icon, line.Speaker.Color);
        void NPCIcon(Sprite[] sprites, Color col)
        {
            if (_npcIcon != null) foreach (Card c in _npcIcon) UnityEngine.Object.DestroyImmediate(c.GO);
            if (sprites != null) _npcIcon = SetUpNPCIcon();

            Card[] SetUpNPCIcon()
            {
                Card[] cs = new Card[sprites.Length];
                for (int i = 0; i < sprites.Length; i++)
                {
                    cs[i] = new Card(nameof(NPCIcon), Parent.transform)
                          .SetImageSprite(sprites[i])
                          .SetImageSize(Vector3.one * 2f)
                          .SetImagePosition(new Vector3(-Cam.UIOrthoX + .65f, Cam.UIOrthoY - 1f))
                          .SetImageColor(col)
                          .SetCanvasSortingOrder(i + 11)
                          .SetImageRectPivot(0, .5f)
                          ;
                }
                return cs;
            }
        }

        private Card _dialogCard;
        public Card DialogCard => _dialogCard ??= new Card(nameof(DialogCard), Parent.transform)
            .SetTextAlignment(TMPro.TextAlignmentOptions.TopLeft)
            .SetTMPSize(new Vector2(Cam.UIOrthoX * 1.5f, Cam.UIOrthoY * .7f))
            .SetTMPPosition(new Vector3(1.15f, Cam.UIOrthoY - (Cam.UIOrthoY * .45f)))
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(true)
            .SetCanvasSortingOrder(3)
            ;

        private Card _dialogBG;
        public Card DialogBG => _dialogBG ??= new Card(nameof(DialogBG), Parent.transform)
            .SetImageSprite(Assets.White)
            .SetImageColor(new Color(.15f, .15f, .15f, .65f))
            .SetCanvasSortingOrder(2)
            .SetImagePosition(new Vector3(1, Cam.UIOrthoY - (Cam.UIOrthoY * .45f)))
            .SetImageSize(new Vector2(Cam.UIOrthoX * 1.6f, Cam.UIOrthoY * .8f))
            ;



        private VideoPlayer _videoPlayer;
        public VideoPlayer VideoPlayer => _videoPlayer = _videoPlayer != null ? _videoPlayer : SetUpVideo();
        VideoPlayer SetUpVideo()
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
            go.GetComponent<MeshRenderer>().material = Assets.Video_Mat;
            go.name = nameof(VideoPlayer);
            go.transform.SetParent(Parent.transform);
            go.transform.position = new Vector3(0, -1, -1f);
            VideoPlayer v = go.AddComponent<VideoPlayer>();
            v.playOnAwake = false;
            return v;
        }
    }
}