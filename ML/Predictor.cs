using LoginAnomalyDetector.ML.Base;
using LoginAnomalyDetector.ML.Objects;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoginAnomalyDetector.ML
{
    public class Predictor : BaseML
    {
        public void Predict(string inputDataFile)
        {
            if (!File.Exists(ModelPath))
            {
                ////Verifying if the model exists prior to reading it
                Console.WriteLine($"Failed to find model at {ModelPath}");
                return;

            }
            if (!File.Exists(inputDataFile))
            {
                //Verifying if the input file exists before making predictions on it 
                Console.WriteLine($"Failed to find input data at {inputDataFile}");
                return;
            }

            /*Loading the model  */
            //Then we define the ITransformer Object

            ITransformer mlModel;
            using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                mlModel = MlContext.Model.Load(stream, out _);
            }


            if (mlModel == null)
            {
                Console.WriteLine("Failed to load the model");
                return;
            }

            //Create our prediction Engine with the LoginHistory and LoginPrediction types;

            var predictionEngine = MlContext.Model.CreatePredictionEngine<LoginHistory, LoginPrediction>(mlModel);

            //Read in the file as Text 
            var json = File.ReadAllText(inputDataFile);


            //Call predict model on prediction engine class
            //Lastly, we run the prediction and then output the results of the model run
            //Deserialize the JSON into our LoginHistory Object
#pragma warning disable 8604
            var prediction = predictionEngine.Predict(JsonConvert.DeserializeObject<LoginHistory>(json));


            Console.WriteLine(
                                $"Based on input json:{System.Environment.NewLine}" +
                                $"{json}{System.Environment.NewLine}" +
                                $"The login history is {(prediction.PredictedLabel ? "abnormal" : "normal")}, with a {prediction.Score:F2} outlier score");



        }
    }
}
