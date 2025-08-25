# Veeam_Task

---

### How it works

You run the app with 4 command-line arguments:

--source:  the source folder path  
--replica: the replica folder path  
--interval: sync interval in seconds  
--log: path to the log file

The application performs the following steps every interval:

-> Scans the source folder and identifies new or updated files  
-> Copies these files to the replica folder  
-> Deletes files from the replica if they no longer exist in the source  
-> Logs every action with a timestamp

!!! To test locally in Visual Studio:  
Go to **Project -> Properties -> Debug -> Application arguments** and add:  
`--source "C:\Source" --replica "C:\Replica" --interval 30 --log "C:\Logs\sync.log"`

---

### Project Structure

**AppConfig.cs**: Stores the app's configuration  
**ArgumentParser.cs**: Parses command-line arguments  
**Logger.cs**: Handles logging to file and console  
**FileSynchronizer.cs**: Does the file sync logic  
**Program.cs**: Entry point and main loop

---

### My Thought Process

While building this project, I changed my approach a few times.

At first, I hardcoded the folder paths directly in the code (like `"C:\Test\src"`), thinking it would be faster. But I realized that this limits reusability. So I added command-line arguments to make it more flexible.

For file comparison, I thought about checking both file size and last modified time. Later, I decided that using only `LastWriteTimeUtc` is enough and keeps the logic simpler.

In the beginning, I placed all the logic inside **Program.cs**. It worked but was messy. When argument values were missing or invalid, the program crashed. I handled those exceptions by adding checks and printing error messages. Then I refactored the code step by step:

-> Created **ArgumentParser.cs** to handle input parsing and validation  
-> Added **Logger.cs** to centralize logging  
-> Used **AppConfig.cs** to hold the parsed config values

This helped me apply the **Single Responsibility Principle (SRP)** and made the code cleaner and easier to update later.

It wasn’t perfect on the first try. I got some unexpected results and runtime errors while testing. But after debugging and reorganizing, the app became much more stable and structured.
