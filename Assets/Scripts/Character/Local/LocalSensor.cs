using UnityEngine;

namespace Character.Local
{
    public class LocalSensor : MonoBehaviour
    {
        private int colCount;

        private void Start()
        {
            colCount = 0;
        }

        public bool State()
        {
            return colCount > 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            colCount++;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            colCount--;
        }
    }
}
