using DinkToPdf;
using DinkToPdf.Contracts;
using RazorLight;

namespace BibliotecaUteco.Services;

public class PdfService
{
    private readonly IConverter _converter;
    private readonly RazorLightEngine _razorEngine;

    public PdfService(IConverter converter)
    {
        _converter = converter;

        _razorEngine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(typeof(PdfService))
            .UseMemoryCachingProvider()
            .Build();
    }

    // Renderiza un Razor a HTML
    public async Task<string> RenderRazorToString<TModel>(string templatePath, TModel model)
    {
        return await _razorEngine.CompileRenderAsync(templatePath, model);
    }

    // Convierte HTML a PDF
    public byte[] GeneratePdfFromHtml(string html)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
            },
            Objects = {
                new ObjectSettings()
                {
                    HtmlContent = html,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
        };

        return _converter.Convert(doc);
    }
}