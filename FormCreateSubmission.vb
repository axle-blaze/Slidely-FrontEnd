Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Diagnostics

Public Class FormCreateSubmission
    Inherits Form

    Private ReadOnly baseUrl As String = "http://localhost:3000" ' Base URL of your backend server
    Private stopwatch As New Stopwatch()

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.KeyPreview = True
    End Sub

    Private Sub btnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click
        ToggleStopwatch()
    End Sub

    Private Sub ToggleStopwatch()
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            btnStartStop.Text = "START (CTRL + T)"
        Else
            stopwatch.Start()
            btnStartStop.Text = "STOP (CTRL + T)"
        End If
        UpdateStopwatchTime()
    End Sub

    Private Sub UpdateStopwatchTime()
        lblStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub FormCreateSubmission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add labels to text boxes
        AddLabelTextBox("Name:", txtName)
        AddLabelTextBox("Email:", txtEmail)
        AddLabelTextBox("Phone Num:", txtPhone)
        AddLabelTextBox("GitHub Link For Task 2:", txtGitHubLink)

        ' Start the stopwatch
        stopwatch.Start()

        ' Set form colors and fonts
        Me.BackColor = Color.LightGray ' Set form background color

        ' Style buttons
        btnStartStop.BackColor = Color.LightYellow
        btnStartStop.ForeColor = Color.Black
        btnStartStop.Font = New Font("Arial", 10)

        btnSubmit.BackColor = Color.LightBlue
        btnSubmit.ForeColor = Color.Black
        btnSubmit.Font = New Font("Arial", 15, FontStyle.Bold)

        ' Style stopwatch label
        lblStopwatchTime.BackColor = Color.LightGray
        lblStopwatchTime.ForeColor = Color.Black
        lblStopwatchTime.Font = New Font("Arial", 10)

        ' Style text boxes with different colors
        Dim textBoxFont As New Font("Arial", 10)
        txtName.Font = textBoxFont
        txtName.BackColor = Color.LightPink
        txtName.ForeColor = Color.Black

        txtEmail.Font = textBoxFont
        txtEmail.BackColor = Color.LightGreen
        txtEmail.ForeColor = Color.Black

        txtPhone.Font = textBoxFont
        txtPhone.BackColor = Color.LightBlue
        txtPhone.ForeColor = Color.Black

        txtGitHubLink.Font = textBoxFont
        txtGitHubLink.BackColor = Color.LightYellow
        txtGitHubLink.ForeColor = Color.Black
    End Sub

    Private Sub AddLabelTextBox(labelText As String, targetTextBox As TextBox)
        Dim label As New Label()
        label.Text = labelText
        label.AutoSize = False
        label.Width = 130
        label.Height = 30
        label.TextAlign = ContentAlignment.MiddleRight
        label.Location = New Point(targetTextBox.Left - 150, targetTextBox.Top)
        label.Font = New Font("Arial", 10, FontStyle.Regular)
        label.ForeColor = Color.Black

        Me.Controls.Add(label)
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SubmitData()
    End Sub

    Private Async Sub SubmitData()
        Dim submission As New Submission() With {
            .Name = txtName.Text,
            .Email = txtEmail.Text,
            .Phone = txtPhone.Text,
            .GithubLink = txtGitHubLink.Text,
            .StopwatchTime = lblStopwatchTime.Text
        }

        Try
            Using client As New HttpClient()
                ' Serialize submission object to JSON
                Dim jsonContent As String = SerializeSubmission(submission)
                Console.WriteLine(jsonContent)
                ' Send a POST request to submit data to the backend
                Dim response = Await client.PostAsync($"{baseUrl}/submit", New StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"))
                response.EnsureSuccessStatusCode() ' Ensure the request was successful

                MessageBox.Show("Submission successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SerializeSubmission(submission As Submission) As String
        ' Serialize the submission object to JSON
        Dim json As String = JsonConvert.SerializeObject(submission)

        ' Return the JSON string
        Return json
    End Function

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.T) Then
            ToggleStopwatch()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.S) Then
            SubmitData()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class

Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GithubLink As String ' Corrected casing
    Public Property StopwatchTime As String
End Class
