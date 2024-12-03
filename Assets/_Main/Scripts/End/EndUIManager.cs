using _Main.Scripts.InGameUI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Main.Scripts.End
{
    public class EndUIManager : MonoSingleton<EndUIManager>
    {
        [SerializeField] private CanvasGroup fadeTransition;
        [SerializeField] private float fadeTransitionTime;
        [SerializeField] private GameObject standingTxtGameObject;
        [SerializeField] private TextMeshProUGUI standingTxt;

        public void EnableEndUI()
        {
            standingTxt.text = InGameUIManager.Instance.StandingTxt.text;
            InGameUIManager.Instance.gameObject.SetActive(false);
            standingTxtGameObject.SetActive(true);
            fadeTransition.gameObject.SetActive(true);
            fadeTransition.DOFade(1f, fadeTransitionTime).SetEase(Ease.Linear).OnComplete(()=>
                {
                    SceneManager.LoadScene(3);
                });
        }
    }
}