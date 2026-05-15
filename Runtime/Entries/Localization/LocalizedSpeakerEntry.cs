#if UNITY_LOCALIZATION
using Aarthificial.Typewriter.Entries;
using System;
using UnityEngine.Localization;

namespace Aarthificial.Typewriter.Entries {
  /// <summary>
  /// An fact representing a speaker.
  /// </summary>
  /// <remarks>
  /// Typewriter entries can be extended to store additional information specific
  /// to your game.
  /// </remarks>
  [Serializable]
  public class LocalizedSpeakerEntry : FactEntry {
    /// <summary>
    /// The speaker's name.
    /// </summary>
    public LocalizedString DisplayName = new();

    public override string ToString() {
      return $"{DisplayName} ({ID})";
    }
  }
}
#endif
