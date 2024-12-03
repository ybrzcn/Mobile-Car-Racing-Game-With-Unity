using System;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.UI
{
    public class SceneEntryFade : MonoBehaviour
    {
        [SerializeField] private CanvasGroup cg;
        [SerializeField] private float fadeTime;

        private void Start()
        {
            cg.gameObject.SetActive(true);
            cg.DOFade(0f, fadeTime).OnComplete(() => cg.gameObject.SetActive(false));
        }
    }
}