using UnityEngine.UI;
using UnityEngine;

namespace FelineFrenzy.UI
{
    /**
     *  A data structure that manipulates a float to keep record of a players stamina.
     * 
     *  @project Feline Frenzy.
     *  @author Thomas Jacobs.
     */
    [System.Serializable] public struct Stamina 
    {
        //Attributes:
        private float currentValue;
        private const float EMPTY = 0.0f;
        [SerializeField] private float maximumValue;

        //Constructor:
        public Stamina(float maximumValue)
        {
            this.maximumValue = maximumValue;
            currentValue = maximumValue;
        }

        //Properties:

        /**
         *  The level of stamina present.
         *  
         *  @returns A float representing the level of stamina present in structure.
         */
        public float Value { get => currentValue; }

        /**
         *  The maximum possible stamina value.
         *  The stamina will automatically be capped to the maximum value.
         * 
         *  @returns Float representing the maximum possible stamina value.
         */
        public float MaximumValue { get => maximumValue; }

        //Methods:

        /**
         *  The result of dividing the current stamina value by the maximum possible stamina value.
         * 
         *  @returns Float representing the percentage of stamina left in the form of a decimal value.
         */
        public float Decimal { get => currentValue / maximumValue; }

        public void Reset() => currentValue = maximumValue;
        public void Empty() => currentValue = default;

        /**
         *  Subtracts stamina by the value specified if enough stamina is present.
         * 
         *  @param 'substitution' : The value to be substituted from the present stamina value.
         *  @returns False if not enough stamina is available to complete substitution, True if substitution was successful.
         */
        public bool Subtract(float substitution)
        {
            if (currentValue - substitution < EMPTY) return false;

            currentValue -= substitution;

            return true;
        }

        public void Add(float addition)
        {
            currentValue += addition;
            if (currentValue > maximumValue) currentValue = maximumValue;
        }
    }

    /**
     *  A simple class for displaying a players stamina by manipulating a 'Slider' component.
     *  
     *  @owner Thomas Jacobs.
     *  @project Feline Frenzy.
     */
    [RequireComponent(typeof(Slider))] public class StaminaBar : MonoBehaviour
    {
        //Attributes:
        public Prototypes.Prototype__Controller target;
        private Slider slider;
        private const float minimum = 0.0f;
        private const float maximum = 1.0f;

        //Methods:
        private void Awake() => slider = GetComponent<Slider>();

        protected void LateUpdate()
        {
            if (slider == null || target == null) return;

            slider.maxValue = maximum;
            slider.minValue = minimum;
            slider.value = target.stamina.Decimal;
        }
    }
}