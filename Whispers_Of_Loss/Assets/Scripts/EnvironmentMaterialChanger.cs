using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMaterialChanger : MonoBehaviour
{
    public static EnvironmentMaterialChanger instance;
    public MaterialTexToToChange[] materialsToChange;
    public GameObject[] fire;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        SetInitTex();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeMaterialsTexture(true);
            for (int i = 0; i < fire.Length; i++)
            {
                fire[i].SetActive(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            ChangeMaterialsTexture(false);
            for (int i = 0; i < fire.Length; i++)
            {
                fire[i].SetActive(false);
            }
        }
    }

    void SetInitTex()
    {
        for (int i = 0; i < materialsToChange.Length; i++)
        {
            materialsToChange[i].initTex = materialsToChange[i].mat.GetTexture("_BaseColorMap");
        }
    }

    public void ChangeMaterialsTexture(bool val)
    {
        if (val)
        {
            for (int i = 0; i < materialsToChange.Length; i++)
            {
                materialsToChange[i].mat.SetTexture("_BaseColorMap", materialsToChange[i].texToReplace);
            }
        }
        else
        {
            for (int i = 0; i < materialsToChange.Length; i++)
            {
                materialsToChange[i].mat.SetTexture("_BaseColorMap", materialsToChange[i].initTex);
            }
        }
    }
}

[System.Serializable]
public class MaterialTexToToChange
{
    public Material mat;
    public Texture initTex;
    public Texture texToReplace;
}
