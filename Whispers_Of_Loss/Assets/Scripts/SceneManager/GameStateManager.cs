using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameStateManager : MonoBehaviour
{

    [Header("=== PLAYER WAKES UP ===")]
    public VideoPlayer videoPlayer;
    public GameObject VideoTexture;

    public GameObject player;
    public GameObject cameraHolder;

    public Animator wakeUpAnimation;

    public CinemachineBrain cinemachineBrain;
    public GameObject[] gameCameras;
    public float introVideoTime = 24f;
    public float timeDelayToActivatePlayer = 10f;
    public float faceRightCameraSpeed = 3f;

    PlayerWakesUpState playerWakesUpState = new PlayerWakesUpState();
    SceneState currentState;

    public PostProcessingController psController => GetComponent<PostProcessingController>();
    private void Start()
    {
        cameraHolder.SetActive(false);
        player.SetActive(false);
        StartCoroutine(PlayerWakesUp());
        wakeUpAnimation.enabled = false;
    }
    void Update()
    {
        if (currentState == null) return;
        currentState.OnUpdateState(this);
    }

    public void SwitchState(SceneState state)
    {
        currentState = state;
        state.OnEnterState(this);
    }

    IEnumerator PlayerWakesUp()
    {
        yield return new WaitForSeconds(introVideoTime);
        SwitchState(playerWakesUpState);
        VideoTexture.SetActive(false);
        AudioManager.instance?.PlaySound("PlayerWakesUp");
        wakeUpAnimation.enabled = true;
        yield return new WaitForSeconds(6);
        AudioManager.instance?.PlaySound("CharacterStandingUp");
    }
}
