using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkMethods;

namespace Gangwar
{
    // public class Timer
    // {
    //     private static readonly List<Timer> _timer = new List<Timer>();
    //
    //     private static readonly List<Timer> _insertAfterList = new List<Timer>();
    //
    //     private static Action<string> _logger;
    //
    //     private static Func<ulong> _tickGetter;
    //
    //     public Action Func;
    //
    //     public readonly uint ExecuteAfterMs;
    //
    //     private ulong _executeAtMs;
    //
    //     public uint ExecutesLeft;
    //
    //     public bool HandleException;
    //
    //     private bool _willRemoved = false;
    //
    //     public bool IsRunning => !_willRemoved;
    //
    //     public ulong RemainingMsToExecute => _executeAtMs - _tickGetter();
    //
    //     public static void Init(Action<string> thelogger, Func<ulong> theTickGetter)
    //     {
    //         _logger = thelogger;
    //         _tickGetter = theTickGetter;
    //     }
    //
    //     public Timer(Action thefunc, uint executeafterms, uint executes = 1, bool handleexception = true)
    //     {
    //         ulong executeatms = executeafterms + _tickGetter();
    //         Func = thefunc;
    //         ExecuteAfterMs = executeafterms;
    //         _executeAtMs = executeatms;
    //         ExecutesLeft = executes;
    //         HandleException = handleexception;
    //         _insertAfterList.Add(this);
    //     }
    //
    //     public void Kill()
    //     {
    //         _willRemoved = true;
    //     }
    //
    //     private void ExecuteMe()
    //     {
    //         Func();
    //         if (ExecutesLeft == 1)
    //         {
    //             ExecutesLeft = 0;
    //             _willRemoved = true;
    //         }
    //         else
    //         {
    //             if (ExecutesLeft != 0)
    //                 ExecutesLeft--;
    //             _executeAtMs += ExecuteAfterMs;
    //             _insertAfterList.Add(this);
    //         }
    //     }
    //
    //     private void ExecuteMeSafe()
    //     {
    //         try
    //         {
    //             Func();
    //         }
    //         catch (Exception ex)
    //         {
    //             _logger?.Invoke(ex.ToString());
    //         }
    //         finally
    //         {
    //             if (ExecutesLeft == 1)
    //             {
    //                 ExecutesLeft = 0;
    //                 _willRemoved = true;
    //             }
    //             else
    //             {
    //                 if (ExecutesLeft != 0)
    //                     ExecutesLeft--;
    //                 _executeAtMs += ExecuteAfterMs;
    //                 _insertAfterList.Add(this);
    //             }
    //         }
    //     }
    //
    //     public void Execute(bool changeexecutems = true)
    //     {
    //         if (changeexecutems)
    //         {
    //             _executeAtMs = _tickGetter();
    //         }
    //         if (HandleException)
    //             ExecuteMeSafe();
    //         else
    //             ExecuteMe();
    //     }
    //
    //     private void InsertSorted()
    //     {
    //         bool putin = false;
    //         for (int i = _timer.Count - 1; i >= 0 && !putin; i--)
    //             if (_executeAtMs <= _timer[i]._executeAtMs)
    //             {
    //                 _timer.Insert(i + 1, this);
    //                 putin = true;
    //             }
    //
    //         if (!putin)
    //             _timer.Insert(0, this);
    //     }
    //
    //     public static void OnUpdateFunc()
    //     {
    //         try
    //         {
    //             ulong tick = _tickGetter();
    //             for (int i = _timer.Count - 1; i >= 0; i--)
    //             {
    //                 if (!_timer[i]._willRemoved)
    //                 {
    //                     if (_timer[i]._executeAtMs <= tick)
    //                     {
    //                         var thetimer = _timer[i];
    //                         _timer.RemoveAt(i);
    //                         if (thetimer.HandleException)
    //                             thetimer.ExecuteMeSafe();
    //                         else
    //                             thetimer.ExecuteMe();
    //                     }
    //                     else
    //                         break;
    //                 }
    //                 else
    //                     _timer.RemoveAt(i);
    //             }
    //
    //             if (_insertAfterList.Count > 0)
    //             {
    //                 foreach (var timer in _insertAfterList)
    //                 {
    //                     timer.InsertSorted();
    //                 }
    //                 _insertAfterList.Clear();
    //             }
    //
    //         }
    //         catch (Exception ex)
    //         {
    //             Core.SendConsoleMessage("Timer.cs:OnUpdateFunc: " + ex.Message);
    //             Core.SendConsoleMessage("Timer.cs:OnUpdateFunc: " + ex.StackTrace);
    //         }
    //     }
    // }

    public class Timer
    {
        private static List<Timer> _timers = new List<Timer>();

        private static Action<string> _logger;
        private static Func<ulong> _tickGetter;

        public string Name;
        public Action Func;
        public readonly uint ExecuteAfterMs;
        public uint ExecutesLeft;

        private DateTime _executeAtMs;
        public ulong RemainingMsToExecute => (ulong) (_executeAtMs - DateTime.Now).Milliseconds;

        public static void Init(Action<string> thelogger, Func<ulong> theTickGetter)
        {
            _logger = thelogger;
            _tickGetter = theTickGetter;
        }

        public Timer(Action thefunc, uint executeafterms, string name, uint executes = 1)
        {
            ulong executeatms = executeafterms + _tickGetter();
            Func = thefunc;
            ExecuteAfterMs = executeafterms;
            _executeAtMs = DateTime.Now.AddMilliseconds(executeafterms);
            ExecutesLeft = executes;
            _timers.Add(this);
        }

        public void Kill()
        {
            _timers.Remove(this);
        }

        private void ExecuteMe()
        {
            try
            {
                Func();
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"Timer onExecute [{this.Name}]: " + ex.Message);
                Core.SendConsoleMessage($"Timer onExecute [{this.Name}]: " + ex.StackTrace);
            }
            finally
            {
                if (ExecutesLeft == 1)
                {
                    ExecutesLeft = 0;
                    _timers.Remove(this);
                }
                else
                {
                    if (ExecutesLeft != 0)
                        ExecutesLeft--;
                    _executeAtMs = DateTime.Now.AddMilliseconds(ExecuteAfterMs);
                }
            }
        }

        public static void OnUpdateFunc()
        {
            try
            {
                ulong tick = _tickGetter();
                for (int i = _timers.Count - 1; i >= 0; i--)
                {
                    if (_timers[i]._executeAtMs <= DateTime.Now)
                    {
                        _timers[i].ExecuteMe();
                        _timers = _timers.OrderBy(o => o._executeAtMs).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage("Timer OnUpdateFunc: " + ex.Message);
                Core.SendConsoleMessage("Timer OnUpdateFunc: " + ex.StackTrace);
            }
        }
     }
}