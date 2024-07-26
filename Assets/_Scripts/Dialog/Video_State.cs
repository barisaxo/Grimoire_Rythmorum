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

    readonly VideoClip VideoClip;
    readonly State SubsequentState;
    bool stop;
    protected override void PrepareState(Action callback)
    {
        if (VideoClip != null)
        {
            Debug.Log("Video");
            // var width = Cam.Io.UICamera.orthographicSize * Cam.Io.UICamera.aspect * 1.65f;

            // Dialog.VideoPlayer.transform.localScale = new Vector3(width,
            //     width / ((float)Dialog.CurrentLine.VideoClip.width / Dialog.CurrentLine.VideoClip.height), 1);

            VideoPlayer.transform.localScale = new Vector3(1.65f, 1, 1);
            VideoPlayer.gameObject.SetActive(true);
            VideoPlayer.playOnAwake = false;
            VideoPlayer.waitForFirstFrame = false;
            VideoPlayer.isLooping = false;
            VideoPlayer.clip = VideoClip;
            VideoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
            VideoPlayer.Prepare();
            LoadVideo(callback).StartCoroutine();
            return;

            IEnumerator LoadVideo(Action callback)
            {
                while (!VideoPlayer.isPrepared) yield return null;
                callback();
            }
        }


    }
    protected override void EngageState()
    {
        if (VideoClip != null)
        {
            VideoPlayer.transform.SetPositionAndRotation(
                    Cam.Io.Camera.transform.position + Cam.Io.Camera.transform.forward,
                    Cam.Io.Camera.transform.rotation
            );
            VideoPlayer.Play();
        }
        return;
    }

    protected override void DisengageState()
    {
        GameObject.Destroy(_videoPlayer.gameObject);
    }

    protected override void GPInput(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.South_Press:
                SetState(SubsequentState);
                return;

            case GamePadButton.North_Press:
                if (VideoPlayer.isPaused) VideoPlayer.Play();
                else VideoPlayer.Pause();
                return;

            case GamePadButton.Left_Press:
                VideoPlayer.time -= 5;
                return;

            case GamePadButton.Right_Press:
                VideoPlayer.time += 5;
                return;
        }
    }

    private VideoPlayer _videoPlayer;
    public VideoPlayer VideoPlayer => _videoPlayer = _videoPlayer != null ? _videoPlayer : SetUpVideo();
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
}
