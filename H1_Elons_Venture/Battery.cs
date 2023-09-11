using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_Elons_Venture
{
    /// <summary>
    /// This class contains properties for the RC car battery
    /// </summary>
    internal class Battery
    {
        private float _capacity;
        private float _batteryDrain;

        public float Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public float BatteryDrain
        {
            get { return _batteryDrain; }
            set { _batteryDrain = value; }
        }
    }
}
