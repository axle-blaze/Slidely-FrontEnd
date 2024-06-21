Imports System.Net.Http
Imports Newtonsoft.Json

Public Class FormViewSubmissions
    Private ReadOnly baseUrl As String = "http://localhost:3000" ' Base URL of your backend server
    Private submissionIndex As Integer = 0 ' Variable to track the current submission index

    Private Async Sub FormViewSubmissions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadSubmissions()
    End Sub

    Private Async Function LoadSubmissions() As Task
        Try
            Using client As New HttpClient()
                ' Send a GET request to fetch data from the backend
                Dim response = Await client.GetAsync($"{baseUrl}/ping")
                response.EnsureSuccessStatusCode() ' Ensure the request was successful

                ' Send a GET request to fetch submission data from the backend
                response = Await client.GetAsync($"{baseUrl}/read?index={submissionIndex}")
                response.EnsureSuccessStatusCode() ' Ensure the request was successful

                ' Read the response content as a string
                Dim responseData As String = Await response.Content.ReadAsStringAsync()

                ' Deserialize the JSON response into submission data
                Dim submission As Submission = DeserializeSubmission(responseData)

                ' Display the submission data in the text boxes
                DisplaySubmission(submission)
            End Using
        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ' Deserialize JSON string into Submission object
    Private Function DeserializeSubmission(jsonString As String) As Submission
        ' Deserialize the JSON string into a Submission object
        Dim submission As Submission = JsonConvert.DeserializeObject(Of Submission)(jsonString)

        ' Return the deserialized Submission object
        Return submission
    End Function

    Private Sub DisplaySubmission(submission As Submission)
        ' Display the submission data in the text boxes
        lblName.Text = "Name:"
        txtName.Text = submission.Name

        lblPhone.Text = "Email:"
        txtEmail.Text = submission.Email

        lblEmail.Text = "Phone:"
        txtPhone.Text = submission.Phone

        lblGithubLink.Text = "GitHub Link:"
        txtGithubLink.Text = submission.GithubLink

        lblStopwatchTime.Text = "Stopwatch Time:"
        txtStopwatchTime.Text = submission.StopwatchTime

        ' Apply vibrant colors
        Me.BackColor = Color.LightBlue ' Set form background color
        Me.ForeColor = Color.DarkBlue ' Set form text color

        ' Apply font styles
        Dim headerFont As New Font("Arial", 12, FontStyle.Bold)
        lblName.Font = headerFont
        lblPhone.Font = headerFont
        lblEmail.Font = headerFont
        lblGithubLink.Font = headerFont
        lblStopwatchTime.Font = headerFont

        Dim headerFont1 As New Font("Arial", 15, FontStyle.Bold)

        btnNext.Font = headerFont1
        btnPrevious.Font = headerFont1

        Dim textFont As New Font("Arial", 10, FontStyle.Regular)
        txtName.Font = textFont
        txtEmail.Font = textFont
        txtPhone.Font = textFont
        txtGithubLink.Font = textFont
        txtStopwatchTime.Font = textFont
    End Sub

    ' Button for moving to the next submission
    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        submissionIndex += 1 ' Increment the submission index
        Await LoadSubmissions() ' Load the next submission
    End Sub

    ' Button for moving to the previous submission
    Private Async Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If submissionIndex > 0 Then ' Ensure the index is not negative
            submissionIndex -= 1 ' Decrement the submission index
            Await LoadSubmissions() ' Load the previous submission
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles txtStopwatchTime.TextChanged

    End Sub

    ' Process keyboard shortcuts
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.N) Then ' Ctrl + N for next
            btnNext.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.P) Then ' Ctrl + P for previous
            btnPrevious.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
