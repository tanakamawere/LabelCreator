using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using LabelCreator.Services;

namespace LabelCreator
{
    [INotifyPropertyChanged]
    public partial class MainPageViewModel
    {
        public ObservableRangeCollection<string> Subjects { get; set; } = new();

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

        public MainPageViewModel()
        {

        }


        [RelayCommand]
        void SaveDocumentClicked()
        {
            SaveDocument(CreateDocument());
        }

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
            boxHeight = ((float)fontSize * 4) + 100;

            foreach (string item in Subjects)
            {
                if (numberOfLabels.Equals(3))
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
        WSection SectionInstance(WordDocument document)
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
            Shape rectangle = paragraph.AppendShape(AutoShapeType.Rectangle, 572, boxHeight);
            //Sets position for shape
            rectangle.VerticalPosition = verticalPositionOfNextBox;
            rectangle.HorizontalPosition = 0;
            _ = section.AddParagraph() as WParagraph;
            //Adds textbody contents to the shape
            paragraph = rectangle.TextBody.AddParagraph() as WParagraph;
            IWTextRange text1 = paragraph.AppendText($"Name:            {Name}");
            paragraph.AppendBreak(BreakType.LineBreak);
            paragraph.AppendBreak(BreakType.LineBreak);
            IWTextRange text2 = paragraph.AppendText($"Subject:         {item}");
            paragraph.AppendBreak(BreakType.LineBreak);
            paragraph.AppendBreak(BreakType.LineBreak);
            IWTextRange text3 = paragraph.AppendText($"Class:           {ClassTitle}");
            paragraph.AppendBreak(BreakType.LineBreak);
            paragraph.AppendBreak(BreakType.LineBreak);
            IWTextRange text4 = paragraph.AppendText($"Year:            {DateTime.Now.Year}");

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
            saveService.SaveAndView("Sample.docx", "application/msword", memoryStream);
        }
    }
}
