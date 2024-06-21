Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set form properties
        Me.Text = "Slidely Application"
        Me.MaximizeBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(255, 230, 240) ' Light pink background for the form

        ' Set button properties
        btnViewSubmissions.Text = "View Submissions"
        btnCreateSubmission.Text = "Create Submission"

        ' Add button tooltips
        Dim toolTip As New ToolTip()
        toolTip.SetToolTip(btnViewSubmissions, "View existing submissions")
        toolTip.SetToolTip(btnCreateSubmission, "Create a new submission")

        ' Set button styles
        btnViewSubmissions.FlatStyle = FlatStyle.Flat
        btnCreateSubmission.FlatStyle = FlatStyle.Flat
        btnViewSubmissions.BackColor = Color.FromArgb(255, 128, 128) ' Vibrant red background
        btnCreateSubmission.BackColor = Color.FromArgb(128, 255, 128) ' Vibrant green background
        btnViewSubmissions.ForeColor = Color.Black ' Black text
        btnCreateSubmission.ForeColor = Color.Black ' Black text

        ' Set button font
        Dim buttonFont As New Font("Segoe UI", 10, FontStyle.Bold)
        btnViewSubmissions.Font = buttonFont
        btnCreateSubmission.Font = buttonFont
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewSubmissionsForm As New FormViewSubmissions()
        viewSubmissionsForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        Dim createSubmissionForm As New FormCreateSubmission()
        createSubmissionForm.Show()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmission.PerformClick()
        End If
    End Sub
End Class
