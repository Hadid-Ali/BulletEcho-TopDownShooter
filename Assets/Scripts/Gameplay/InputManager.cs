using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   public static Action OnPrimaryWeaponDown;
   public static Action OnSecondaryWeaponDown;
   
   public static Action OnPrimaryWeaponUp;
   public static Action OnSecondaryWeaponUp;

   public static bool IsSprinting = false;

   public void StartSprint()
   {
      IsSprinting = true;
   }

   public void ReleaseSprint()
   {
      IsSprinting = false;
   }
   
   public void PrimaryWeaponFire()
   {
      OnPrimaryWeaponDown?.Invoke();
   }
   
   public void PrimaryWeaponUp()
   {
      OnPrimaryWeaponUp?.Invoke();
   }
   
   public void SecondaryWeaponFire()
   {
      OnSecondaryWeaponDown?.Invoke();
   }
   
   public void SecondaryWeaponUp()
   {
      OnSecondaryWeaponUp?.Invoke();
   }
}
