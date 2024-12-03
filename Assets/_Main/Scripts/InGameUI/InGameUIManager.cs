using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Main.Scripts.InGameUI
{
    public class InGameUIManager : MonoSingleton<InGameUIManager>
    {
        [SerializeField] private SkillUIManager skillUIManager;
        [SerializeField] private TextMeshProUGUI speedTxt;
        [SerializeField] private TextMeshProUGUI gearTxt;
        [SerializeField] private TextMeshProUGUI standingTxt;
        [SerializeField] private TextMeshProUGUI countdownTxt;
        [SerializeField] private TextMeshProUGUI lapsTxt;
        [SerializeField] private GameObject wrongDirectionImage;
        [SerializeField] private Slider respawnSlider;
        [SerializeField] private EventTrigger lSidewayInputBtn;
        [SerializeField] private EventTrigger rSidewayInputBtn;
        [SerializeField] private EventTrigger brakeBtn;
        [SerializeField] private EventTrigger backBtn;

        public TextMeshProUGUI CountdownTxt => countdownTxt;

        public EventTrigger BackBtn => backBtn;

        public EventTrigger LSidewayInputBtn => lSidewayInputBtn;

        public EventTrigger RSidewayInputBtn => rSidewayInputBtn;

        public EventTrigger BrakeBtn => brakeBtn;

        public Slider RespawnSlider => respawnSlider;
        public TextMeshProUGUI SpeedTxt => speedTxt;
        public SkillUIManager SkillUIManager => skillUIManager;
        public TextMeshProUGUI GearTxt => gearTxt;

        public TextMeshProUGUI StandingTxt => standingTxt;

        public TextMeshProUGUI LapsTxt => lapsTxt;

        public GameObject WrongDirectionImage => wrongDirectionImage;
    }
}