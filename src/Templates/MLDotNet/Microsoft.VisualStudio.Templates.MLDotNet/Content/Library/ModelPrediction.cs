/* This template shows how to use a model trained with ML.NET (https://aka.ms/mlnet) to make predictions.
 * This model predicts whether a sentence has a positive or negative sentiment. It is based on a sample that can be 
 * found at https://aka.ms/mlnetsentimentanalysis, which provides a more detailed introduction to ML.NET and the scenario.
 * Note that this model was trained on a very small sample dataset, which leads to a relatively low accuracy. */
using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace $safeprojectname$
{
    public static class ModelPrediction
    {
       public static string Predict(SentimentData input)
        {
            // 1. Load the model from file.
            var mlContext = new MLContext();
            var model = mlContext.Model.Load(@"Models\sentiment_model.zip", out var modelInputSchema);

            // 2. Predict the sentiment.
            var predictionEngine = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
            var resultprediction = predictionEngine.Predict(input);
            var sentiment = Convert.ToBoolean(resultprediction.Prediction) ? "Positive" : "Negative";
            
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
