
Option Strict Off
Option Explicit Off
Imports System.Drawing.Imaging
Imports Microsoft.Win32
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography

Module Module1
    Public constring3 As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\sdescounter.mdb ;"
    Public SqlConnection1 As SqlClient.SqlConnection = New SqlClient.SqlConnection
    Public SqlDataAdapter1 As SqlClient.SqlDataAdapter
    Public SqlDataAdapter2 As SqlClient.SqlDataAdapter
    Public ds As DataSet = New DataSet
    Public ds1 As DataSet = New DataSet
    Public ds2 As DataSet = New DataSet
    Public ds3 As DataSet = New DataSet
    Public cnn As New OleDbConnection
    Public cnn1 As New OleDbConnection
    Public cmd As New OleDbCommand
    Public adapt As New OleDbDataAdapter
    Public adapt1 As New OleDbDataAdapter
    Public adapt2 As New OleDbDataAdapter
    'Public ds9 As New cmpeprodbDataSet
    Public conn As New OleDbConnection
    Public WithEvents BS As New BindingSource
    Public con As New OleDbConnection
    Public da As OleDbDataReader
    Public DSCHECKUSERS As New DataSet
    Public ADPCHECKUSERS As OleDb.OleDbDataAdapter
    Public CONCHECKUSERS As New OleDb.OleDbConnection
    Public USERNAME As String
    Public PASSWORD As String
    Public id As String
    Public IDUSER1 As String
    Public BUTTONADD As Boolean
    Public BUTTONEDIT As Boolean
    Public BUTTONDELETE As Boolean
    Public BUTTONREVIEWER As Boolean
    Public mnu1 As Boolean
    Public mnu2 As Boolean
    Public mnu3 As Boolean
    Public mnu4 As Boolean
    Public mnu5 As Boolean
    Public mnu6 As Boolean
    Public mnu7 As Boolean
    Public mnu8 As Boolean
    Public mnu9 As Boolean
    Public mnu10 As Boolean
    Public mnu11 As Boolean

End Module
