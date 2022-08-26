using System.Text;
using MarkdownSharp;
using Microsoft.Extensions.FileProviders;
using Ude;

namespace aspnetcore.mvc;

public class MarkdownMiddleware
{
    private readonly RequestDelegate next;
    private readonly IWebHostEnvironment hostEnv;

    public MarkdownMiddleware(RequestDelegate next, IWebHostEnvironment hostEnv)
    {
        this.hostEnv = hostEnv;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString();
        if (!path.EndsWith(".md", true, null))
        {
            await next.Invoke(context);
            return;
        }

        var file = hostEnv.WebRootFileProvider.GetFileInfo(path);
        if (!file.Exists)
        {
            await next.Invoke(context);
            return;
        }
        var mdText = await GetMarkdownTextAsync(file);
        var html = MarkDownToHtml(mdText);
        context.Response.ContentType = "text/html; charset=UTF-8";
        await context.Response.WriteAsync(html);
    }

    private async Task<string> GetMarkdownTextAsync(IFileInfo file)
    {
        using var stream = file.CreateReadStream();
        var charset = DetectCharset(stream);
        using var reader = new StreamReader(stream, Encoding.GetEncoding(charset));
        return await reader.ReadToEndAsync();
    }

    private string MarkDownToHtml(string markdownText)
    {
        var markdown = new Markdown();
        return markdown.Transform(markdownText);
    }

    private string DetectCharset(Stream stream)
    {
        var charDetector = new CharsetDetector();
        charDetector.Feed(stream);
        charDetector.DataEnd();
        var charset = charDetector.Charset ?? "UTF-8";
        stream.Position = 0;
        return charset;
    }
}