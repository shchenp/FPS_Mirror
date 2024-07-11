// Copyright 2021, Infima Games. All Rights Reserved.

using Mirror;
using VContainer;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Interface Element.
    /// </summary>
    public abstract class Element : NetworkBehaviour
    {
        #region FIELDS
        
        /// <summary>
        /// Player Character.
        /// </summary>
        protected CharacterBehaviour playerCharacter;
        /// <summary>
        /// Player Character Inventory.
        /// </summary>
        protected InventoryBehaviour playerCharacterInventory;

        /// <summary>
        /// Equipped Weapon.
        /// </summary>
        protected WeaponBehaviour equippedWeapon;
        
        #endregion

        #region UNITY

        /// <summary>
        /// Update.
        /// </summary>
        private void Update()
        {
            //Ignore if we don't have an Inventory.
            if (Equals(playerCharacterInventory, null))
                return;

            //Get Equipped Weapon.
            equippedWeapon = playerCharacterInventory.GetEquipped();
            
            //Tick.
            Tick();
        }

        #endregion

        #region METHODS
        
        public void Initialize(CharacterBehaviour player)
        {
            playerCharacter = player;
            playerCharacterInventory = playerCharacter.GetInventory();
        }

        /// <summary>
        /// Tick.
        /// </summary>
        protected virtual void Tick() {}

        #endregion
    }
}