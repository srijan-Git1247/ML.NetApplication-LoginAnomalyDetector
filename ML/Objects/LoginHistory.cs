using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAnomalyDetector.ML.Objects
{
    //Container Class that contains the data to both predict and train our model.
    
    //Each property maps to a value that will be sent into the model for anomaly detection
    public class LoginHistory
    {
        [LoadColumn(0)] //The number in the LoadColumn decorator maps to the index in the CSV file
        public float UserID
        {
            get; set;
        }
        [LoadColumn(1)]
        public float CorporateNetwork
        {
            get;
            set;

        }

        [LoadColumn(2)]
        public float HomeNetwork
        {
            get;
            set;
        }
        [LoadColumn(3)]
        public float WithinWorkHours
        {
            get;set;
        }
        [LoadColumn(4)]
        public float WorkDay
        {
            get;
            set;
        }
        [LoadColumn(5)]
        public float Label
        {
            get;
            set;
        }

    }
}
