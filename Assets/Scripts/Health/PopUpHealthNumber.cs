using TMPro;
using UnityEngine;

namespace Fighting
{
    public class PopUpHealthNumber : MonoBehaviour
    {
        public float liveTime = 1f;
        public Color damageColor = new Color(0.75f, 0f, 0f, 0.7f);
        public Color healColor = new Color(0f, 0.75f, 0.04f, 0.77f);
        public string damageFormat = "-{0}";
        public string healFormat = "+{0}";
        [SerializeField] private TextMeshProUGUI changeText;

        private float _changeAmount;

        public void SetUp(float change)
        {
            _changeAmount = change;
            if (change < 0) SetDamage();
            else SetHeal();
            Destroy(gameObject, liveTime);
        }

        private void SetDamage()
        {
            SetTextParams(damageColor, damageFormat, _changeAmount);
        }

        private void SetHeal()
        {
            SetTextParams(healColor, healFormat, _changeAmount);
        }

        private void SetTextParams(Color color, string textFormat, float change)
        {
            changeText.color = color;
            changeText.text = string.Format(textFormat, change);
        }
    }
}