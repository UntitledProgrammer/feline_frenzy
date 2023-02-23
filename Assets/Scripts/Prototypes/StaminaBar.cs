using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FelineFrenzy.UI
{
    /**
     *  A data structure that manipulates a float to keep record of a players stamina.
     * 
     *  @project Feline Frenzy.
     *  @author Thomas Jacobs.
     */
    [System.Serializable] public class Stamina : Object
    {
        //Attributes:
        private float currentValue;
        [SerializeField] private float maximumValue;

        //Constructor:
        public Stamina(float maximumValue)
        {
            this.maximumValue = maximumValue;
            currentValue = maximumValue;
        }

        //Properties:
        public float Value { get => currentValue; }
        public float MaximumValue { get => maximumValue; }

        //Methods:
        public float Decimal { get => currentValue / maximumValue; }

        public void Reset() => currentValue = maximumValue;
        public bool Subtract(float substitution)
        {
            if (substitution > currentValue) return false;

            currentValue -= substitution;

            return true;
        }
        public void Add(float addition)
        {
            currentValue += addition;
            if (currentValue > addition) currentValue = maximumValue;
        }
    }


    [RequireComponent(typeof(Image))]
    public class StaminaBar : Image
    {
        //Attributes:
        public Prototypes.Prototype__Controller target;

        //Methods:
        protected override void Awake()
        {
            base.Awake();
            sprite = default;
            this.type = Type.Filled;
            this.fillMethod = FillMethod.Horizontal;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(StaminaBar))]
    public class EStaminaBar : Editor
    {
        //Attributes:
        private StaminaBar self;

        //Methods:
        private void OnEnable() => self = (StaminaBar)target;

        public override void OnInspectorGUI()
        {
            self.target = (Prototypes.Prototype__Controller)EditorGUILayout.ObjectField("Target", self.target, typeof(Prototypes.Prototype__Controller), true);
            self.sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", self.sprite, typeof(Sprite), true);
            self.color = EditorGUILayout.ColorField("Colour", self.color);

            serializedObject.Update();

            if (self.target) self.fillAmount = self.target.stamina.Decimal;
        }
    }

#endif
}