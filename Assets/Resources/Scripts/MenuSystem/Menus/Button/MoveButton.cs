using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public Direction direction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerController.Instance.MovePlayer(direction);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerController.Instance.StopPlayer();
    }
}
