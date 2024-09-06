using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video_State : State
{
    public Video_State(VideoClip video, State subsequentState)
    {
        VideoClip = video;
        SubsequentState = subsequentState;
    }

    Cove.ButtonHUD HUD;
    readonly VideoClip VideoClip;
    readonly State SubsequentState;

    protected override void PrepareState(Action callback)
    {
        if (VideoClip != null)
        {
            // var width = Cam.Io.UICamera.orthographicSize * Cam.Io.UICamera.aspect * 1.65f;

            // Dialog.VideoPlayer.transform.localScale = new Vector3(width,
            //     width / ((float)Dialog.CurrentLine.VideoClip.width / Dialog.CurrentLine.VideoClip.height), 1);
            float aspect = (float)((float)VideoClip.width / (float)VideoClip.height);

            Debug.Log("Height: " + VideoClip.height + ", width: " + VideoClip.width + ", " + aspect);
            VideoPlayer.gameObject.SetActive(true);
            VideoPlayer.transform.localScale = new Vector3(1.5f, 1.5f / aspect, 1);
            VideoPlayer.isLooping = false;
            VideoPlayer.clip = VideoClip;
            VideoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
            VideoPlayer.SetDirectAudioVolume(0, .85f);
            VideoPlayer.Prepare();
            LoadVideo(callback).StartCoroutine();
            return;

            IEnumerator LoadVideo(Action callback)
            {
                while (!VideoPlayer.isPrepared) yield return null;

                HUD = new();
                callback();
            }
        }

        // HUD.HideTexts();

    }
    protected override void EngageState()
    {
        if (VideoClip != null)
        {
            VideoPlayer.transform.SetPositionAndRotation(
                    Cam.Io.Camera.transform.position + Cam.Io.Camera.transform.forward,
                    Cam.Io.Camera.transform.rotation);

            VideoPlayer.Play();

            MonoHelper.OnUpdate += CheckForCompletion;
        }

        // HUD.HideTexts();
        HUD.SetCardPos1(HUD.South);
        HUD.SetCardPos2(HUD.North);
        HUD.SetCardPos3(HUD.East);
        HUD.SetCardPos4(HUD.West);
        HUD.North.SetImageColor(Color.white).SetTextString("Pause");
        HUD.South.SetImageColor(Color.white).SetTextString("Back");
        HUD.West.SetImageColor(Color.white).SetTextString("<<").SetImageSprite(Assets.LeftButton);
        HUD.East.SetImageColor(Color.white).SetTextString(">>").SetImageSprite(Assets.RightButton);
    }

    protected override void DisengageState()
    {
        MonoHelper.OnUpdate -= CheckForCompletion;
        HUD.SelfDestruct();
        _progress?.SelfDestruct();
        GameObject.Destroy(_videoPlayer.gameObject);
    }

    protected override void GPInput(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.South_Press:
                // SetState(SubsequentState);
                VideoPlayer.Stop();
                return;

            case GamePadButton.North_Press:
                if (VideoPlayer.isPaused)
                {
                    MonoHelper.OnUpdate += CheckForCompletion;
                    VideoPlayer.Play();
                    HUD.North.SetImageColor(Color.white).SetTextString("Pause");
                }
                else
                {
                    MonoHelper.OnUpdate -= CheckForCompletion;
                    VideoPlayer.Pause();
                    HUD.North.SetImageColor(Color.white).SetTextString("Play");
                }
                return;

            case GamePadButton.Left_Press:
                VideoPlayer.time -= VideoPlayer.clip.length * .1f;
                return;

            case GamePadButton.Right_Press:
                VideoPlayer.time += VideoPlayer.clip.length * .1f;
                return;
        }
    }

    void CheckForCompletion()
    {
        Progress.SetTextString(
            ((int)VideoPlayer.time).TimeStamp() +
            " / " +
            ((int)VideoPlayer.clip.length).TimeStamp());

        if (!VideoPlayer.isPlaying)
            SetState(SubsequentState);
    }

    private VideoPlayer _videoPlayer;
    public VideoPlayer VideoPlayer => _videoPlayer ? _videoPlayer : _videoPlayer = SetUpVideo();
    VideoPlayer SetUpVideo()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
        go.GetComponent<MeshRenderer>().material = Assets.Video_Mat;
        go.name = nameof(VideoPlayer);
        // go.transform.SetParent(Parent.transform);
        go.transform.position = new Vector3(0, -1, -1f);
        VideoPlayer v = go.AddComponent<VideoPlayer>();
        v.playOnAwake = false;
        return v;
    }

    Card _progress;
    Card Progress => _progress ??= new Card(nameof(Progress), null)
        .SetTMPPosition(new Vector2(0, 1f - Cam.MainOrthoY));
}