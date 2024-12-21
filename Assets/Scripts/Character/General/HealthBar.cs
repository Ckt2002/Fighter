using DG.Tweening;
using System;
using UnityEngine;

namespace Character.General
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject healthBar;
        [SerializeField] private Transform healthValue;
        [SerializeField] private Transform healthBackground;
        [SerializeField] private float duration;

        private Tweener healthBackgroundTween;

        public void UpdateHealthValue(float value)
        {
            var localScaleHealthValue = healthValue.localScale;
            localScaleHealthValue.x = value;
            healthValue.localScale = localScaleHealthValue;

            if (Math.Abs(healthBackground.localScale.x - localScaleHealthValue.x) == 0)
                return;

            if (healthBackgroundTween != null && healthBackgroundTween.IsActive())
            {
                healthBackgroundTween.Kill();
            }

            healthBackgroundTween = healthBackground.DOScaleX(localScaleHealthValue.x, duration);
        }

        public void ShowHealthBar(bool show)
        {
            healthBar.SetActive(show);
        }
    }
}
