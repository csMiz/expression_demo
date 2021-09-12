<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PBox = New System.Windows.Forms.PictureBox()
        Me.TBox = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        CType(Me.PBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PBox
        '
        Me.PBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBox.Location = New System.Drawing.Point(21, 21)
        Me.PBox.Name = "PBox"
        Me.PBox.Size = New System.Drawing.Size(400, 400)
        Me.PBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBox.TabIndex = 0
        Me.PBox.TabStop = False
        '
        'TBox
        '
        Me.TBox.Location = New System.Drawing.Point(441, 145)
        Me.TBox.Multiline = True
        Me.TBox.Name = "TBox"
        Me.TBox.Size = New System.Drawing.Size(329, 276)
        Me.TBox.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(441, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 50)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Load"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(441, 77)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 50)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "<-"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(549, 77)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(102, 50)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "->"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(657, 77)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(102, 50)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Jump"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(549, 21)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(102, 50)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "Save"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(657, 21)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 27)
        Me.Button6.TabIndex = 7
        Me.Button6.Text = "Resize128"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(657, 44)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 27)
        Me.Button7.TabIndex = 8
        Me.Button7.Text = "Rename"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TBox)
        Me.Controls.Add(Me.PBox)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PBox As PictureBox
    Friend WithEvents TBox As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
End Class
