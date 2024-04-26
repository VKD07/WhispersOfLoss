using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWakesUpState : SceneState
{
    bool disableBlink;
    GameStateManager gameStateManager;
    public override void OnCollisionEnter(GameStateManager state)
    {
    }

    public override void OnEnterState(GameStateManager state)
    {
        gameStateManager = state;
        gameStateManager.psController.SetVigannete(1f);
        state.gameCameras[0].SetActive(false);
        state.StartCoroutine(ActivatePlayer(state, state.timeDelayToActivatePlayer));
        state.StartCoroutine(DisableBlink());
        AudioManager.instance?.PlayMusicSource();
    }

    public override void OnUpdateState(GameStateManager state)
    {
        if (disableBlink) return;
        state.psController.ViganetteBlink();
    }

    IEnumerator ActivatePlayer(GameStateManager state, float time)
    {
        yield return new WaitForSeconds(time);
        state.player.SetActive(true);
        state.cameraHolder.SetActive(true);
    }

    IEnumerator DisableBlink()
    {
        yield return new WaitForSeconds(15);
        gameStateManager.cinemachineBrain.m_DefaultBlend.m_Time = gameStateManager.faceRightCameraSpeed;
        disableBlink = true;
        gameStateManager.psController.SetVigannete(.1f);
    }
}
