using Data;
using UnityEngine;

namespace Character.Remote
{
    public class SyncPosition : MonoBehaviour
    {
        public void SyncPos(PlayerState playerStates, SpriteRenderer spriteRenderer)
        {
            var charTransform = transform.parent.transform;
            var newPosition = charTransform.position;
            newPosition.x = playerStates.x;
            newPosition.y = playerStates.y;
            charTransform.position = newPosition;

            spriteRenderer.flipX = playerStates.flipX;
        }
    }
}
