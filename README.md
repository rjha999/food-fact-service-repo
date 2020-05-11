# food-fact-service-repo
Food Fact Microservice Repository

ReadMe
This MicroService is Consuming a third party API to get Products list with matching Ingredients
And also Unit test cases are added using xUnit.
This developed microservice can be Run Independently using Docker Container, Using below commands

1. docker build . -f foodfact.microservice/dockerfile -t foodfactservice

2. docker run -p  8081:80 foodfactservice


After running above commands, Browse with URL  http://192.168.99.100:8081/foodfact?ingredient=sugar&limit=20

Note: I have developed this application using Docker ToolBox, because I have Win 10 Home edition on my personal PC. This is why I cannot access URL with 'localhost'.

Below are some Search criteria:
1. Search parameters  (Ingredient, Limit)
2. In 3 seconds, only maximum 3 requests can be done
3. Food Fact API response is parsed and converted in required result format