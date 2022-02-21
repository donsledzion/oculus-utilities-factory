namespace XRInputManager
{
    public partial class XRInput
    {
        /// <summary>
        /// Dostepne (obsłużone przez XRInput) przyciski na kontrolerach
        /// </summary>
        public enum XRInputs
        {
            Trigger,        // bool
            Grip,           // bool
            Joystick,       // bool
            JoystickLeft,   // bool
            JoystickRight,  // bool
            JoystickUp,     // bool
            JoystickDown,   // bool
            JoystickAxis,   // Vector2
            TriggerFloat,   // float
            GripFloat,      // float
            One,            // bool
            Two             // bool
        }
    }
}