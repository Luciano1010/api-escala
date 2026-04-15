using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Features;

public class UploadService : ILeitorArquivo
{
    public async Task<List<string>> LerNomesAsync(IFormFile file)
    {
        var extensao = Path.GetExtension(file.FileName).ToLower();

        return extensao switch
        {
            ".txt" => await LerTxt(file),
            ".xlsx" => await LerExcel(file),
            ".pdf" => await LerPdf(file),
            ".docx" => await LerDocx(file),
            ".csv" => await LerCsvManual(file),
            _ => throw new Exception("Formato não suportado")
        };
    }

    private Task<List<string>> LerTxt(IFormFile file)
    {
        var nomes = new List<string>();

        using var reader = new StreamReader(file.OpenReadStream());

        while (!reader.EndOfStream)
        {
            var linha = reader.ReadLine();

            if (!string.IsNullOrWhiteSpace(linha))
                nomes.Add(NormalizarNome(linha));
        }

        return Task.FromResult(nomes);
    }

    private Task<List<string>> LerCsv(IFormFile file)
    {
        var nomes = new List<string>();

        using var reader = new StreamReader(file.OpenReadStream());

        while (!reader.EndOfStream)
        {
            var linha = reader.ReadLine();

            if (!string.IsNullOrWhiteSpace(linha))
            {
                var partes = linha.Split(',', StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length > 0)
                    nomes.Add(NormalizarNome(partes[0]));
            }
        }

        return Task.FromResult(nomes);
    }

    private Task<List<string>> LerCsvManual(IFormFile file)
    {
       
        bool primeiraLinha = true;
        var nomes = new List<string>();
        using var reader = new StreamReader(file.OpenReadStream());

        while (!reader.EndOfStream)
        {
            var linha = reader.ReadLine();

            if (linha == null)
            {
                continue;
            }

            if (primeiraLinha)
            {
                primeiraLinha= false;
                continue;
            }
            
            linha = linha.Trim();

            if(string.IsNullOrWhiteSpace(linha))
            continue;

            var partes = linha.Split(",");
            var nome = partes[0].Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                nomes.Add(nome);
            }


        }
        return Task.FromResult(nomes);

    }

    private Task<List<string>> LerExcel(IFormFile file)
    {
        var nomes = new List<string>();

        using var stream = file.OpenReadStream();
        using var package = new ExcelPackage(stream);

        var worksheet = package.Workbook.Worksheets.FirstOrDefault();

        if (worksheet?.Dimension == null)
            return Task.FromResult(nomes);

        for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
        {
            var cellValue = worksheet.Cells[row, 1].Text;

            if (!string.IsNullOrWhiteSpace(cellValue))
                nomes.Add(NormalizarNome(cellValue));
        }

        return Task.FromResult(nomes);
    }

    private async Task<List<string>> LerPdf(IFormFile file)
    {
        var nomes = new List<string>();

        using var stream = file.OpenReadStream();
        using var reader = new PdfReader(stream);
        using var pdf = new PdfDocument(reader);

        int totalPaginas = pdf.GetNumberOfPages();

        for (int page = 1; page <= totalPaginas; page++)
        {
            var texto = PdfTextExtractor.GetTextFromPage(pdf.GetPage(page));

            var linhas = texto.Split('\n');

            foreach (var linha in linhas)
            {
                if (!string.IsNullOrWhiteSpace(linha))
                    nomes.Add(NormalizarNome(linha));
            }
        }

        return nomes;
    }

    private Task<List<string>> LerDocx(IFormFile file)
    {
        var nomes = new List<string>();

        using var stream = file.OpenReadStream();
        using var document = WordprocessingDocument.Open(stream, false);

        var body = document.MainDocumentPart?.Document?.Body;

        if (body == null)
            return Task.FromResult(nomes);

        var paragraphs = body.Elements<Paragraph>();

        foreach (var paragraph in paragraphs)
        {
            var text = paragraph.InnerText;

            if (!string.IsNullOrWhiteSpace(text))
                nomes.Add(NormalizarNome(text));
        }

        return Task.FromResult(nomes);
    }

    private string NormalizarNome(string texto)
    {
        return texto.Trim();
    }
}