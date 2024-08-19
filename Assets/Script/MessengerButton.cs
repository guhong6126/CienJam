using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerButton : MonoBehaviour
{
    [SerializeField] GameObject messenger;

    public void OnButtonClicked()
    {
        messenger.SetActive(true);
    }
}
