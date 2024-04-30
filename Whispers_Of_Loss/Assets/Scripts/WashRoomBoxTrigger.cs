using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WashRoomBoxTrigger : MonoBehaviour
{
    [SerializeField] GameObject blackImage;
    public float enableFireDelayTime = 3f;
    public float timeBeforeDoorOpens = 5f;
    public GameObject fireObj;
    public GameObject blackImg;
    public DoorScript washRoomDoor;
    public DoorScript roomDoor;
    public GameObject livingRoomBarrier;
    public UnityEvent OnPlayerEnter;
    public AudioSource runWhispersSource;
    public AudioSource screamingPeople;
    bool hasEntered;
    public PlayerMovement playerMovement;
    public float newMoveSpeed = 2f;
    float initMoveSpeed;

    public GuidingHopeScript guideScript;
    public float timeToEnableGuideScene = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if(hasEntered) { return; }
        if(other.GetComponent<Player>() != null)
        {
            hasEntered = true;
            washRoomDoor.SetOpenOrCloseDoor(true);
            livingRoomBarrier.SetActive(true);
            roomDoor.SetOpenOrCloseDoor(true);
            StartCoroutine(EnableFire());
            playerMovement.moveSpeed = initMoveSpeed;
            playerMovement.moveSpeed = newMoveSpeed;
        }
    }

    IEnumerator EnableFire()
    {
        yield return new WaitForSeconds(2);

        runWhispersSource.Play();

        yield return new WaitForSeconds(enableFireDelayTime);
        screamingPeople.Play();
        blackImg.SetActive(false);
        fireObj.SetActive(true);

        PostProcessingController.instance?.DarkenEnvironment(true);

        yield return new WaitForSeconds(timeBeforeDoorOpens);
        washRoomDoor.SetOpenOrCloseDoor(false);

        yield return new WaitForSeconds(timeToEnableGuideScene);
        guideScript.EnableGuidingScene();
    }
}
