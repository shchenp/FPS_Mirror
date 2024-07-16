// Copyright 2021, Infima Games. All Rights Reserved.

using Mirror;
using UnityEngine.Events;

namespace InfimaGames.LowPolyShooterPack
{
    public class Inventory : InventoryBehaviour
    {
        public UnityEvent OnWeaponChanged;
        
        #region FIELDS
        
        /// <summary>
        /// Array of all weapons. These are gotten in the order that they are parented to this object.
        /// </summary>
        private WeaponBehaviour[] weapons;
        
        /// <summary>
        /// Currently equipped WeaponBehaviour.
        /// </summary>
        private WeaponBehaviour equipped;
        /// <summary>
        /// Currently equipped index.
        /// </summary>
        [SyncVar(hook = nameof(WeaponChanged))]
        private int equippedIndex = -1;

        #endregion
        
        #region METHODS
        
        public override void Init(int equippedAtStart = 0)
        {
            //Cache all weapons. Beware that weapons need to be parented to the object this component is on!
            weapons = GetComponentsInChildren<WeaponBehaviour>(true);
            
            //Disable all weapons. This makes it easier for us to only activate the one we need.
            foreach (WeaponBehaviour weapon in weapons)
                weapon.gameObject.SetActive(false);

            //Equip.
            Equip(equippedAtStart);
            
            WeaponChanged(equippedIndex, equippedAtStart);
        }
        
        protected override void Equip(int index)
        {
            //If we have no weapons, we can't really equip anything.
            if (weapons == null)
                return;
            
            //The index needs to be within the array's bounds.
            if (index > weapons.Length - 1)
                return;

            //No point in allowing equipping the already-equipped weapon.
            if (equippedIndex == index)
                return;

            //Update index.
            equippedIndex = index;
            
            //Update equipped.
            equipped = weapons[equippedIndex];
        }
        
        [Server]
        public override void ServerEquip(int index)
        {
            //If we have no weapons, we can't really equip anything.
            if (weapons == null)
                return;
            
            //The index needs to be within the array's bounds.
            if (index > weapons.Length - 1)
                return;

            //No point in allowing equipping the already-equipped weapon.
            if (equippedIndex == index)
                return;

            //Update index.
            equippedIndex = index;
        }

        private void WeaponChanged(int oldWeaponIndex, int newWeaponIndex)
        {
            //Disable the currently equipped weapon, if we have one.
            if (equipped != null)
                DisableWeapon(oldWeaponIndex);
            
            //Activate the newly-equipped weapon.
            EnableWeapon(newWeaponIndex);
            
            //Update equipped.
            equipped = weapons[newWeaponIndex];

            InvokeCallback();
        }

        [Command(requiresAuthority = false)]
        private void InvokeCallback()
        {
            OnWeaponChanged?.Invoke();
        }
        
        // Метод для включения оружия
        private void EnableWeapon(int index)
        {
            weapons[index].gameObject.SetActive(true);
        }

        // Метод для отключения оружия
        private void DisableWeapon(int index)
        {
            weapons[index].gameObject.SetActive(false);
        }
        
        #endregion

        #region Getters

        public override int GetLastIndex()
        {
            //Get last index with wrap around.
            int newIndex = equippedIndex - 1;
            if (newIndex < 0)
                newIndex = weapons.Length - 1;

            //Return.
            return newIndex;
        }

        public override int GetNextIndex()
        {
            //Get next index with wrap around.
            int newIndex = equippedIndex + 1;
            if (newIndex > weapons.Length - 1)
                newIndex = 0;

            //Return.
            return newIndex;
        }

        public override WeaponBehaviour GetEquipped() => equipped;
        public override int GetEquippedIndex() => equippedIndex;

        #endregion
    }
}