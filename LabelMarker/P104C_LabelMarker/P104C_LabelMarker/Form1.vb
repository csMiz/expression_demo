Imports System.IO
Imports System.Numerics
Imports OpenCvSharp
Imports OpenCvSharp.Cv2


Public Class Form1

    Public FolderPath As String
    Public FileFullList As New List(Of String)
    Public FileShortList As New List(Of String)
    Public CurrentFileIdx As Integer = 0
    Public LabelPath As String = "C:\Users\asdfg\Desktop\P104C\Datasets\lbl_out.txt"
    Public LabelList As New Dictionary(Of String, Single())

    Public Sub LoadSamplesAndLabels()
        Dim files As String() = IO.Directory.GetFiles(FolderPath)
        FileFullList.Clear()
        FileShortList.Clear()
        CurrentFileIdx = 0
        For Each fullpath As String In files
            FileFullList.Add(fullpath)
            Dim shortname As String = fullpath.Substring(FolderPath.Length + 1)
            FileShortList.Add(shortname)
        Next

        LabelList.Clear()
        Dim labelFile As New FileStream(LabelPath, FileMode.OpenOrCreate)
        Using sr As New StreamReader(labelFile)
            While Not sr.EndOfStream
                Dim line As String = sr.ReadLine.Trim.Replace(" ", "")
                If line.StartsWith("#") Then
                    Continue While
                End If

                Dim segs As String() = line.Split(",")
                If segs.Count > 0 Then
                    Dim key As String = segs(0)
                    Dim value As New List(Of Single)
                    If segs.Count > 1 Then
                        For i = 1 To segs.Count - 1
                            If segs(i).Length > 0 Then
                                value.Add(CSng(segs(i)))
                            End If
                        Next
                    End If
                    LabelList(key) = value.ToArray
                End If
            End While
        End Using
        labelFile.Close()
        labelFile.Dispose()

    End Sub

    Public Sub DispalyImage()
        Dim oldImg As Image = PBox.Image
        Dim img As New Bitmap(FileFullList(CurrentFileIdx))
        PBox.Image = img
        If oldImg IsNot Nothing Then
            oldImg.Dispose()
        End If
    End Sub

    Public Sub UpdateUI()
        Me.Text = CurrentFileIdx & " - " & FileShortList(CurrentFileIdx)
        Button2.Enabled = Not (CurrentFileIdx = 0)
        Button3.Enabled = Not (CurrentFileIdx = FileFullList.Count - 1)

        TBox.Text = ""
        If LabelList.ContainsKey(FileShortList(CurrentFileIdx)) Then
            Dim raw As Single() = LabelList(FileShortList(CurrentFileIdx))
            If raw.Count > 0 Then
                For i = 0 To raw.Count - 1
                    TBox.Text = TBox.Text & raw(i).ToString
                    If i Mod 2 = 1 Then
                        TBox.Text = TBox.Text & vbCrLf
                    Else
                        TBox.Text = TBox.Text & ","
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub ApplyCurrentLabel()
        Dim text As String = TBox.Text.Replace(vbCrLf, ",").Replace(" ", "")
        Dim segs As String() = text.Split(",")
        Dim list As New List(Of Single)
        If segs.Count > 0 Then
            For i = 0 To segs.Count - 1
                If segs(i).Length > 0 Then
                    list.Add(CSng(segs(i)))
                End If
            Next
        End If
        LabelList(FileShortList(CurrentFileIdx)) = list.ToArray
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fbd As New FolderBrowserDialog
        With fbd
            .ShowNewFolderButton = False
        End With
        If fbd.ShowDialog = DialogResult.OK Then
            FolderPath = fbd.SelectedPath
            LoadSamplesAndLabels()
            DispalyImage()
            UpdateUI()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ApplyCurrentLabel()
        CurrentFileIdx -= 1
        DispalyImage()
        UpdateUI()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ApplyCurrentLabel()
        CurrentFileIdx += 1
        DispalyImage()
        UpdateUI()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ApplyCurrentLabel()
        CurrentFileIdx = CInt(InputBox("idx: 0 - " & (FileFullList.Count - 1).ToString))
        DispalyImage()
        UpdateUI()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ApplyCurrentLabel()
        Dim labelFile As New FileStream(LabelPath, FileMode.Create)
        Using sw As New StreamWriter(labelFile)
            For Each lbl As KeyValuePair(Of String, Single()) In LabelList
                Dim line As String = lbl.Key
                If lbl.Value.Count > 0 Then
                    line = line & ","
                    For i = 0 To lbl.Value.Count - 1
                        line = line & lbl.Value(i).ToString
                        If i <> lbl.Value.Count - 1 Then
                            line = line & ","
                        End If
                    Next
                End If
                sw.WriteLine(line)
            Next
        End Using
        labelFile.Close()
        labelFile.Dispose()
    End Sub

    Private Sub PBox_MouseDown(sender As Object, e As MouseEventArgs) Handles PBox.MouseDown
        Dim pos As New Vector2(e.X / PBox.Width, e.Y / PBox.Height)
        TBox.Text = TBox.Text & pos.X.ToString & "," & pos.Y.ToString & vbCrLf
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim resize_src_folder As String = "C:\Users\asdfg\Desktop\P104C\Datasets\tmp\"
        Dim files As String() = IO.Directory.GetFiles(resize_src_folder)

        For Each fullpath As String In files
            Dim shortname As String = fullpath.Substring(resize_src_folder.Length)
            Dim targetPath As String = "C:\Users\asdfg\Desktop\P104C\Datasets\tmp2\" & shortname

            Dim image = ImRead(fullpath, ImreadModes.Color)
            image = image.Resize(New Size(128, 128))
            ImWrite(targetPath, image)

        Next

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim rename_src_folder As String = "C:\Users\asdfg\Desktop\P104C\Datasets\video\"
        Dim files As String() = IO.Directory.GetFiles(rename_src_folder)

        For Each fullpath As String In files
            Dim shortname As String = fullpath.Substring(rename_src_folder.Length)
            shortname = shortname.Substring(0, shortname.Length - 4)
            Dim val As Integer = CInt(shortname)
            Dim newname As String = val.ToString("000")

            Dim targetPath As String = "C:\Users\asdfg\Desktop\P104C\Datasets\tmp2\" & newname & ".png"
            IO.File.Copy(fullpath, targetPath)


        Next
    End Sub


    Public Sub test()


    End Sub

End Class
