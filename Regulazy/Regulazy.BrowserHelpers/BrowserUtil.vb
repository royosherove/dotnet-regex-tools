Imports System.Windows.Forms
Imports mshtml

Public Class BrowserUtil
    Private bro As WebBrowser
    Private doc As IHTMLDocument2

    Public ReadOnly Property Document() As IHTMLDocument2
        Get
            doc = bro.Document.DomDocument
            Return doc
        End Get
    End Property

    Public ReadOnly Property SelectedRange() As IHTMLTxtRange
        Get
            doc = bro.Document.DomDocument

            If doc.selection.type="" Then
                Return Nothing
            End If

            Return doc.selection.createRange()
        End Get
    End Property

    Public Sub New(ByVal br As WebBrowser)
        bro = br
    End Sub

End Class
