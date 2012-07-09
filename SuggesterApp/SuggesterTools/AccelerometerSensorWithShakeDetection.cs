using System;
using Microsoft.Devices.Sensors;

namespace SuggesterTools
{
    /// <summary>
    /// 
    /// </summary>
    /// <references>Microsoft.Devices.Sensors</references>
    /// <see cref="http://mark.mymonster.nl/2010/10/24/shake-that-windows-phone-7-and-detect-it/"/>
    public class AccelerometerSensorWithShakeDetection : IDisposable
    {
        private readonly double _shakeThreshold;
        private readonly Accelerometer _sensor = new Accelerometer();
        private AccelerometerReadingEventArgs _lastReading;
        private int _shakeCount;
        private bool _shaking;

        public AccelerometerSensorWithShakeDetection()
            : this(0.4)
        {
        }
        public AccelerometerSensorWithShakeDetection(double shakeThreshold)
        {
            _shakeThreshold = shakeThreshold;
            var sensor = new Accelerometer();
            if (sensor.State == SensorState.NotSupported)
                throw new NotSupportedException("Accelerometer not supported on this device");
            _sensor = sensor;
        }

        public SensorState State
        {
            get { return _sensor.State; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_sensor != null)
                _sensor.Dispose();
        }

        #endregion

        private event EventHandler ShakeDetectedHandler;

        public event EventHandler ShakeDetected
        {
            add
            {
                ShakeDetectedHandler += value;
                _sensor.ReadingChanged += ReadingChanged;
            }
            remove
            {
                ShakeDetectedHandler -= value;
                _sensor.ReadingChanged -= ReadingChanged;
            }
        }

        public void Start()
        {
            if (_sensor != null)
                _sensor.Start();
        }

        public void Stop()
        {
            if (_sensor != null)
                _sensor.Stop();
        }

        private void ReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            //Code for checking shake detection
            if (_sensor.State == SensorState.Ready)
            {
                AccelerometerReadingEventArgs reading = e;
                try
                {
                    if (_lastReading != null)
                    {
                        if (!_shaking && CheckForShake(_lastReading, reading, _shakeThreshold) && _shakeCount >= 1)
                        {
                            //We are shaking
                            _shaking = true;
                            _shakeCount = 0;
                            OnShakeDetected();
                        }
                        else if (CheckForShake(_lastReading, reading, _shakeThreshold))
                        {
                            _shakeCount++;
                        }
                        else if (!CheckForShake(_lastReading, reading, 0.2))
                        {
                            _shakeCount = 0;
                            _shaking = false;
                        }
                    }
                    _lastReading = reading;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void OnShakeDetected()
        {
            if (ShakeDetectedHandler != null)
                ShakeDetectedHandler(this, EventArgs.Empty);
        }

        private static bool CheckForShake(AccelerometerReadingEventArgs last, AccelerometerReadingEventArgs current,
                                            double threshold)
        {
            double deltaX = Math.Abs((last.X - current.X));
            double deltaY = Math.Abs((last.Y - current.Y));
            double deltaZ = Math.Abs((last.Z - current.Z));

            return (deltaX > threshold && deltaY > threshold) ||
                    (deltaX > threshold && deltaZ > threshold) ||
                    (deltaY > threshold && deltaZ > threshold);
        }
    }
}
