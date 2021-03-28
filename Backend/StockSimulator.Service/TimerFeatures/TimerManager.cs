using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StockSimulator.Service.TimerFeatures
{
    public class TimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;
        public DateTime TimerStarted { get; }
        private int _period;

        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 3000);
            TimerStarted = DateTime.Now;
        }

        public TimerManager(Action action, int period)
        {
            _period = period;
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, (_period * 1000));
            TimerStarted = DateTime.Now;
        }

        public void Execute(object stateInfo)
        {
            _action();
            if ((DateTime.Now - TimerStarted).Seconds > (_period == 1 ? 0.5 : _period - 1) * 60)
            {
                _timer.Dispose();
            }
        }
    }
}
