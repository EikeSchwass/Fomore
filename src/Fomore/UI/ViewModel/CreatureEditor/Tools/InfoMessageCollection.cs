using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class InfoMessageCollection : ViewModelBase
    {
        private List<InfoMessage> PrevShownMessages { get; } = new List<InfoMessage>();
        public ObservableCollection<InfoMessage> InfoMessages { get; }

        private Dictionary<Task, CancellationTokenSource> Tasks { get; } = new Dictionary<Task, CancellationTokenSource>();

        public DelegateCommand ResetShownMessagesCommand { get; }

        public DelegateCommand MarkEverythingReadCommand { get; }

        public bool IsEverythingRead { get; private set; }

        public InfoMessageCollection()
        {
            InfoMessages = new ObservableCollection<InfoMessage>();
            ResetShownMessagesCommand = new DelegateCommand(o => ResetShownMessages(), o => true);
            MarkEverythingReadCommand = new DelegateCommand(o => MarkEverythingRead(), o => true);
        }

        private void MarkEverythingRead()
        {
            IsEverythingRead = true;
            CancelAll();
        }

        private void ResetShownMessages()
        {
            PrevShownMessages.Clear();
            IsEverythingRead = false;
        }

        public void AddInfoMessage(InfoMessage infoMessage)
        {
            if (IsEverythingRead || PrevShownMessages.Any(m => string.Equals(m.Message, infoMessage.Message, StringComparison.Ordinal)))
                return;
            PrevShownMessages.Add(infoMessage);
            InfoMessages.Insert(0, infoMessage);
            var cancellationTokenSource = new CancellationTokenSource();
            var task = Task.Delay(infoMessage.Duration, cancellationTokenSource.Token)
                           .ContinueWith(t =>
                           {
                               if (Tasks.ContainsKey(t))
                                   Tasks.Remove(t);
                               RemoveMessage(infoMessage);
                           },
                                         TaskScheduler.FromCurrentSynchronizationContext());
            Tasks.Add(task, cancellationTokenSource);
        }

        private void RemoveMessage(InfoMessage infoMessage)
        {
            var message =
                InfoMessages.FirstOrDefault(m => m.Message == infoMessage.Message);
            if (message != null)
                InfoMessages.Remove(infoMessage);
        }

        public void CancelAll()
        {
            var tasks = Tasks.Keys.ToList();
            foreach (var task in tasks)
            {
                Tasks[task].Cancel();
                Tasks.Remove(task);
            }

            InfoMessages.Clear();
        }

        public bool HasShownMessage(InfoMessage infoMessage)
        {
            return PrevShownMessages.Any(m => string.Equals(m.Message, infoMessage.Message, StringComparison.Ordinal));
        }

        public void AddInfoMessageWithoutTracking(InfoMessage infoMessage)
        {
            InfoMessages.Insert(0,infoMessage);
            var task = Task.Delay(infoMessage.Duration)
                           .ContinueWith(t =>
                                         {
                                             InfoMessages.Remove(infoMessage);
                                             Tasks.Remove(t);
                                         },
                                         TaskScheduler.FromCurrentSynchronizationContext());
            Tasks.Add(task, new CancellationTokenSource());
        }
    }
}