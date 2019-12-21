# SearchFight

Project Structure

- Model
  * RequestMetadata.cs
  * Query.cs
  * Request.cs
- Controller
  * QueryProcessor.cs
- RestClient.cs
- Program.cs


Contents

* Program 
  - It's the main class of the application.
* RestClient
  - This class defines our rest client.
* QueryProcessor
  - In this class I implemented the method to consume the Custom Search JSON API from Google. I created 3 custom engines: Google, Bing and Yahoo. In order to use these engines I had to generate an API Key and 3 Search Engine IDs.
* RequestMetadata, Query and Request
  - They define a custom structure to desearialize the JSON object from the response content according to documentation (https://developers.google.com/custom-search/v1/cse/list).

NOTE: Custom Search JSON API provides 100 search queries per day for free. So additional requests have a cost. If you reached out this limit Google blocks the API queries for the rest of the day.
