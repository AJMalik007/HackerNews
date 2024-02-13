# 🚀 HackerNews

HackerNews is a task given to me for a hiring purpose. It's designed to demonstrate my coding skills and problem-solving abilities. The project is built with a focus on simplicity, efficiency, and user-friendliness.

## 🎯 How to Run the Application

Follow these detailed steps to run the application:

1. **Open the Project**: Launch Visual Studio and open the Project E solution file.
2. **Build the Project**: Navigate to the menu bar, click on `Build` and then select `Build Solution`. Wait for the build to complete successfully.
3. **Run the Application**: To start the application, go to the menu bar, click on `Debug`, and then select `Start Debugging`.

### Fetching Top N Best Stories

1. **Access Swagger UI**: Launch your ASP.NET Core Web API application, and navigate to [Swagger UI](https://localhost:7093/swagger/index.html).

2. **Explore API Documentation**: Swagger UI provides comprehensive documentation of your API endpoints. Locate the endpoint for fetching the top N best stories.

3. **Specify Parameters**: Within the Swagger UI interface, specify the parameters for fetching the top stories. Enter values for `top`, `pageSize`, and `pageNumber` as required.

4. **Submit Request**: Click the "Try it out" button to submit the request to the API.

5. **Review Response**: Swagger UI will display the response returned by the API. You'll receive a JSON array containing the top N best stories, each with properties like `title`, `uri`, `postedBy`, `time`, `score`, and `commentCount`.

    ```json
    [
        {
            "title": "I designed a cube that balances itself on a corner",
            "uri": "https://willempennings.nl/balancing-cube/",
            "postedBy": "dutchkiwifruit",
            "time": "2024-02-11T16:36:58+00:00",
            "score": 2390,
            "commentCount": 94
        },
        {
            "title": "Almost every infrastructure decision I endorse or regret",
            "uri": "https://cep.dev/posts/every-infrastructure-decision-i-endorse-or-regret-after-4-years-running-infrastructure-at-a-startup/",
            "postedBy": "slyall",
            "time": "2024-02-09T11:05:39+00:00",
            "score": 1148,
            "commentCount": 79
        }
    ]
    ```

6. **Process Response**: Utilize the received JSON response as needed in your application. You may display the stories, extract specific information, or perform further operations.

## 📚 Assumptions

For this project, no assumptions have been made as the project requirements were clear and concise.

## 🚧 Enhancements and Changes

Given more time, I would love to make the following enhancements and changes to improve the project:

- **Unit Tests**: Implementing unit tests to ensure the functionality of the application is working as expected. This would help in catching any bugs or issues early in the development process.
- **Global Exception Handling**: Setting up a global exception handling mechanism to catch and handle any unexpected errors or exceptions that may occur during the execution of the application.
- **Validations**: Incorporating validations to ensure that the input data is correct and in the expected format. This would help prevent any errors or issues caused by incorrect or unexpected input data.

## 📈 Future Work

In the future, I plan to add the following features and improvements:

- **Unit Tests**: To ensure the robustness of the application.
- **Global Exception Handling**: To provide a seamless user experience.
- **Validations**: To ensure data integrity and prevent potential issues.

## 🤝 Contributing

I welcome contributions! For major changes, please open an issue first to discuss what you would like to change.

## 📜 License

This project is currently not under any license.
