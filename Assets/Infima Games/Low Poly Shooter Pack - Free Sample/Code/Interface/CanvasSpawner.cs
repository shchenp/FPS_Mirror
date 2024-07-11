// Copyright 2021, Infima Games. All Rights Reserved.

using Mirror;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Player Interface.
    /// </summary>
    public class CanvasSpawner : NetworkBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Header("Settings")]
        
        [Tooltip("Canvas prefab spawned at start. Displays the player's user interface.")]
        [SerializeField]
        private CanvasManager _canvasPrefab;

        [SerializeField] 
        private CharacterBehaviour _player;

        #endregion

        #region UNITY FUNCTIONS

        public override void OnStartLocalPlayer()
        {
            //Spawn Interface.
            var canvas = Instantiate(_canvasPrefab);
            canvas.Initialize(_player);
        }

        #endregion
    }
}