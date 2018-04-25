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
        StartCoroutine("MovePlayer");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine("MovePlayer");
    }
    
    IEnumerator MovePlayer()
    {
        while(true)
        {
            PlayerController.Instance.MovePlayer(direction);
            yield return null;
        }
    }
}
