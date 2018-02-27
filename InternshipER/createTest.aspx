﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="createTest.aspx.cs" Inherits="InternshipER.createTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/createTest.css">
    <asp:Panel ID="pnlTextBoxes" runat="server">
    <div class="questionMaker">
        <asp:RadioButtonList ID="questionType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButton_SelectedIndexChanged" CellPadding="5" AutoPostBack="true" CausesValidation="False" OnDataBinding="RadioButton_SelectedIndexChanged" usesubmitbehavior="False">
            <asp:ListItem Text="Çoktan Seçmeli" />
            <asp:ListItem Text="Kod" />
            <asp:ListItem Text="Açık Uçlu" />
            <asp:ListItem Text="Ses Kaydı" />
        </asp:RadioButtonList>
        <asp:Button ID="submitQuestion" runat="server" OnClick="Submit_onClick" Text="Ekle" CausesValidation="False" OnDataBinding="addChoice_Click" UseSubmitBehavior="False" />
        <asp:Label runat="server" Text="Soru Giriniz:"></asp:Label><br />
        <asp:TextBox runat="server" ID="question" TextMode="MultiLine" Row="5" Height="100px" Width="200px"></asp:TextBox><br /><br />
        <!--<asp:Button ID="addChoice" runat="server" OnClick="addChoice_Click"  Text="Seçenek Ekle" CausesValidation="False" OnDataBinding="addChoice_Click" UseSubmitBehavior="False" />-->

            <span id="choiceAL" runat="server">A : </span>
            <input id="choiceA" runat="server" type="text" />
            <br /><br />

            <span id="choiceBL" runat="server">B : </span>
            <input  id="choiceB" runat="server" type="text" />
            <br /><br />
            <span id="choiceCL" runat="server">C : </span>
            <input  id="choiceC" runat="server" type="text" />
            <br /><br />
            <span id="choiceDL" runat="server">D : </span>
            <input id="choiceD" runat="server" type="text" />
            <br /><br />

        <asp:PlaceHolder ID="multipleChoices" runat="server" />
        

    </div>
     </asp:Panel>
    <div class="addedQuestions">
        <asp:PlaceHolder ID="addedQuestions" runat="server" />
    </div>
</asp:Content>

