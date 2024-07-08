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
        private GameObject canvasPrefab;

        #endregion

        #region UNITY FUNCTIONS

        public override void OnStartLocalPlayer()
        {
            //Spawn Interface.
            Instantiate(canvasPrefab);
        }

        #endregion
    }
}