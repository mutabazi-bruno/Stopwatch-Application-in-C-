using NUnit.Framework;
using StopwatchApp;

namespace StopwatchApp.Tests
{
    [TestFixture]
    public class StopwatchEngineTests
    {
        private StopwatchEngine _engine;

        [SetUp]
        public void Setup()
        {
            _engine = new StopwatchEngine();
        }

        // ---- Initial State ----

        [Test]
        public void NewEngine_ShouldStartAtZero()
        {
            Assert.That(_engine.ElapsedSeconds, Is.EqualTo(0));
            Assert.That(_engine.GetFormattedTime(), Is.EqualTo("00:00:00"));
        }

        [Test]
        public void NewEngine_ShouldNotBeRunning()
        {
            Assert.That(_engine.IsRunning, Is.False);
            Assert.That(_engine.IsPaused, Is.False);
        }

        // ---- Start ----

        [Test]
        public void Start_ShouldSetRunningToTrue()
        {
            _engine.Start();
            Assert.That(_engine.IsRunning, Is.True);
        }

        [Test]
        public void Start_ShouldResetElapsedToZero()
        {
            // simulate some time passing, then restart
            _engine.Start();
            _engine.Tick();
            _engine.Tick();
            _engine.Stop();

            _engine.Start();
            Assert.That(_engine.ElapsedSeconds, Is.EqualTo(0));
        }

        // ---- Tick ----

        [Test]
        public void Tick_WhenRunning_ShouldIncrementByOne()
        {
            _engine.Start();
            _engine.Tick();
            Assert.That(_engine.ElapsedSeconds, Is.EqualTo(1));
        }

        [Test]
        public void Tick_WhenNotStarted_ShouldNotIncrement()
        {
            _engine.Tick();
            Assert.That(_engine.ElapsedSeconds, Is.EqualTo(0));
        }

        [Test]
        public void Tick_WhenPaused_ShouldNotIncrement()
        {
            _engine.Start();
            _engine.Tick(); // 1 second
            _engine.Pause();
            _engine.Tick(); // should stay at 1
            Assert.That(_engine.ElapsedSeconds, Is.EqualTo(1));
        }

        // ---- Pause ----

        [Test]
        public void Pause_ShouldSetIsPausedTrue()
        {
            _engine.Start();
            _engine.Pause();
            Assert.That(_engine.IsPaused, Is.True);
        }

        [Test]
        public void Pause_WhenNotRunning_ShouldDoNothing()
        {
            _engine.Pause(); // shouldn't crash or change state
            Assert.That(_engine.IsPaused, Is.False);
        }

        // ---- Resume ----

        [Test]
        public void Resume_ShouldClearPausedFlag()
        {
            _engine.Start();
            _engine.Pause();
            _engine.Resume();
            Assert.That(_engine.IsPaused, Is.False);
        }

        [Test]
        public void Resume_ShouldAllowTickingAgain()
        {
            _engine.Start();
            _engine.Tick(); // 1
            _engine.Pause();
            _engine.Tick(); // still 1 (paused)
            _engine.Resume();
            _engine.Tick(); // now 2
            Assert.That(_engine.ElapsedSeconds, Is.EqualTo(2));
        }

        // ---- Reset ----

        [Test]
        public void Reset_ShouldBringTimeBackToZero()
        {
            _engine.Start();
            _engine.Tick();
            _engine.Tick();
            _engine.Tick();
            _engine.Reset();
            Assert.That(_engine.ElapsedSeconds, Is.EqualTo(0));
            Assert.That(_engine.GetFormattedTime(), Is.EqualTo("00:00:00"));
        }

        [Test]
        public void Reset_ShouldKeepRunningState()
        {
            _engine.Start();
            _engine.Tick();
            _engine.Reset();
            Assert.That(_engine.IsRunning, Is.True);
        }

        // ---- Stop ----

        [Test]
        public void Stop_ShouldSetRunningToFalse()
        {
            _engine.Start();
            _engine.Stop();
            Assert.That(_engine.IsRunning, Is.False);
        }

        [Test]
        public void Stop_ShouldRecordLastTime()
        {
            _engine.Start();
            _engine.Tick();
            _engine.Tick();
            _engine.Tick(); // 3 seconds
            _engine.Stop();
            Assert.That(_engine.LastRecordedTime, Is.EqualTo("00:00:03"));
        }

        [Test]
        public void Stop_WhenAlreadyStopped_ShouldDoNothing()
        {
            _engine.Stop(); // not running, shouldn't crash
            Assert.That(_engine.LastRecordedTime, Is.EqualTo("00:00:00"));
        }

        // ---- Time Formatting ----

        [Test]
        public void GetFormattedTime_ShouldShowCorrectMinutes()
        {
            _engine.Start();

            // run 65 ticks = 1 min 5 sec
            for (int i = 0; i < 65; i++)
                _engine.Tick();

            Assert.That(_engine.GetFormattedTime(), Is.EqualTo("00:01:05"));
        }

        [Test]
        public void GetFormattedTime_ShouldShowCorrectHours()
        {
            _engine.Start();

            // 3661 seconds = 1h 1m 1s
            for (int i = 0; i < 3661; i++)
                _engine.Tick();

            Assert.That(_engine.GetFormattedTime(), Is.EqualTo("01:01:01"));
        }

        [Test]
        public void GetFormattedTime_AtExactlyOneHour()
        {
            _engine.Start();

            for (int i = 0; i < 3600; i++)
                _engine.Tick();

            Assert.That(_engine.GetFormattedTime(), Is.EqualTo("01:00:00"));
        }
    }
}
