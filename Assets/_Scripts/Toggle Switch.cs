using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro; // For TextMeshPro

public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private RectTransform toggleIndicator;

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Color onColor;

    [SerializeField]
    private Color offColor;

    [SerializeField]
    private TMP_Dropdown dropdownOn;  // Dropdown for the ON state
    [SerializeField]
    private TMP_Dropdown dropdownOff; // Dropdown for the OFF state

    private bool _isOn = false;
    public bool isOn
    {
        get { return _isOn; }
    }

    private float offX;
    private float onX;

    [SerializeField]
    private float tweenTime = 0.25f;

    // Define the event to notify when the toggle state changes
    public delegate void ToggleStateChanged(bool value);
    public event ToggleStateChanged OnToggleStateChanged;

    void Start()
    {
        offX = toggleIndicator.anchoredPosition.x;
        onX = backgroundImage.rectTransform.rect.width / 2 - toggleIndicator.rect.width / 2;

        // Set the initial toggle state and display the appropriate dropdown
        Toggle(_isOn);
    }

    public void Toggle(bool value)
    {
        if (value != _isOn)
        {
            _isOn = value;
            MoveIndicator(_isOn);

            // Show/Hide the appropriate dropdown
            UpdateDropdownVisibility(_isOn);

            // Notify listeners (such as DropdownOptionsManager) of the state change
            OnToggleStateChanged?.Invoke(_isOn);
        }
    }

    private void MoveIndicator(bool value)
    {
        if (value)
            toggleIndicator.DOAnchorPosX(onX, tweenTime);
        else
            toggleIndicator.DOAnchorPosX(offX, tweenTime);
    }

    private void UpdateDropdownVisibility(bool isToggledOn)
    {
        // Show the ON dropdown and hide the OFF dropdown when toggle is ON
        if (dropdownOn != null && dropdownOff != null)
        {
            dropdownOn.gameObject.SetActive(isToggledOn);
            dropdownOff.gameObject.SetActive(!isToggledOn);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Toggle the state when clicked
        Toggle(!_isOn);
    }
}
