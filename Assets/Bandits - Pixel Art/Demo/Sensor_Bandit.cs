using UnityEngine;

namespace Bandits___Pixel_Art.Demo
{
    public class SensorBandit : MonoBehaviour
    {
        private int mColCount = 0;

        private float mDisableTimer;

        private void OnEnable()
        {
            mColCount = 0;
        }

        public bool State()
        {
            if (mDisableTimer > 0)
                return false;
            return mColCount > 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            mColCount++;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            mColCount--;
        }

        private void Update()
        {
            mDisableTimer -= Time.deltaTime;
        }

        public void Disable(float duration)
        {
            mDisableTimer = duration;
        }
    }
}
