/* This template shows how to use a model trained with ML.NET (https://aka.ms/mlnet) to make predictions.
 * This model predicts whether a sentence has a positive or negative sentiment. It is based on a sample that can be 
 * found at https://aka.ms/mlnetsentimentanalysis, which provides a more detailed introduction to ML.NET and the scenario.
 * Note that this model was trained on a very small sample dataset, which leads to a relatively low accuracy.*/
using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Runtime.Data;

namespace $safeprojectname$
{
    public static class ModelPrediction
    {
        public static string Predict(SentimentData input)
        {
            // 1. Load the model from file.
            MLContext mlContext = new MLContext();
            var stream = new FileStream(@"Models\sentiment_model.zip", FileMode.Open, FileAccess.Read, FileShare.Read);
            var model = TransformerChain.LoadFrom(mlContext, stream);

            // 2. Predict the sentiment
            var predictionFunct = model.MakePredictionFunction<SentimentData, SentimentPrediction>(mlContext);
            var resultprediction = predictionFunct.Predict(input);
            var sentiment = (Convert.ToBoolean(resultprediction.Prediction) ? "Positive" : "Negative");
            Console.WriteLine($"Predicted sentiment for \"{input.Text}\" is:" + sentiment);
            return sentiment;
        }
    }

    /// <summary>
    /// Input class that tells ML.NET how to read the input for predictions.
    /// </summary>
    public class SentimentData
    {
        public bool Label { get; set; }
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
