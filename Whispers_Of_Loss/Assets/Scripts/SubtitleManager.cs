using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subTitle;
    void Start()
    {
        subTitle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableSubTitle()
    {
        subTitle.gameObject.SetActive(true);
        StartCoroutine(SubTitle());
    }
    
    IEnumerator SubTitle()
    {
        subTitle.text = "Hey, It's okay.";

        yield return new WaitForSeconds(1.5f);
        subTitle.gameObject.SetActive(false);

        yield return new WaitForSeconds(2);
        subTitle.gameObject.SetActive(true);
        subTitle.text = "Focus.";
        
        yield return new WaitForSeconds(1f);
        subTitle.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        subTitle.gameObject.SetActive(true);
        subTitle.text = "It's not your fault";

        yield return new WaitForSeconds(1f);
        subTitle.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        subTitle.gameObject.SetActive(true);
        subTitle.text = "Find the light.";

        yield return new WaitForSeconds(1f);
        subTitle.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        subTitle.gameObject.SetActive(true);
        subTitle.text = "It's gonna be okay.";

        yield return new WaitForSeconds(3f);
        subTitle.gameObject.SetActive(false);
    }
}
