using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDIR
{
    static class Utilities
    {
        public static double MeasureBufferDb(byte[] buffer)
        {
            double sum = 0;
            for (var i = 0; i < buffer.Count(); i = i + 2)
            {
                double sample = BitConverter.ToInt16(buffer, i) / 32768.0;
                sum += (sample * sample);
            }
            if (sum == 0) return 0;
            double rms = Math.Sqrt(sum / (buffer.Count() / 2));
            var decibel = 20 * Math.Log10(rms);
            return decibel;
        }
    }
}
