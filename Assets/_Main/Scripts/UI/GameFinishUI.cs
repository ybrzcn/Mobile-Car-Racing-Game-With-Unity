using System.Collections.Generic;
using _Main.Scripts.SRStanding;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> leaderboardTexts;
    [SerializeField] private List<Transform> showcaseModelsTransforms;

    private void Start()
    {
        UpdateLeaderboard();
        SpawnShowcaseModels();
    }
    void UpdateLeaderboard()
    {
        for (int i = 0; i < leaderboardTexts.Count; i++)
        {
            if (i < StandingManager.Instance.StandingList.Count)
            {
                if (StandingManager.Instance.StandingList[i].IsPlayerControlled)
                {
                    leaderboardTexts[i].text = $"{i + 1}. {StandingManager.Instance.StandingList[i].CarProps.CarName}" +" (YOU)";
                }

                else
                {
                    leaderboardTexts[i].text = $"{i + 1}. {StandingManager.Instance.StandingList[i].CarProps.CarName}" +" (AI)";
                }
            }
            else
            {
                leaderboardTexts[i].text = "";
            }
        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(StandingManager.Instance.gameObject);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }

    private void SpawnShowcaseModels()
    {
        for (int i = 0; i < showcaseModelsTransforms.Count; i++)
        {
            Instantiate(StandingManager.Instance.StandingList[i].CarProps.ShowcaseModel, showcaseModelsTransforms[i].position, showcaseModelsTransforms[i].rotation);
        }
    }
}
