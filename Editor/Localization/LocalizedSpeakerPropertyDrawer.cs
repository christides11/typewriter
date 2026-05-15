#if UNITY_LOCALIZATION
using Aarthificial.Typewriter.Editor.PropertyDrawers;
using Aarthificial.Typewriter.Entries;
using UnityEditor;
using UnityEngine.UIElements;

namespace Aarthificial.Typewriter.Editor.Localization {
  [CustomPropertyDrawer(typeof(LocalizedSpeakerEntry), true)]
  public class LocalizedSpeakerPropertyDrawer : BaseEntryPropertyDrawer {
    protected override void PopulateContent(
      VisualElement root,
      SerializedProperty property
    ) {
      root.Add(
        new LocalizedTextEditor(
          property.FindPropertyRelative(nameof(LocalizedSpeakerEntry.DisplayName))
        ) {
          multiline = false,
          style = {
            height = 20,
            marginBottom = 8,
            whiteSpace = WhiteSpace.Normal,
          },
        }
      );
      base.PopulateContent(root, property);
    }
  }
}
#endif
