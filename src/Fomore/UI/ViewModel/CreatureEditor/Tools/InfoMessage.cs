using System;
using System.Windows.Media;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class InfoMessage
    {
        public string Message { get; }
        public TimeSpan Duration { get; }
        public Brush Brush { get; }

        public InfoMessage(string message, TimeSpan duration) : this(message, duration, Brushes.Black) { }

        public InfoMessage(string message, TimeSpan duration, Brush brush)
        {
            Message = message;
            Duration = duration;
            Brush = brush;
        }

        public bool Equals(InfoMessage other) => string.Equals(Message, other.Message) && Duration.Equals(other.Duration) && Equals(Brush, other.Brush);

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is InfoMessage && Equals((InfoMessage)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Message != null ? Message.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Duration.GetHashCode();
                hashCode = (hashCode * 397) ^ (Brush != null ? Brush.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}