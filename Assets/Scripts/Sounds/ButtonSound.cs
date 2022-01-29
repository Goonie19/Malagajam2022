using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    private Button _button;

    public FMODUnity.EventReference navigationSound;

    public FMODUnity.EventReference selectSound;

    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(PressedButton);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FMODUnity.RuntimeManager.PlayOneShot(navigationSound);
    }

    private void PressedButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot(selectSound);
    }

    public void OnSelect(BaseEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
