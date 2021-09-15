/*
 * Created by aschia
 * Date Created: Sept 15
 * 
 * Last Edited: Sept 15
 * 
 * Desc: Game boundary stuff
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsLock : MonoBehaviour
{
    public Rect levelBounds;    // x,y,w,&h of the bounding rectangle

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,levelBounds.xMin,levelBounds.xMax), 
            transform.position.y, Mathf.Clamp(transform.position.z,levelBounds.yMin,levelBounds.yMax));
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 centerp = new Vector3(levelBounds.xMin + (levelBounds.width * 0.5f), 0.0f, levelBounds.yMin + (levelBounds.height * 0.5f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerp, new Vector3(levelBounds.width,0.0f,levelBounds.height));
    }
}
