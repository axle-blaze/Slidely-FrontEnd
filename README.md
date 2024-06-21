# Slidely - Submission Tracker

Slidely is a Windows Forms application for tracking submissions. It allows users to create, view, and manage submissions with ease. This project demonstrates the use of VB.NET for creating a user-friendly UI and interacting with a backend server.

## Features

- Start and pause a stopwatch to track time spent on a task.
- Create new submissions with details such as name, email, phone, GitHub link, and stopwatch time.
- View existing submissions and navigate through them using "Next" and "Previous" buttons.
- Data serialization and deserialization using JSON.
- RESTful API interaction with a backend server.

## Getting Started

### Prerequisites

- .NET Framework 4.7.2 or later
- Visual Studio 2019 or later
- Newtonsoft.Json library (included via NuGet)

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/slidely.git
    ```
2. Open the solution file `Slidely.sln` in Visual Studio.
3. Restore NuGet packages.
4. Build the solution.

### Usage

1. Start the backend server (make sure it is running on `http://localhost:3000`).
2. Run the application from Visual Studio.

#### Creating a Submission

1. Click on the "Create Submission" button.
2. Fill in the details (Name, Email, Phone, GitHub Link).
3. Start the stopwatch by clicking the "Start" button.
4. Pause the stopwatch by clicking the "Pause" button.
5. Click "Submit" to save the submission.

#### Viewing Submissions

1. Click on the "View Submissions" button.
2. Use the "Next" and "Previous" buttons to navigate through the submissions.

## Code Overview

### FormCreateSubmission

Handles the creation of new submissions:
- Manages the stopwatch for tracking time.
- Submits the form data to the backend server.

### FormViewSubmissions

Handles the viewing of existing submissions:
- Fetches submission data from the backend server.
- Displays the data in a user-friendly format.
- Allows navigation through submissions.

### Backend API

Ensure the backend server provides the following endpoints:
- `GET /ping`: To check if the server is running.
- `GET /read?index={index}`: To fetch a submission by index.
- `POST /submit`: To create a new submission.


