using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public virtual void Collect()
    {
        gameObject.SetActive(false);
    }
}
