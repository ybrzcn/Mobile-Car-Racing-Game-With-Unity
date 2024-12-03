using System;
using UnityEngine;

namespace _Main.Scripts.StartLight
{
    public class StartLightManager : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] meshRenderers;
        
        [SerializeField] private Color redColor;
        [ColorUsage(true,true)][SerializeField] private Color redEmissionColor;
        [SerializeField] private Color yellowColor;
        [ColorUsage(true,true)][SerializeField] private Color yellowEmissionColor;
        [SerializeField] private Color greenColor;
        [ColorUsage(true,true)][SerializeField] private Color greenEmissionColor;
        
        public void ChangeColors(float timePercent)
        {
            var baseColor = Color.black;
            var emissionColor = Color.black;

            if (timePercent <= .5f)
            {
                baseColor = redColor;
                emissionColor = redEmissionColor;
            }

            else
            {
                baseColor = yellowColor;
                emissionColor = yellowEmissionColor;
            }
            
            
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.SetColor("_BaseColor", baseColor); 
                meshRenderer.material.SetColor("_EmissionColor", emissionColor); 
            }
        }

        public void MakeLightsGreen()
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.SetColor("_BaseColor", greenColor); 
                meshRenderer.material.SetColor("_EmissionColor", greenEmissionColor); 
            }
        }
        
        
    }
}