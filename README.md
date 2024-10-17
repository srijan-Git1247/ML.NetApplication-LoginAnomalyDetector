The project is to demonstrate Randomized PCA trainer to detect anomalies such as unusual login times.
The Randomized PCA trainer requires normalization of the values, Caching is not necessary and no additional NuGet Package is required to utilize the trainer.

The input is a known vector size of the Float type. The output comprises two properties: Score and PredictedLabel
Score values is of the Float Type, non-negative and unbounded. 
PredictedLable property indicates a valid anomaly based on the threshold set; a true values indicates an anomaly, while a value of false indicates otherwise.
Default value given by ML.Net is 0.5. Values higher than threshold returns true, and false if they are lower.

The sampledata.csv file in the Common Folder contains 100 rows of login data. 
Feel free to adjust the data to fit your own observation or to adjust the trained model. Here is a snippet of the data:


![image](https://github.com/user-attachments/assets/0c87527e-18ab-4907-9323-dae654c6adb4)


Each of these rows contains the value for the properties in the LoginHistoryClass. 
These correspond to UserID, CorporateNetwork, HomeNetwork, WithinWorkHours, WorkDay, Label


In addition to this, testdata.csv file in the Common Folder contains additional data points to test the newly trained model against and evaluate. 
Here is a snippet of the data:

![image](https://github.com/user-attachments/assets/623a60d5-21de-4d45-8994-268ccc86afba)


Run the Console Application with commandline arguments:

1. Train and test-evaluate the model using sampledata.csv and testdata.csv

>D:\Machine Learning Projects\LoginAnomalyDetector\bin\Debug\net8.0 train "D:\Machine Learning Projects\LoginAnomalyDetector\Data\sampledata.csv"
> "D:\Machine Learning Projects\LoginAnomalyDetector\Data\testdata.csv"

![Screenshot 2024-08-21 142604](https://github.com/user-attachments/assets/f2898751-c59c-4cb1-b577-66668fcfa332)


2. After training the model, build a sample JSON file and save it as input.json as follows:

 ![Screenshot 2024-08-21 143016](https://github.com/user-attachments/assets/3537acf6-3481-4feb-ab66-5937c42166da)

3. To run the model with the input.json, simply pass in the filename to built application and the predicted output will appear:

> D:\Machine Learning Projects\LoginAnomalyDetector\bin\Debug\net8.0\LoginAnomalyDetector.exe predict "D:\Machine Learning Projects\LoginAnomalyDetector\Data\input.json"

![Screenshot 2024-08-21 143309](https://github.com/user-attachments/assets/1d5480f0-b921-4b45-8a0e-9f0ff636d136)













