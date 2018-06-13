using System.Windows;

namespace Fomore.UI.ViewModel.Commands
{
    public struct SizeChange
    {
        public Size OldSize { get; }
        public Size NewSize { get; }

        public SizeChange(Size oldSize, Size newSize)
        {
            OldSize = oldSize;
            NewSize = newSize;
        }
    }

    public struct MouseInfo
    {
        public Point AbosultePosition { get; }
        public Point RelativePosition { get; }
        public bool LeftMouseButtonDown { get; }
        public bool MiddleMouseButtonDown { get; }
        public bool RightMouseButtonDown { get; }
        public int Timestamp { get; }

        public MouseInfo(Point absoultePosition,
                         Point relativePosition,
                         bool leftMouseButtonDown,
                         bool middleMouseButtonDown,
                         bool rightMouseButtonDown,
                         int timestamp)
        {
            AbosultePosition = absoultePosition;
            RelativePosition = relativePosition;
            LeftMouseButtonDown = leftMouseButtonDown;
            MiddleMouseButtonDown = middleMouseButtonDown;
            RightMouseButtonDown = rightMouseButtonDown;
            Timestamp = timestamp;
        }
    }

    public struct MouseWheelInfo
    {
        public Point AbosultePosition { get; }
        public Point RelativePosition { get; }
        public double MouseWheelDelta { get; }
        public bool LeftMouseButtonDown { get; }
        public bool MiddleMouseButtonDown { get; }
        public bool RightMouseButtonDown { get; }
        public int Timestamp { get; }

        public MouseWheelInfo(Point absoultePosition,
                              Point relativePosition,
                              double mouseWheelDelta,
                              bool leftMouseButtonDown,
                              bool middleMouseButtonDown,
                              bool rightMouseButtonDown,
                              int timestamp)
        {
            AbosultePosition = absoultePosition;
            RelativePosition = relativePosition;
            MouseWheelDelta = mouseWheelDelta;
            LeftMouseButtonDown = leftMouseButtonDown;
            MiddleMouseButtonDown = middleMouseButtonDown;
            RightMouseButtonDown = rightMouseButtonDown;
            Timestamp = timestamp;
        }
    }
}