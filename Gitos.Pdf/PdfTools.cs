using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gitos.Pdf
{
    public class PdfTools
    {
        public static byte[] WriteHtmlToPdf(string htmlString, string css)
        {
            byte[] bytes;
            //Boilerplate iTextSharp setup here
            //Create a stream that we can write to, in this case a MemoryStream
            using (var ms = new MemoryStream())
            {

                //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                using (var doc = new Document())
                {
                    //Create a writer that's bound to our PDF abstraction and our stream
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();

                        var tagProcessorFactory = Tags.GetHtmlTagProcessorFactory();

                        var htmlPipelineContext = new HtmlPipelineContext(null);
                        htmlPipelineContext.SetTagFactory(tagProcessorFactory);
                        var pdfWriterPipeline = new PdfWriterPipeline(doc, writer);
                        var htmlPipeline = new HtmlPipeline(htmlPipelineContext, pdfWriterPipeline);

                        // get an ICssResolver and add the custom CSS
                        var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
                        cssResolver.AddCss(css, "utf-8", true);
                        var cssResolverPipeline = new CssResolverPipeline(
                            cssResolver, htmlPipeline
                        );


                        var worker = new XMLWorker(cssResolverPipeline, true);
                        var parser = new XMLParser(worker);
                        using (var sr = new StringReader(htmlString))

                        {
                            parser.Parse(sr);

                            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);


                        }
                        doc.Close();
                    }
                    bytes = ms.ToArray();
                }
                return bytes;
            }
        }
    }
}
