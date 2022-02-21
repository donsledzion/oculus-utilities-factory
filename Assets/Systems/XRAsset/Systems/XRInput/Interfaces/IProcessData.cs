namespace XRInputManager
{
    public partial class XRInput
    {
        public interface IProcessData
        {
            void ProcessData(ControllerInputData inputData);
        }
    }
}