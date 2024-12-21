using UnityEngine;

namespace Data
{
    public class CharacterRawImage : MonoBehaviour
    {
        [SerializeField] private GameObject character;

        public GameObject SlotCharacter => character;
    }
}
