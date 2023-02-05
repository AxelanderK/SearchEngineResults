
# Search Engine Result

Makes searches on several search engines using SerpApi and presents the total count of results.

## Authors

- [@axelanderk](https://www.github.com/axelanderk)


## Requirements

* .Net 6
* node.js
* npm

## Run Locally

Clone the project

```bash
  git clone https://github.com/AxelanderK/SearchEngineResults.git
```

Go to the project directory

```bash
  cd SearchEngineResults
```

Install dependencies

```bash
  npm install
```

Add SerpAPIKey to dotnet user secrets. Change [APIKEY] to your accual API key. Your can find your API key on [SerpApi.com](https://SerpApi.com).

```
dotnet user-secrets set "SerpApiKey" "[APIKEY]"
```

Run dev-certs command in terminal to trust dev certs.
```
dotnet dev-certs https --trust
```

Start the application

```bash
  dotnet run
```

Open web browser and access application with URL
```
https://localhost:7107
```