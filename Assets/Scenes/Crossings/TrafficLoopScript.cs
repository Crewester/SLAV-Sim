using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLoopScript : MonoBehaviour
{
    public Material RED;
    public Material AMBER;
    public Material GREEN;

    public bool Debug;

    int LightStage;

    int waitTime;

    public GameObject Walls;

    // Start is called before the first frame update
    void Start()
    {
        LightStage = 1;

        waitTime = 5;

        RED.EnableKeyword("_EMISSION");
        AMBER.DisableKeyword("_EMISSION");
        GREEN.DisableKeyword("_EMISSION");

        StartCoroutine(lightchanger());
    }

    // Update is called once per frame
    void Update()
    {
        switch (LightStage)
        {
            case 1: // Red light

                RED.EnableKeyword("_EMISSION");
                AMBER.DisableKeyword("_EMISSION");
                GREEN.DisableKeyword("_EMISSION");
                waitTime = 10;
                Walls.SetActive(true);
                if (Debug == true)
                {
                    print("Red");
                }

                break;

            case 2: // going green
                RED.EnableKeyword("_EMISSION");
                AMBER.EnableKeyword("_EMISSION");
                GREEN.DisableKeyword("_EMISSION");
                waitTime = 1;
                Walls.SetActive(true);
                if (Debug == true)
                {
                    print("Going Green");
                }

                break;

            case 3: // green
                RED.DisableKeyword("_EMISSION");
                AMBER.DisableKeyword("_EMISSION");
                GREEN.EnableKeyword("_EMISSION");
                waitTime = 10;
                Walls.SetActive(false);
                if (Debug == true)
                {
                    print("Green");
                }

                break;

            case 4: // going red
                RED.DisableKeyword("_EMISSION");
                AMBER.EnableKeyword("_EMISSION");
                GREEN.DisableKeyword("_EMISSION");
                waitTime = 1;
                Walls.SetActive(true);
                if (Debug == true)
                {
                    print("Going Red");
                }

                break;

        }

    }

    IEnumerator lightchanger()
    {
        yield return new WaitForSeconds(waitTime);

        LightStage++;

        if (LightStage > 4)
        {
            LightStage = 1;
        }

        StartCoroutine(lightchanger());
    }
}
