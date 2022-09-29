using UnityEngine;
using UnityEngine.UI;

public class CanvasComponents : MonoBehaviour
{
        [SerializeField] private Button leftButton;
        [SerializeField] private Button forwardButton;
        [SerializeField] private Button rightButton;

        public Button GetLeftButton => leftButton;
        public Button GetForwardButton => forwardButton;
        public Button GetRightButton => rightButton;
}