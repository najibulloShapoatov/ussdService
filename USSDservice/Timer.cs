using System;
using System.Threading;

namespace BabilonUSSD
{
    class Timer
    {
        // ────────────────────────── Enumerations ──────────────────────────

        // ────────────────────────── Events ──────────────────────────

        #region Delegates

        public delegate void ElapsedEventHandler(object sender, ElapsedEventArgs e);

        #endregion

        #region TimerElapseReentranceMode enum

        public enum TimerElapseReentranceMode
        {
            Reentrant,
            NonReentrant
        }

        #endregion

        #region TimerElapseStartMode enum

        public enum TimerElapseStartMode
        {
            Immediate,
            AfterInterval
        }

        #endregion

        // ────────────────────────── Private Fields ──────────────────────────

        private readonly System.Timers.Timer _timer = new System.Timers.Timer();
        private int _executing;

        // ────────────────────────── Constructor ──────────────────────────

        public Timer(double interval) :
            this(
            interval,
            TimerElapseStartMode.AfterInterval,
            TimerElapseReentranceMode.Reentrant)
        {
        }

        public Timer(
            double interval,
            TimerElapseStartMode startMode,
            TimerElapseReentranceMode reentranceMode)
        {
            _timer = new System.Timers.Timer(interval);
            _timer.Elapsed += OnElapsed;
            ElapseStartMode = startMode;
            ElapseReentranceMode = reentranceMode;
        }

        // ────────────────────────── Public Members ──────────────────────────

        public bool AutoReset
        {
            get { return _timer.AutoReset; }
            set { _timer.AutoReset = value; }
        }

        public bool Enabled
        {
            get { return _timer.Enabled; }
            set { _timer.Enabled = value; }
        }

        public double Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        public TimerElapseStartMode ElapseStartMode { get; set; }
        public TimerElapseReentranceMode ElapseReentranceMode { get; set; }
        public event ElapsedEventHandler Elapsed;

        public void Start()
        {
            if (ElapseStartMode == TimerElapseStartMode.Immediate)
                ThreadPool.QueueUserWorkItem(
                    state => Elapse(new ElapsedEventArgs()));
            Enabled = true;
        }

        public void BeginInit()
        {
            _timer.BeginInit();
        }

        public void Close()
        {
            _timer.Close();
        }

        public void EndInit()
        {
            _timer.EndInit();
        }

        public void Stop()
        {
            Enabled = false;
        }

        // ────────────────────────── Private Members ──────────────────────────

        private void OnElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Elapse(new ElapsedEventArgs(e));
        }

        private void Elapse(ElapsedEventArgs args)
        {
            if (ElapseReentranceMode == TimerElapseReentranceMode.NonReentrant && Interlocked.CompareExchange(ref _executing, 1, 0) == 1)
                return;

            if (Elapsed != null)
                Elapsed(this, args);

            _executing = 0;
        }

        // ────────────────────────── Nested Types ──────────────────────────

        #region Nested type: ElapsedEventArgs

        public class ElapsedEventArgs : EventArgs
        {
            private readonly DateTime _signalTime;

            public ElapsedEventArgs()
            {
                _signalTime = DateTime.Now;
            }

            public ElapsedEventArgs(System.Timers.ElapsedEventArgs args)
            {
                _signalTime = args.SignalTime;
            }

            public DateTime SignalTime
            {
                get { return _signalTime; }
            }
        }

        #endregion
    }
}