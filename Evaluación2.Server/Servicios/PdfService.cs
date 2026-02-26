using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.Shared.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProyectoModelado2024.Server.Servicios
{
    public class PdfService : IPdfService
    {
        public PdfService(Context context)
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> GenerarPdfPedidoAsync(CrearPedidoDTO pedido)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("PEDIDO DE COMPRA")
                                .FontSize(20).Bold().FontColor(Colors.Blue.Medium);
                            col.Item().Text($"La Josefa: Bv San Juan")
                                .FontSize(15);
                            col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                .FontSize(10);
                        });
                    });

                    // Content
                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        // Tabla de productos
                        col.Item().Text("PRODUCTOS SOLICITADOS").Bold().FontSize(14);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(4);   // Producto
                                cols.RelativeColumn(1);   // Cantidad
                            });

                            // Header
                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Producto");
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Cantidad");
                            });

                            // Rows
                            foreach (var item in pedido.Renglones)
                            {
                                table.Cell().Padding(5).Text(item.Producto?.Nombre ?? "N/A");
                                table.Cell().Padding(5).Text(item.Cantidad.ToString());
                            }

                        });
                    });

                    // Footer
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
