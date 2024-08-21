using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAnomalyDetector.ML.Objects
{
    //This class contains the properties mapped to our prediction output
    public class LoginPrediction
    {
        public float Label; //Used for Evaluation
        public float Score; //Used for Evaluation
        public bool PredictedLabel; //This property will hold our Prediction
    }
}
