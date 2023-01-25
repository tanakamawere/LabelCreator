﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using LabelCreator.Services;
using CommunityToolkit.Maui.Alerts;

namespace LabelCreator
{
    [INotifyPropertyChanged]
    public partial class MainPageViewModel
    {
        public ObservableRangeCollection<string> Subjects { get; set; } = new();
        public ObservableRangeCollection<string> LineStyles { get; set; } = new();
        public ObservableRangeCollection<string> Fonts { get; set; } = new()
        {
            "Times New Roman",
            "Century Gothic",
            "Calibri"
        };

        [ObservableProperty]
        private string subject;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string classTitle;

        //Label Formating
        [ObservableProperty]
        float boxHeight = 200;
        [ObservableProperty]
        private string fontSelected = "Times New Roman";

        int numberOfLabels = 0;
        float verticalPositionOfNextBox = 0;

        [ObservableProperty]
        int fontSize = 24;
        [ObservableProperty]
        bool isBold = true;
        [ObservableProperty]
        float rectangleWidth = 1;
        [ObservableProperty]
        string selectedLineStyle = "Single";
        [ObservableProperty]
        bool hasYear = false;
        int numberOfLabelsPerPage = 3;

        public MainPageViewModel()
        {
            LineStyles.AddRange(Enum.GetNames(typeof(LineStyle)));
        }

        [RelayCommand]
        void SaveDocumentClicked() =>
            SaveDocument(CreateDocument());

        [RelayCommand]
        void AddSubject()
        {
            Subjects.Add(Subject);
            Subject = string.Empty;
        }
        [RelayCommand]
        void Clear()
        {
            Subjects.Clear();
            Name = string.Empty;
            ClassTitle = string.Empty;
            verticalPositionOfNextBox = 0;
            numberOfLabels = 0;
        }

        [RelayCommand]
        void DeleteSubject(string subjectSelected) =>
            Subjects.Remove(subjectSelected);
        private MemoryStream CreateDocument()
        {
            //Creates a new document.
            using WordDocument document = new();

            WSection section = SectionInstance(document);

            int labelDetails = 3;
            if (HasYear == true)
                labelDetails = 4;

            boxHeight = ((float)fontSize * labelDetails) + 75;

            if (fontSize > 30)
                numberOfLabelsPerPage = 2;

            foreach (string item in Subjects)
            {
                if (numberOfLabels.Equals(numberOfLabelsPerPage))
                {
                    section = SectionInstance(document);
                    section.BreakCode = SectionBreakCode.NewPage;
                    numberOfLabels = 0;
                    verticalPositionOfNextBox = 0;
                }

                PrintToPage(section, item);
                verticalPositionOfNextBox += (boxHeight + 10);
                numberOfLabels++;
            }

            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            return ms;
        }
        static WSection SectionInstance(WordDocument document)
        {
            WSection section = document.AddSection() as WSection;
            //Sets Margin of the section.
            section.PageSetup.Margins.All = 20;
            //Sets the page size of the section.
            section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);
            return section;
        }

        void PrintToPage(WSection section, string item)
        {
            //NAME
            //Adds new paragraph to the section
            WParagraph paragraph = section.AddParagraph() as WParagraph;
            //Adds new shape to the document
            //
            Shape rectangle = paragraph.AppendShape(AutoShapeType.Rectangle, 572, boxHeight);
            //Sets position for shape
            rectangle.VerticalPosition = verticalPositionOfNextBox;
            rectangle.HorizontalPosition = 0;

            //Formating the rectangle
            rectangle.LineFormat.Style = (LineStyle)Enum.Parse(typeof(LineStyle), SelectedLineStyle);
        
            //Set rectangle width to 4 so that the border styles are visible
            if (rectangle.LineFormat.Style != LineStyle.Single)
                rectangleWidth = 5;

            rectangle.LineFormat.Weight = rectangleWidth;

            _ = section.AddParagraph() as WParagraph;

            IWTextRange text1, text2, text3, text4 = paragraph.AppendText("");

            //Adds textbody contents to the shape
            paragraph = rectangle.TextBody.AddParagraph() as WParagraph;
            text1 = paragraph.AppendText(Name);
            paragraph.AppendBreak(BreakType.LineBreak);
            paragraph.AppendBreak(BreakType.LineBreak);
            text2 = paragraph.AppendText(item);
            paragraph.AppendBreak(BreakType.LineBreak);
            paragraph.AppendBreak(BreakType.LineBreak);
            text3 = paragraph.AppendText(ClassTitle);

            if (hasYear == true)
            {
                paragraph.AppendBreak(BreakType.LineBreak);
                paragraph.AppendBreak(BreakType.LineBreak);
                text4 = paragraph.AppendText(DateTime.Now.Year.ToString());
            }

            //Text Attributes
            text1.CharacterFormat.Bold = IsBold;
            text2.CharacterFormat.Bold = IsBold;
            text3.CharacterFormat.Bold = IsBold;
            text4.CharacterFormat.Bold = IsBold;

            text1.CharacterFormat.FontSize = (int)FontSize;
            text2.CharacterFormat.FontSize = (int)FontSize;
            text3.CharacterFormat.FontSize = (int)FontSize;
            text4.CharacterFormat.FontSize = (int)FontSize;

            text1.CharacterFormat.FontName = FontSelected;
            text2.CharacterFormat.FontName = FontSelected;
            text3.CharacterFormat.FontName = FontSelected;
            text4.CharacterFormat.FontName = FontSelected;
        }

        static void SaveDocument(MemoryStream memoryStream)
        {
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Sample5.docx", "application/msword", memoryStream);

            //bool permission = PermissionsService.CheckForStoragePermission().Result;

            //if (permission == true)
            //{
            //    try
            //    {
            //        //Saves the memory stream as file.
            //        SaveService saveService = new();
            //        saveService.SaveAndView("Sample.docx", "application/msword", memoryStream);
            //    }
            //    catch (Exception)
            //    {
            //        await Toast.Make("Something went wrong", CommunityToolkit.Maui.Core.ToastDuration.Short, 12).Show();
            //    }
            //}
        }
    }
}
