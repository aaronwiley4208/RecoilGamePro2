using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface for gun upgrades. 
/// Upgrades have an effect on equip and the opposite effect on unequip.
/// </summary>
public interface IUpgrade {
    /// <summary>
    /// Effect of equipping an item to a gun
    /// </summary>
    void Equip();
    /// <summary>
    /// Effect of unequipping an item to a gun. Probably the opposite of equip()
    /// </summary>
    void Unequip();
    /// <summary>
    /// Sets the gun that the upgrade will affect.
    /// </summary>
    /// <param name="pistol"></param>
    void SetGun(Pistol pistol);
    /// <summary>
    /// Gets the gameobject for this upgrade
    /// </summary>
    /// <returns></returns>
    GameObject GetGameObject();
    /// <summary>
    /// Gets the prefab for the UI this upgrade should use.
    /// </summary>
    /// <returns></returns>
    GameObject GetUIPrefab();
    /// <summary>
    /// Return the value of the upgrade
    /// </summary>
    /// <returns></returns>
    float GetValue();
}
