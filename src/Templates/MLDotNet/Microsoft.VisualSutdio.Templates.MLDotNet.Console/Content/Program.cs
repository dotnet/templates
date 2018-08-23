/* This template shows the building blocks for training a machine learning model with ML.NET (https://aka.ms/mlnet).
 * This model predicts whether a sentence has a positive or negative sentiment. It is based on a sample that can be 
 * found at https://aka.ms/mlnetsentimentanalysis, which provides a more detailed introduction to ML.NET and the scenario.*/

using System;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace $safeprojectname$
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Build an ML.NET pipeline for training a sentiment analysis model
            Console.WriteLine("Training a model for Sentiment Analysis using ML.NET");
            var pipeline = new LearningPipeline();

            // 1a. Load the training data using a TextLoader.
            pipeline.Add(new TextLoader(@"..\..\..\Data\wikipedia-detox-250-line-data.tsv").CreateFrom<SentimentData>(useHeader: true));
            
            // 1b. Featurize the text into a numeric vector that can be used by the machine learning algorithm.
            pipeline.Add(new TextFeaturizer("Features", "SentimentText"));
            
            // 1c. Add AveragedPerceptron (a linear learner) to the pipeline.
            pipeline.Add(new AveragedPerceptronBinaryClassifier() { NumIterations = 10 });
            
            // 1d. Get a model by training the pipeline that was built.
            PredictionModel<SentimentData, SentimentPrediction> model =
                pipeline.Train<SentimentData, SentimentPrediction>();

            // 2. Evaluate the model to see how well it performs on different data (output the percent of examples classified correctly).
            Console.WriteLine("Training of model is complete \nTesting the model with test data");
            var testData = new TextLoader(@"..\..\..\Data\wikipedia-detox-250-line-test.tsv").CreateFrom<SentimentData>(useHeader: true);
            var evaluator = new BinaryClassificationEvaluator();
            BinaryClassificationMetrics metrics = evaluator.Evaluate(model, testData);
            Console.WriteLine($"Accuracy of trained model for test data is: {metrics.Accuracy:P2}");

            // 3. Save the model to file so it can be used in another app.
            model.WriteAsync("sentiment_model.zip");

            // 4. Use the model for a single prediction.
            SentimentData testInput = new SentimentData { SentimentText = "ML.NET is fun, more samples at https://github.com/dotnet/machinelearning-samples" };
            var sentiment = (model.Predict(testInput).Sentiment == true) ? "Positive" : "Negative";
            Console.WriteLine("Predicted sentiment for \"" + testInput.SentimentText + "\" is:" +  sentiment);
            Console.ReadKey();

        }

        // Input class that tells ML.NET how to read the dataset (which columns are included).
        public class SentimentData
        {
            [Column(ordinal: "0", name: "Label")]
            public float Sentiment;
            [Column(ordinal: "1")]
            public string SentimentText;
        }

        // Output class for the prediction, in this case including only the predicted sentiment.
        public class SentimentPrediction
        {
            [ColumnName("PredictedLabel")]
            public bool Sentiment;
        }
    }
}
