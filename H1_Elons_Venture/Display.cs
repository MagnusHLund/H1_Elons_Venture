using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_Elons_Venture
{
    /// <summary>
    /// This class has properties for the RC display
    /// </summary>
    internal class Display
    {
        private float _capacity;
        private float _distanceDriven;

        public float Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public float DistanceDriven
        {
            get { return _distanceDriven; }
            set { _distanceDriven = value; }
        }
    }
}
