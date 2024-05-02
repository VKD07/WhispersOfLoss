using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPickUpPills : MonoBehaviour
{
    [SerializeField] GameObject pickUpUI;
    [SerializeField] GameObject fire;
    [SerializeField] AudioSource ScreamingPeopleWhispers;
    [SerializeField] GameObject pointLight, pills1, pills2;
    [SerializeField] float fadeOutSpeed = 10f;
    public string sceneName;
    bool hasntPickedUp;
    bool hasEntered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null && !hasntPickedUp)
        {
            pickUpUI.SetActive(true);
            hasEntered = true;
        }
    }

    private void Update()
    {
        PlayerPicksUpThePills();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            hasEntered = false;
            pickUpUI.SetActive(false);
        }
    }

    void PlayerPicksUpThePills()
    {
        if (Input.GetKeyDown(KeyCode.E) && hasEntered)
        {
            PostProcessingController.instance?.DarkenEnvironment(false);
            pointLight.SetActive(false);
            pills1.SetActive(false);
            pills2.SetActive(false);

            pickUpUI.SetActive(false);
            hasntPickedUp = true;
            StartCoroutine(FadeOutSound());
            fire.SetActive(false);
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator FadeOutSound()
    {
        while (ScreamingPeopleWhispers.volume > 0)
        {
            ScreamingPeopleWhispers.volume -= Time.deltaTime * fadeOutSpeed;
            yield return null;
        }
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(2);
    }
}
