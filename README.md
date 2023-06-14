# Task description

## Backend task:
- Every minute, fetch data from https://tradestie.com/api/v1/apps/reddit and store success/failure attempt log in the table and full payload in the blob. 
- Create a GET API call to list all logs for the specific time period (from/to)
- Create a GET API call to fetch a payload from blob for the specific log entry

Technologies:
- Azure Function (Cloud/Local)
- Azure Storage (Cloud /Local storage emulator)
  Table
  Blob
- .Net

Publish code on GitHub

 

## Optional frontend task:
- Create SPA application as a client for backend task
- Application should allow a user:
    - to enter the period (from, to), fetch a list of all logs for this period and display it
    - to select one log entry on the list and fetch the blob
    - for fetched blob total "no_of_comments" and average of "sentiment_score" should be calculated and displayed

Technologies:
- any javascript framework (next.js preferable)

Publish code on GitHub
