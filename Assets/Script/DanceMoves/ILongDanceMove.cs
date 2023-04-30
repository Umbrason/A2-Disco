public interface ILongDanceMove : IDanceMove {
    /// <summary>
    /// The estimated duration used when a gathering is triggered.
    /// Use values <= 0 to prevent gatherings (this is the default).
    /// </summary>
    float GatherDuration => 0f;
}
