using LoginAnomalyDetector.Common;
using LoginAnomalyDetector.ML.Base;
using LoginAnomalyDetector.ML.Objects;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LoginAnomalyDetector.ML
{
    //We need to make use of the Randomized PCA Trainer
    public class Trainer:BaseML
    {

        //Using GetDataView Helper Method which builds the IDataView data view from the columns defined in LoginHistory class
        private (IDataView DataView , IEstimator<ITransformer> Transformer) GetDataView(string fileName, bool training=true)
        {
            var trainingDataView = MlContext.Data.LoadFromTextFile<LoginHistory>(fileName, ',');
            if(!training)
            {
#pragma warning disable 8619
                return (trainingDataView, null);
            }
            IEstimator<ITransformer> dataProcessPipeLine = MlContext.Transforms.Concatenate(FEATURES, typeof(LoginHistory).ToPropertyList<LoginHistory>(nameof(LoginHistory.Label)));
            return (trainingDataView, dataProcessPipeLine);
        }

        public void Train(string trainingFileName, string testFileName)
        {
            // Check if training data exists
            if (!File.Exists(trainingFileName))
            {
                Console.WriteLine($"Failed to find the training data file {trainingFileName}");
                return;
            }

            //Ensure that the test fileName exists
            if (!File.Exists(testFileName))
            {
                Console.WriteLine($"Failed to find test data file ({testFileName}");
                return;
            }


            //We then build the training data view and the RandomizedPcaTrainer.Options Object:

            var trainingDataView = GetDataView(trainingFileName);

            var options = new RandomizedPcaTrainer.Options
            {
                FeatureColumnName = FEATURES,
                ExampleWeightColumnName = null,
                Rank=5, //Rank property must be equal to or less than the features
                Oversampling=20,
                EnsureZeroMean=true,
                Seed=1

            };


            //We can then create the Randomized PCA trainer
            IEstimator<ITransformer> trainer = MlContext.AnomalyDetection.Trainers.RandomizedPca(options: options);

            //Append it to the training data/pipeline
            EstimatorChain<ITransformer> trainingPipeLine=trainingDataView.Transformer.Append(trainer);

            //View, Fit the model and save it
            TransformerChain<ITransformer> trainedModel = trainingPipeLine.Fit(trainingDataView.DataView);

            //Save the model

            MlContext.Model.Save(trainedModel, trainingDataView.DataView.Schema,ModelPath);


            //Now we evaluate the model we just trained using the testing dataset

            var testingDataView = GetDataView(testFileName, true);

            var testSetTransform=trainedModel.Transform(testingDataView.DataView);

            var modelMetrics=MlContext.AnomalyDetection.Evaluate(testSetTransform);

            //Finally we output the classification metrics


            Console.WriteLine($"Area Under Curve: {modelMetrics.AreaUnderRocCurve:P2}{Environment.NewLine}" +
                              $"Detection at FP Count: {modelMetrics.DetectionRateAtFalsePositiveCount}");










        }
    }
}
