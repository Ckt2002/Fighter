using Data;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(Item))]
    public class ItemEditor : UnityEditor.Editor
    {
        private SerializedProperty itemName;
        private SerializedProperty icon;

        private SerializedProperty itemType;

        private SerializedProperty armor;
        private SerializedProperty damage;
        private SerializedProperty speed;
        private SerializedProperty health;

        private SerializedProperty coins;


        private void OnEnable()
        {
            itemName = serializedObject.FindProperty("itemName");
            icon = serializedObject.FindProperty("icon");

            itemType = serializedObject.FindProperty("itemType");

            armor = serializedObject.FindProperty("armor");
            health = serializedObject.FindProperty("health");
            damage = serializedObject.FindProperty("damage");
            speed = serializedObject.FindProperty("speed");

            coins = serializedObject.FindProperty("coins");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(itemName);
            EditorGUILayout.PropertyField(icon);
            EditorGUILayout.PropertyField(itemType);

            ItemType type = (ItemType)itemType.enumValueIndex;

            switch (type)
            {
                case ItemType.Armor:
                case ItemType.Shield:
                case ItemType.Helmet:
                    damage.intValue = 0;
                    speed.intValue = 0;
                    EditorGUILayout.PropertyField(armor);
                    EditorGUILayout.PropertyField(health);
                    break;
                case ItemType.Weapon:
                    armor.intValue = 0;
                    health.intValue = 0;
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(speed);
                    break;
                case ItemType.Boot:
                    damage.intValue = 0;
                    armor.intValue = 0;
                    EditorGUILayout.PropertyField(health);
                    EditorGUILayout.PropertyField(speed);
                    break;
            }

            EditorGUILayout.PropertyField(coins);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
