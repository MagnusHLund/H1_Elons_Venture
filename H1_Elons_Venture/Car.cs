using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_Elons_Venture
{
    /// <summary>
    /// This class contains properties for the RC car
    /// </summary>
    internal class Car
    {
        private Color _color;
        private float _distanceDriven;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public float DistanceDriven
        {
            get { return _distanceDriven; }
            set { _distanceDriven = value; }
        }
    }
}
