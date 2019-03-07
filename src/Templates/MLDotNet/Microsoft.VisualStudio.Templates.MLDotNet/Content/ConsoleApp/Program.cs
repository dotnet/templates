/* 
 ***************************************************************************************************************************
 * This template shows the building blocks for training a machine learning model with ML.NET (https://aka.ms/mlnet).
 * This model predicts whether a sentence has a positive or negative sentiment. It is based on a sample that can be 
 * found at https://aka.ms/mlnetsentimentanalysis, which provides a more detailed introduction to ML.NET and the scenario. 
 ***************************************************************************************************************************
*/

using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Core.Data;
using Microsoft.Data.DataView;

namespace $safeprojectname$
{
    class Program
    {
        static void Main()
        {
            // 1. Implement the pipeline for creating and training the model    
            var mlContext = new MLContext();

            // 2. Specify how training data is going to be loaded into the DataView
            IDataView trainingDataView = mlContext.Data.ReadFromTextFile<SentimentData>(@"Data\wikipedia-detox-250-line-data.tsv", hasHeader: true);

            // 2. Create a pipeline to prepare your data, pick your features and apply a machine learning algorithm.
            // 2a. Featurize the text into a numeric vector that can be used by the machine learning algorithm.
            var pipeline = mlContext.Transforms.Text.FeaturizeText(outputColumnName: DefaultColumnNames.Features, inputColumnName: nameof(SentimentData.Text))
                    .Append(mlContext.BinaryClassification.Trainers.StochasticDualCoordinateAscent(labelColumn: DefaultColumnNames.Label, 
                                                                                                   featureColumn: DefaultColumnNames.Features));

            // 3. Get a model by training the pipeline that was built.
            Console.WriteLine("Creating and Training a model for Sentiment Analysis using ML.NET");
            ITransformer model = pipeline.Fit(trainingDataView);

            // 4. Evaluate the model to see how well it performs on different dataset (test data).
            Console.WriteLine("Training of model is complete \nEvaluating the model with test data");

            IDataView testDataView = mlContext.Data.ReadFromTextFile<SentimentData>(@"Data\wikipedia-detox-250-line-test.tsv", hasHeader: true);
            var predictions = model.Transform(testDataView);
            var results = mlContext.BinaryClassification.Evaluate(predictions);
            Console.WriteLine($"Accuracy: {results.Accuracy:P2}");

            // 5. Use the model for making a single prediction.
            var predictionEngine = model.CreatePredictionEngine<SentimentData, SentimentPrediction>(mlContext);
            var testInput = new SentimentData { Text = "ML.NET is fun, more samples at https://github.com/dotnet/machinelearning-samples" };
            SentimentPrediction resultprediction = predictionEngine.Predict(testInput);

            /* This template uses a minimal dataset to build a sentiment analysis model which leads to relatively low accuracy. 
             * Building good Machine Learning models require large volumes of data. This template comes with a minimal dataset (Data/wikipedia-detox) for sentiment analysis. 
             * In order to build a sentiment analysis model with higher accuracy please follow the walkthrough at https://aka.ms/mlnetsentimentanalysis/. */
            Console.WriteLine($"Predicted sentiment for \"{testInput.Text}\" is: { (Convert.ToBoolean(resultprediction.Prediction) ? "Positive" : "Negative")}");

            // 6. Save the model to file so it can be used in another app.
            Console.WriteLine("Saving the model");

            using (var fs = new FileStream("sentiment_model.zip", FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                model.SaveTo(mlContext, fs);
                fs.Close();
            }

            Console.ReadLine();
        }
    }

    /// <summary>
    /// Input class that tells ML.NET how to read the dataset.
    /// </summary>
    public class SentimentData
    {
        [LoadColumn(0)]
        public bool Label { get; set; }

        [LoadColumn(1)]
        public string Text { get; set; }
    }

    /// <summary>
    /// Output class for the prediction, in this case including only the predicted sentiment.
    /// </summary>
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
    }
}
