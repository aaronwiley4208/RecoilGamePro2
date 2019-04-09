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
    void Unequip();
}
