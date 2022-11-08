using System;

namespace Filter_Frequency_Response_Visualizer
{
    internal class InputDataProcessor
    {
        #region Properties
        public int[,] inputIntArray { get; set; } // Using [time, value] formatting

        public double[,] inputDoubleArray { get; set; } // Using [time, value] formatting conversion

        #endregion

        #region Constructor
        public void processor(int numElements)
        {
            this.inputIntArray = new int[numElements, 2];
            this.inputDoubleArray = new double[numElements, 2];
        }
        #endregion

        #region Methods
        public void ScaleValues(int scaleRange, double mag)
        {
            double rangeDouble = Convert.ToDouble(scaleRange);

            for (int i = 0; i < this.inputIntArray.Length / 2; i++)
            {
                double sampleDouble = Convert.ToDouble(this.inputIntArray[i, 1]);

                this.inputDoubleArray[i, 1] = (mag * sampleDouble) / rangeDouble;
            }
        }

        public void ZeroTimes()
        {
            int baseTime = this.inputIntArray[0, 0];

            for (int i = 0; i < this.inputIntArray.Length / 2; i++)
            {
                int rawTimeMicroSec = this.inputIntArray[i, 0] - baseTime;
                double timeMSec = 0.001 * Convert.ToDouble(rawTimeMicroSec);
                this.inputDoubleArray[i, 0] = timeMSec;
            }
        }

        #endregion

    }
}
