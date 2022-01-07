Imports Microsoft.Win32
Public Class Form1
    Dim rValue, rsvalue As Int32
    Dim Regkey, RegKey2 As RegistryKey
    Dim Gvalue, tvalue As Int32
    Dim Regpath As String = "System\CurrentControlSet\Services\USBSTOR"
    Dim ReadAndWriteRegPath2 As String = "System\CurrentControlSet\Control"
    Dim ReadAndWriteRegPath As String = "System\CurrentControlSet\Control\StorageDevicePolicies"
    Public Pont As Point

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'check if the current log-in user is a member of the administrators
        If My.User.IsInRole("Administrators") = False Then
            MsgBox("You don't have proper privileges level to make changes, administrators privileges are required", MsgBoxStyle.Critical, "Error")
            End
        Else
            Regkey = Registry.LocalMachine.OpenSubKey(Regpath, True)
            Gvalue = CInt(Regkey.GetValue("Start"))
            'check the current state of the usb/whether is enabled or disabled
            If Gvalue = 3 Then
                RadioButtonenable.Checked = True
            ElseIf Gvalue = 4 Then
                RadioButtondisable.Checked = True
            End If
            RegKey2 = Registry.LocalMachine.OpenSubKey(ReadAndWriteRegPath, True)
            Try
                tvalue = CInt(RegKey2.GetValue("WriteProtect"))
                If tvalue = 1 Then
                    RadioButtonreadonly.Checked = True
                ElseIf tvalue = 0 Then
                    RadioButtonreadwrite.Checked = True
                End If
            Catch ex As NullReferenceException
            End Try

        End If
    End Sub

    Private Sub RadioButtonenable_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonenable.CheckedChanged
        GroupBox2.Enabled = True
        rValue = 3
    End Sub

    Private Sub RadioButtondisable_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtondisable.CheckedChanged
        GroupBox2.Enabled = False
        rValue = 4
    End Sub

    Private Sub RadioButtonreadonly_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonreadonly.CheckedChanged
        rsvalue = 1
    End Sub

    Private Sub RadioButtonreadwrite_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonreadwrite.CheckedChanged
        rsvalue = 0
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Regkey = Registry.LocalMachine.OpenSubKey(Regpath, True)
        Regkey.SetValue("Start", rValue)
        If GroupBox2.Enabled = True Then
            RegKey2 = Registry.LocalMachine.OpenSubKey(ReadAndWriteRegPath2, True)
            RegKey2.CreateSubKey("StorageDevicePolicies")
            RegKey2 = Registry.LocalMachine.OpenSubKey(ReadAndWriteRegPath, True)
            RegKey2.SetValue("WriteProtect", rsvalue)
        End If
        If rValue = 3 And rsvalue = 1 Then
            MsgBox("USB Port were enable and Read only is enabled")
        ElseIf rValue = 3 And rsvalue = 0 Then
            MsgBox("USB Port were enable and Read and write is enabled")
        Else
            MsgBox("USB Port were disable")
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        End
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        AboutBox1.Show()
    End Sub

    Private Sub ArabicToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArabicToolStripMenuItem.Click
        Me.Hide()
        Arabic.Show()
    End Sub
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        ContextMenuStrip1.Show(PictureBox3, 1, PictureBox3.Height)
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Pont = e.Location
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - Pont
        End If
    End Sub
End Class
