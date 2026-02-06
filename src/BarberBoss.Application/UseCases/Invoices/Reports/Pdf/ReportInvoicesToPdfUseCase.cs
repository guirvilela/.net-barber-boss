
using BarberBoss.Application.UseCases.Invoices.Reports.Pdf.Fonts;
using BarberBoss.Domain.Enums.InvoiceStatus;
using BarberBoss.Domain.Enums.PaymentMethod.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Colors;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Invoices.Reports.Pdf;
public class ReportInvoicesToPdfUseCase : IReportInvoicesToPdfUseCase
{
    private const int HEIGHT_ROW_EXPENSE_TABLE = 25;
    private readonly IInvoiceReadOnly _repository;

    public ReportInvoicesToPdfUseCase(IInvoiceReadOnly repository)
    {
        _repository = repository;
        GlobalFontSettings.FontResolver = new InvoicesReportForntResolver();
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var invoices = await _repository.FilterByMonth(month);

        if(invoices.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(month);

        var page = CreatePage(document);

        CreateHeaderProfilePhoto(page);

        var totalInvoices = invoices
            .Where(i => i.Status != InvoiceStatus.Canceled)
            .Sum(i => i.Amount);

        CreateTotalInvoiceSection(page, month, totalInvoices);


        foreach(var invoice in invoices)
        {
            var table = CreateInvoiceTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;

            var invoiceRow = row.Cells[0];
            var invoiceHourRow = row.Cells[1];
            var invoicePaymentTypeRow = row.Cells[2];
            var amountRow = row.Cells[3];

            AddInvoiceTitle(invoiceRow, invoice.ServiceName);
            AddHeaderForAmount(amountRow);

            // Valores das colunas
            row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;

            row.Cells[0].AddParagraph(invoice.Date.ToString("D"));
            SetStyleBaseForCells(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 5;

            row.Cells[1].AddParagraph(invoice.Date.ToString("t"));
            SetStyleBaseForCells(row.Cells[1]);

            row.Cells[2].AddParagraph(invoice.PaymentMethod.PaymentsMethodToString());
            SetStyleBaseForCells(row.Cells[2]);

            AddAmountForInvoices(row.Cells[3], invoice.Amount);

            // Adicionando a linha de descrição do serviço

            if (string.IsNullOrWhiteSpace(invoice.Notes) == false) {
                var noteRow = table.AddRow();
                noteRow.Height = HEIGHT_ROW_EXPENSE_TABLE;

                var noteRowDescription = noteRow.Cells[0];

                noteRow.Cells[0].AddParagraph(invoice.Notes);
                noteRow.Cells[0].Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 9, Color = ColorHelper.GRAY_TEXT };
                noteRow.Cells[0].Shading.Color = ColorHelper.GRAY_LIGHT;
                noteRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                noteRow.Cells[0].Format.LeftIndent = 5;
                //Merge de celulas
                noteRow.Cells[0].MergeRight = 2;
                // Merge de celulas abaixo
                row.Cells[3].MergeDown = 1;

            }
            // Espaçamento invisivel pra cada tabela
            AddWhiteSpaceBetweenTables(table);
        }

        return RenderDocument(document);

    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportInvoiceGenerationMessage.MONTHLY_INVOICE} {month: Y}";
        document.Info.Author = "Guilherme Vilela";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.ROBOTO_REGULAR;

        return document;
    }

    public Section CreatePage(Document document) {
        var section = document.AddSection();

        // Clonando configurações padrão
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;

        // Configurando margens
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 50;
        section.PageSetup.BottomMargin = 50;

        return section;

    }

    public void CreateHeaderProfilePhoto(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var tableRow = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "pdfLogo.png");

        tableRow.Cells[0].AddImage(pathFile);
        tableRow.Cells[0].RoundedCorner = MigraDoc.DocumentObjectModel.Tables.RoundedCorner.TopLeft;
        tableRow.Cells[0].RoundedCorner = MigraDoc.DocumentObjectModel.Tables.RoundedCorner.BottomRight;

        tableRow.Cells[1].AddParagraph("Barbearia Boss");
        tableRow.Cells[1].Format.Font = new Font { Name = FontHelper.BEBAS_NEUE, Size = 25 };
        tableRow.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    public void CreateTotalInvoiceSection(Section page, DateOnly month, decimal totalInvoices)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "20";

        var title = string.Format(ResourceReportInvoiceGenerationMessage.MONTHLY_INVOICE, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 15});

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($" {ResourceReportInvoiceGenerationMessage.CURRENCY_SYMBOL} {totalInvoices}", new Font { Name = FontHelper.BEBAS_NEUE , Size = 50});

      
    }

    public Table CreateInvoiceTable(Section page)
    {
        var table = page.AddTable();

        var column0 = table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    public void AddInvoiceTitle(Cell titleRow, string invoiceTitle)
    {
        titleRow.AddParagraph(invoiceTitle);
        titleRow.Format.Font = new Font { Name = FontHelper.BEBAS_NEUE, Size = 15, Color = ColorHelper.WHITE };
        titleRow.Shading.Color = ColorHelper.BLUE_DARK;
        titleRow.VerticalAlignment = VerticalAlignment.Center;
        titleRow.Format.LeftIndent = 5;
        //Merge de celulas
        titleRow.MergeRight = 2;
    }

    public void AddHeaderForAmount(Cell amountCell)
    {
        amountCell.AddParagraph(ResourceReportInvoiceGenerationMessage.AMOUNT);
        amountCell.Format.Font = new Font { Name = FontHelper.BEBAS_NEUE, Size = 15, Color = ColorHelper.WHITE };
        amountCell.Shading.Color = ColorHelper.GREEN_DARK;
        amountCell.VerticalAlignment = VerticalAlignment.Center;
        amountCell.Format.RightIndent = 5;
    }

    public void AddAmountForInvoices(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{ResourceReportInvoiceGenerationMessage.CURRENCY_SYMBOL} {amount}");
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 12, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.RightIndent = 5;
    }

    public void SetStyleBaseForCells(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.GRAY_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    public void AddWhiteSpaceBetweenTables(Table table)
    {
        var row = table.AddRow();
        row.Height = 15;
        row.Borders.Visible = false;
    }

        private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();

        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
