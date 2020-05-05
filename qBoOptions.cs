namespace Nop.Plugin.Widgets.qBoSlider
{
    /// <summary>
    /// Represents navigation elements displaying type
    /// </summary>
    public enum NavigationType : int
    {
        /// <summary>
        /// Never display navigation element
        /// </summary>
        None = 0,
        /// <summary>
        /// Display element on slider mouse drag event
        /// </summary>
        OnMouseDrag = 1,
        /// <summary>
        /// Always display navigation element
        /// </summary>
        Always = 2
    }

    public enum TransitionPlay : int
    {
        Random = 0,
        Sequence = 1
    }

    public enum DragOrientation : int
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2, 
        Both = 3
    }
}
