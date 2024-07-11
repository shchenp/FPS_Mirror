using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using InfimaGames.LowPolyShooterPack.Interface;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private List<Element> _elements;

    public void Initialize(CharacterBehaviour player)
    {
        foreach (var element in _elements)
        {
            element.Initialize(player);
        }
    }
}
