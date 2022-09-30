using UnityEngine;
using UnityEngine.UI;

public class CanvasComponents : MonoBehaviour
{
        [SerializeField] private Button leftButton;
        [SerializeField] private Button forwardButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Image leftArrow;
        [SerializeField] private Image forwardArrow;
        [SerializeField] private Image rightArrow;

        public Button GetLeftButton => leftButton;
        public Button GetForwardButton => forwardButton;
        public Button GetRightButton => rightButton;
        public Image GetLeftArrow => leftArrow;
        public Image GetForwardArrow => forwardArrow;
        public Image GetRightArrow => rightArrow;
}